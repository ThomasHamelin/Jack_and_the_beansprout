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

    int _endX = 0;
    int _endY = 0;

    private PathNode[,] allNodes = default;
    private List<PathNode> openList = default;
    private List<PathNode> closedList = default;
    private PathNode _endNode = default;
    private PathNode _startNode = default;

    void Start()
    {
        _creationLabyrinthe = FindObjectOfType<Cr�ationLabyrinte>().GetComponent<Cr�ationLabyrinte>();
        getMazeComponents();
        genererGrille();
        
    }

    private void getMazeComponents()
    {
        _longueur = _creationLabyrinthe._longueur;
        _hauteur = _creationLabyrinthe._largeur;
        _tailleGrille = _creationLabyrinthe._tailleGrille;
        _coordonneDepartX = _creationLabyrinthe._CoordonneDepartX;
        _coordonneDepartY = _creationLabyrinthe._CoordonneDepartY;
    }

    private void genererGrille()
    {
        float posX = 0;
        float posY = 0;

        _tailleCaseX = _longueur / _tailleGrille;
        _coordonneDepartX += _tailleCaseX / 2;
        _tailleCaseY = _hauteur / _tailleGrille;
        _coordonneDepartY += _tailleCaseY / 2;

        int nbrCaseX = (int)(_longueur / _tailleCaseX);
        int nbrCaseY = (int)(_hauteur / _tailleCaseY);
        allNodes = new PathNode[nbrCaseX, nbrCaseY];

        while (posX < _longueur)
        {
            while (posY < _hauteur)
            {
                Vector3 position = new Vector3(posX + _coordonneDepartX, posY + _coordonneDepartY, 0f);
                PathNode newNode = Instantiate(_pathNode, position, Quaternion.identity);
                allNodes[(int)(position.x / _tailleCaseX), (int)(position.y / _tailleCaseY)] = newNode;
                newNode.setPosition(posX, posY, _tailleCaseX, _tailleCaseY);
                newNode.transform.parent = _pathNodeContainer.transform;
                posY += _tailleCaseY;
            }
            posX += _tailleCaseX;
            posY = 0;
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
        while (currentNode.getCameFromNode() != _startNode)
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
        _endNode = allNodes[_endX, _endY];
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

