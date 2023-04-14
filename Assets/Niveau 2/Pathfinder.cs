using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] private CréationLabyrinte _creationLabyrinthe = default;

    private float _longueur = 0;
    private float _hauteur = 0;
    private int _tailleGrille = 1;
    private float _tailleCaseX;
    private float _tailleCaseY;
    int _endX, _endY;

    private List<List<PathNode>> allNodes = default;
    private List<PathNode> openList = default;
    private List<PathNode> closedList = default;

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

        //ResizeList(allNodes, _tailleGrille);


        while (posX < _longueur)
        {
            while (posY < _hauteur)
            {
                allNodes[(int)(posX / _tailleCaseX)].Add(new PathNode((int)(posX / _tailleCaseX), (int)(posY / _tailleCaseY), posX, posY));
                posX += _tailleCaseX;
                posY += _tailleCaseY;
            }
        }
    }

    private int CalculateDistanceCost(PathNode p_a, PathNode p_b)
    {
        int distanceX = Mathf.Abs(p_a.x - p_b.x);
        int distanceY = Mathf.Abs(p_a.y - p_b.y);
        return distanceX + distanceY;
    }

    private PathNode GetLowestFCostNode(List<PathNode> p_pathNodeList)
    {
        PathNode lowestFCostNode = p_pathNodeList[0];
        foreach (PathNode currentNode in p_pathNodeList)
        {
            if (currentNode.fCost < lowestFCostNode.fCost)
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
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }

        path.Reverse();
        return path;
    }

    private List<PathNode> GetNeighbourList(PathNode p_currentNode)
    {
        List<PathNode> neighbourList = new List<PathNode>();

        if (p_currentNode.x - 1 >= 0)
        {
            neighbourList.Add(allNodes[p_currentNode.x - 1][p_currentNode.y]);
        }
        if ((p_currentNode.x + 1) <= allNodes.Count)
        {
            neighbourList.Add(allNodes[p_currentNode.x + 1][p_currentNode.y]);
        }
        if (p_currentNode.y - 1 >= 0)
        {
            neighbourList.Add(allNodes[p_currentNode.x - 1][p_currentNode.y - 1]);
        }
        if ((p_currentNode.y + 1) < allNodes[0].Count)
        {
            neighbourList.Add(allNodes[p_currentNode.x][p_currentNode.y + 1]);
        }
        return neighbourList;
    }

    public List<PathNode> FindPath(int p_startX, int p_startY)
    {
        PathNode startNode = allNodes[p_startX][p_startY];
        PathNode endNode = allNodes[_endX][_endY];

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode)
            {
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
            {
                if (closedList.Contains(neighbourNode))
                {
                    int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                    if (tentativeGCost < neighbourNode.gCost)
                    {
                        neighbourNode.cameFromNode = currentNode;
                        neighbourNode.gCost = tentativeGCost;
                        neighbourNode.CalculateFCost();
                    }
                }
                else
                {
                    openList.Add(neighbourNode);
                }


            }
        }

        return null;
    }
}

