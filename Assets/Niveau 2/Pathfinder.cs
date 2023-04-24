using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    //[SerializeField] private CréationLabyrinte _creationLabyrinthe = default;
    [SerializeField] private PathNode _pathNode = default;

    private float _longueur = 100;
    private float _hauteur = 100;
    private int _tailleGrille = 10;
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
        genererGrille();

    }

    private void genererGrille()
    {
        float posX = 0;
        float posY = 0;

        _tailleCaseX = _longueur / _tailleGrille;
        _tailleCaseY = _hauteur / _tailleGrille;

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
        Debug.Log(distanceX + " + " + distanceY);
        return distanceX + distanceY;
    }

    private PathNode GetLowestFCostNode()
    {
        PathNode lowestFCostNode = openList[0];
        foreach (PathNode currentNode in openList)
        {
            currentNode.GetComponent<SpriteRenderer>().color = Color.yellow;
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
        _endNode.GetComponent<SpriteRenderer>().color = Color.red;
        _startNode = GetNodeAtPosition(new Vector3(p_startX, p_startY));
        _startNode.GetComponent<SpriteRenderer>().color = Color.green;


        openList = new List<PathNode> { _startNode };
        closedList = new List<PathNode>();

        _startNode.gCost = 0;
        _startNode.hCost = CalculateDistanceCost(_startNode, _endNode);
        _startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode();
            currentNode.GetComponent<SpriteRenderer>().color = Color.yellow;
            if (currentNode == _endNode)
            {
                return CalculatePath(_endNode);
            }

            List<PathNode> neighbourList = GetNeighbourList(currentNode);

            foreach (PathNode neighbourNode in neighbourList)
            {
                neighbourNode.GetComponent<SpriteRenderer>().color = Color.yellow;
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
}

