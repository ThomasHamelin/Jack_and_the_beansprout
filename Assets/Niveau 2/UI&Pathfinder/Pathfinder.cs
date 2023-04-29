using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] private Cr�ationLabyrinte _creationLabyrinthe = default;
    [SerializeField] private PathNode _pathNode = default;
    [SerializeField] private GameObject _pathNodeContainer = default;

    //Dimensions de la grille de noeuds
    private float _longueur = 100;
    private float _hauteur = 100; 
    private int _tailleGrille = 10; //Nombre de noeuds par rang�

    //Coordonn�es du premier noeud de la grille
    private float _coordonneDepartX = 0;
    private float _coordonneDepartY = 0;

    //Taille d'une case contenant un noeud
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
        _tailleGrille = (int)_creationLabyrinthe._tailleGrille;
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

        //� partir du nombre de noeuds, on ajuste la taille du tableau contenant tous les noeuds
        allNodes = new PathNode[_tailleGrille, _tailleGrille];

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

    /*
     * R�le : Calculer le co�t li� � la distance entre deux noeuds
     * Entr�e : 2 PathNode qui sont les noeuds dont on doit calculer la distance
     * Sortie : 1 entier qui est le co�t li� � la distance
     */
    private int CalculateDistanceCost(PathNode p_a, PathNode p_b)
    {
        int distanceX = Mathf.Abs(p_a.x - p_b.x); //Calcul de la distance en x
        int distanceY = Mathf.Abs(p_a.y - p_b.y);//Calcul de la distance en y
        return distanceX + distanceY; //On retourne la somme des distances en x et en y
    }

    /*
     * R�le : D�terminer quel noeud de la liste ouverte poss�de le co�t f le plus faible
     * Entr�e : Aucune
     * Sortie : 1 PathNode qui est le noeud avec le co�t f le plus faible
     */
    private PathNode GetLowestFCostNode()
    {
        PathNode lowestFCostNode = openList[0]; //On commence avec le premier noeud de la liste

        foreach (PathNode currentNode in openList)//On regarde tous les autres noeuds
        {
            if (currentNode.fCost < lowestFCostNode.fCost) //Si le noeud a un co�t f plus petit que le plus petit co�t f pr�c�dent
            {
                lowestFCostNode = currentNode; //Ce noeud devient celui avec le co�t f le faible
            }
        }

        return lowestFCostNode; 
    }

    /*
     * R�le : Retracer le chemin parcouru
     * Entr�e : 1 PathNode qui est le noeud final
     * Sortie : Une liste de PathNode qui est le chemin parcouru
     */
    private List<PathNode> CalculatePath(PathNode p_endNode)
    {
        //On cr�e une liste de PathNode qui commence par le noeud final
        List<PathNode> path = new List<PathNode>();
        path.Add(p_endNode);

        //En commen�ant par le noeud final
        PathNode currentNode = p_endNode;

        //Tant qu'on n'est pas arriv� au noeud initial
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode); //On retrace par quel noeud on est arriv�
            currentNode = currentNode.cameFromNode; //On passe au noeud pr�c�dent
        }

        path.Reverse(); //On inverse la liste pour que le noeud initial soit au d�but
        return path;
    }

    /*
     * R�le : D�terminer les noeuds adjacents � un noeud
     * Entr�e : 1 PathNode qui est le noeud dont on veut regarder les voisins
     * Sortie : 1 liste de PathNode qui contient tous les noeuds adjacents
     */
    private List<PathNode> GetNeighbourList(PathNode p_currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        if (p_currentNode.DetectWallSide(-_tailleCaseX)) //Regarde s'il y a un mur � gauche
        {
            neighbourList.Add(allNodes[p_currentNode.x - 1, p_currentNode.y]); //Si la voie est libre, on ajoute le noeud qui se trouve � gauche
        }
        if (p_currentNode.DetectWallSide(_tailleCaseX)) //Regarde s'il y a un mur � droite
        {
            neighbourList.Add(allNodes[p_currentNode.x + 1, p_currentNode.y]); //Si la voie est libre, on ajoute le noeud qui se trouve � droite
        }
        if (p_currentNode.DetectWallUp(-_tailleCaseY)) //Regarde en bas
        {
            neighbourList.Add(allNodes[p_currentNode.x, p_currentNode.y - 1]); //Si la voie est libre, on ajoute le noeud qui se trouve en bas
        }
        if (p_currentNode.DetectWallUp(_tailleCaseY)) //Regarde en haut
        {
            neighbourList.Add(allNodes[p_currentNode.x, p_currentNode.y + 1]); //Si la voie est libre, on ajoute le noeud qui se trouve en haut
        }
        return neighbourList;
    }

    /*
     * R�le : Trouver le noeud qui se trouve � une position sp�cifique
     * Entr�e : 1 Vector2 qui la position du noeud cherch�
     * Sortie : 1 PathNode qui est le noeud � la position sp�cifique
     */
    private PathNode GetNodeAtPosition(Vector2 p_position)
    {
        Collider2D[] nodesAtPosition = Physics2D.OverlapCircleAll(p_position, 1f);//On cr�e une liste de tous les objets se trouvant � la position cherch�e

        foreach(Collider2D node in nodesAtPosition) //On regarde tous les objects � cette position
        {
            if (node.tag == "PathNode") //Si c'est un noeud
            {
                return node.GetComponent<PathNode>(); //On indique que c'est le noeud qu'on cherche
            }
            
        }

        //Si aucun noeud ne se trouve � cette position, on retourne le noeud final
        return null;
    }

    /*
     * R�le : Trouver le chemin le plus court entre deux noeuds
     * Entr�e : 2 float qui sont les coordonn�es en x et en y du noeud de d�part
     * Sortie : 1 liste de PathNode qui correspond au chemin le plus court
     */
    public List<PathNode> FindPath(float p_startX, float p_startY)
    { 
        ResetAllNodes(); //On s'assure que les co�ts de tous les noeuds soient initialis�s � 0

        //On d�termine un noeud final et un noeud initial
        _endNode = allNodes[0, 0];

        _startNode = GetNodeAtPosition(new Vector3(p_startX, p_startY));
        

        //Initialisation des listes ouvertes et ferm�es
        openList = new List<PathNode> { _startNode };
        closedList = new List<PathNode>();

        //Calcule des co�ts li�s au noeud de d�part
        _startNode.hCost = CalculateDistanceCost(_startNode, _endNode);
        _startNode.CalculateFCost();

        //Tant qu'il reste des noeuds � analyser
        while (openList.Count > 0)
        {
            //D�termine quel noeud parmi ceux � analyser � le co�t f le plus petit
            PathNode currentNode = GetLowestFCostNode();

            //Si ce noeud est le noeud final
            if (currentNode == _endNode)
            {
                return CalculatePath(_endNode); //On retrace le chemin qui a men� au noeud final
            }

            //D�termine les noeuds adjacents � celui qu'on est en train d'analyser
            List<PathNode> neighbourList = GetNeighbourList(currentNode);

            foreach (PathNode neighbourNode in neighbourList) //Pour chaque noeud adjacent
            {
                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode); //Calcul du co�t f

                if (closedList.Contains(neighbourNode)) //Si le noeud a d�j� �t� analys�
                {
                    if (tentativeGCost < neighbourNode.gCost) //Si le nouveau co�t f est plus petit que le pr�c�dent
                    {
                        //Changement du pr�d�cesseur
                        neighbourNode.cameFromNode = currentNode;
                        
                        //Recalcul des co�ts du noeud
                        neighbourNode.gCost = tentativeGCost;
                        neighbourNode.CalculateFCost();
                    }
                }
                else //Si c'est la premi�re fois qu'on regarde ce noeud
                {
                    openList.Add(neighbourNode); //Ajout de ce noeud � la liste des noeuds � analyser

                    //Calcul des co�ts du noeud
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, _endNode);
                    neighbourNode.CalculateFCost();

                    //Indique le pr�d�cesseur
                    neighbourNode.cameFromNode = currentNode;
                    
                }

                openList.Remove(currentNode);//Ce noeud n'est plus � analyser
                closedList.Add(currentNode);//Indique que le noeud � d�j� �t� analys�


            }
        }

        //Si on n'arrive pas au noeud final, on indique qu'il n'y a aucun chemin
        return null;
    }

    /*
     * R�le : R�initialiser les co�ts des noeuds � 0
     * Entr�e : 1 PathNode qui est le noeud dont on veut regarder les voisins
     * Sortie : 1 liste de PathNode qui contient tous les noeuds adjacents
     */
    public void ResetAllNodes()
    {
        foreach(PathNode node in allNodes)
        {
            node.gCost = 0;
            node.hCost = 0;
            node.fCost = 0;
            node.cameFromNode = null;
        }
    }
}

