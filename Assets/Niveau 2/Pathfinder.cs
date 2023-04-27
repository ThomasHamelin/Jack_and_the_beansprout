using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] private Cr�ationLabyrinte _creationLabyrinthe = default;
    [SerializeField] private PathNode _pathNode = default;
    [SerializeField] private GameObject _pathNodeContainer = default;

    private float _longueur = 100;
    private float _hauteur = 100;
    private float _tailleGrille = 10;
    private float _coordonneDepartX = 0;
    private float _coordonneDepartY = 0;
    private float _tailleCaseX;
    private float _tailleCaseY;

    private PathNode[,] allNodes = default; //Array qui contient tous les noeuds
    private List<PathNode> openList = default; //Liste des noeuds � analyser
    private List<PathNode> closedList = default; //Liste des noeuds qui ont �t� analys�s
    private PathNode _endNode = default; //Noeud � la fin du chemin
    private PathNode _startNode = default; //Noeud au d�but du chemin

    void Start()
    {
        getMazeComponents();
        genererGrille();
        
    }

    /*
     * R�le : Copier les composantes qui ont servies � cr�er le labyrinthe
     * Entr�e : Aucune
     * Sortie : Aucune
     */
    private void getMazeComponents()
    {
        _longueur = _creationLabyrinthe._longueur;
        _hauteur = _creationLabyrinthe._largeur;
        _tailleGrille = _creationLabyrinthe._tailleGrille;
        _coordonneDepartX = _creationLabyrinthe._CoordonneDepartX;
        _coordonneDepartY = _creationLabyrinthe._CoordonneDepartY;
    }

    /*
     * R�le : G�n�rer la grille de noeud
     * Entr�e : Aucune
     * Sortie : Aucune
     */
    private void genererGrille()
    {
        float posX = 0;
        float posY = 0;

        //On d�termine la taille d'un noeud
        _tailleCaseX = _longueur / _tailleGrille; 
        _tailleCaseY = _hauteur / _tailleGrille;

        //On ajuste les valeurs des coordonn�es de d�part, car elles sont en fonction du coin du labyrinthe
        _coordonneDepartX += _tailleCaseX / 2;
        _coordonneDepartY += _tailleCaseY / 2;

        //On d�termine le nombre de noeuds
        int nbrCaseX = (int)(_longueur / _tailleCaseX);
        int nbrCaseY = (int)(_hauteur / _tailleCaseY);
        //� partir du nombre de noeuds, on ajuste la taille du tableau contenant tous les noeuds
        allNodes = new PathNode[nbrCaseX, nbrCaseY];

        //Tant qu'on n'a pas d�pass� la taille horizontale de la grille
        while (posX < _longueur)
        {
            //Tant qu'on n'a pas d�pass� la taille verticale de la grille
            while (posY < _hauteur)
            {
                //On d�termine la position dans la grille o� on est rendu
                Vector3 position = new Vector3(posX + _coordonneDepartX, posY + _coordonneDepartY, 0f);

                PathNode newNode = Instantiate(_pathNode, position, Quaternion.identity); //On cr�e un nouveau noeud
                allNodes[(int)(position.x / _tailleCaseX), (int)(position.y / _tailleCaseY)] = newNode; //On ajoute ce noeud dans le tableau de tous les noeuds
                newNode.setPosition(posX, posY, _tailleCaseX, _tailleCaseY); //On met le noeud � la position o� on est rendu
                newNode.transform.parent = _pathNodeContainer.transform; //Dans la hierarchie, on met ce noeud dans un container de noeuds

                posY += _tailleCaseY;//On va � la position verticale suivante
            }
            posX += _tailleCaseX;//On va � la position horizontale suivante
            posY = 0;//On remet la position verticale � 0
        }
    }

    private int CalculateDistanceCost(PathNode p_a, PathNode p_b)
    {
        int distanceX = Mathf.Abs(p_a.getX() - p_b.getX());
        int distanceY = Mathf.Abs(p_a.getY() - p_b.getY());
        return distanceX + distanceY;
    }

    private PathNode GetLowestFCostNode()
    {
        PathNode lowestFCostNode = openList[0];
        foreach (PathNode currentNode in openList)
        {
            if (currentNode.getFCost() < lowestFCostNode.getFCost())
            {
                lowestFCostNode = currentNode;
            }
        }

        return lowestFCostNode;
    }

    private List<PathNode> CalculatePath(PathNode p_endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(p_endNode);
        PathNode currentNode = p_endNode;
        while (currentNode.getCameFromNode() != null)
        {
            path.Add(currentNode.getCameFromNode());
            currentNode = currentNode.getCameFromNode();
        }

        path.Reverse();
        ResetAllNodes();
        return path;
    }

    private List<PathNode> GetNeighbourList(PathNode p_currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        if (p_currentNode.DetectWallLeft(_tailleCaseX))
        {
            neighbourList.Add(allNodes[p_currentNode.getX() - 1, p_currentNode.getY()]);
        }
        if (p_currentNode.DetectWallSide(_tailleCaseX))
        {
            neighbourList.Add(allNodes[p_currentNode.getX() + 1, p_currentNode.getY()]);
        }
        if (p_currentNode.DetectWallUp(-_tailleCaseY))
        {
            neighbourList.Add(allNodes[p_currentNode.getX(), p_currentNode.getY() - 1]);
        }
        if (p_currentNode.DetectWallUp(_tailleCaseY))
        {
            neighbourList.Add(allNodes[p_currentNode.getX(), p_currentNode.getY() + 1]);
        }
        return neighbourList;
    }

    private PathNode GetNodeAtPosition(Vector2 p_position)
    {
        Collider2D[] nodesAtPosition = Physics2D.OverlapCircleAll(p_position, 0.5f);
        foreach(Collider2D node in nodesAtPosition)
        {
            if (node.tag == "PathNode")
            {
                return node.GetComponent<PathNode>();
            }
            
        }

        return _endNode;
    }

    public List<PathNode> FindPath(float p_startX, float p_startY)
    { 
        _endNode = allNodes[0, 0];
        _startNode = GetNodeAtPosition(new Vector3(p_startX, p_startY));


        openList = new List<PathNode> { _startNode };
        closedList = new List<PathNode>();

        _startNode.gCost = 0;
        _startNode.hCost = CalculateDistanceCost(_startNode, _endNode);
        _startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode();
            if (currentNode == _endNode)
            {
                return CalculatePath(_endNode);
            }

            List<PathNode> neighbourList = GetNeighbourList(currentNode);

            foreach (PathNode neighbourNode in neighbourList)
            {
                int tentativeGCost = currentNode.getGCost() + CalculateDistanceCost(currentNode, neighbourNode);

                if (closedList.Contains(neighbourNode))
                {
                    if (tentativeGCost < neighbourNode.getGCost())
                    {
                        neighbourNode.cameFromNode = currentNode;
                        neighbourNode.gCost = tentativeGCost;
                        neighbourNode.CalculateFCost();
                    }
                }
                else
                {
                    openList.Add(neighbourNode);
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, _endNode);
                    neighbourNode.CalculateFCost();
                    neighbourNode.setCameFromNode(currentNode);
                    
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);


            }
        }

        return null;
    }

    public void ResetAllNodes()
    {
        foreach(PathNode node in allNodes)
        {
            node.gCost = 0;
            node.hCost = 0;
            node.fCost = 0;
            node.cameFromNode = null;
        }
        openList.Clear();
        closedList.Clear();
    }
}

