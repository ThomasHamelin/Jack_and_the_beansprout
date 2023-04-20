//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Pathfinder : MonoBehaviour
//{
//    //[SerializeField] private CréationLabyrinte _creationLabyrinthe = default;

//    private float _longueur = 10;
//    private float _hauteur = 10;
//    private int _tailleGrille = 10;
//    private float _coordonneDepartX = 0;
//    private float _coordonneDepartY = 0;
//    private float _tailleCaseX;
//    private float _tailleCaseY;
//    private PathNode _pathNode;
//    int _endX = 0;
//    int _endY = 0;

//    private List<List<PathNode>> allNodes = default;
//    private List<PathNode> openList = default;
//    private List<PathNode> closedList = default;

//    void Start()
//    {
//        genererGrille();

//    }

//    private void genererGrille()
//    {
//        float posX = 0;
//        float posY = 0;

//        _tailleCaseX = _longueur / _tailleGrille;
//        _tailleCaseY = _hauteur / _tailleGrille;

//        while (posX < _longueur)
//        {
//            while (posY < _hauteur)
//            {
//                Vector3 position = new Vector3(posX + _coordonneDepartX, posY + _coordonneDepartY);
//                PathNode newNode = Instantiate(_pathNode, position, Quaternion.Identity());
//                allNodes[(int)(posX / _tailleCaseX)].Add(newNode);
//                posX += _tailleCaseX;
//                posY += _tailleCaseY;
//            }
//        }
//    }

//    private int CalculateDistanceCost(PathNode p_a, PathNode p_b)
//    {
//        int distanceX = Mathf.Abs(p_a.transform.position.x - p_b.transform.position.x);
//        int distanceY = Mathf.Abs(p_a.transform.position.y - p_b.transform.position.y);
//        return distanceX + distanceY;
//    }

//    private PathNode GetLowestFCostNode(List<PathNode> p_pathNodeList)
//    {
//        PathNode lowestFCostNode = p_pathNodeList[0];
//        foreach (PathNode currentNode in p_pathNodeList)
//        {
//            if (currentNode.fCost < lowestFCostNode.fCost)
//            {
//                lowestFCostNode = currentNode;
//            }
//        }

//        return lowestFCostNode;
//    }

//    private List<PathNode> CalculatePath(PathNode p_endNode)
//    {
//        List<PathNode> path = new List<PathNode>();
//        path.Add(p_endNode);
//        PathNode currentNode = p_endNode;
//        while (currentNode.cameFromNode != null)
//        {
//            path.Add(currentNode.cameFromNode);
//            currentNode = currentNode.cameFromNode;
//        }

//        path.Reverse();
//        return path;
//    }

//    private List<PathNode> GetNeighbourList(PathNode p_currentNode)
//    {
//        List<PathNode> neighbourList = new List<PathNode>();

//        if (p_currentNode.DetectWallSide(-_tailleCaseX))
//        {
//            neighbourList.Add(allNodes[p_currentNode.transform.position.x - 1][p_currentNode.transform.position.y]);
//        }
//        if (p_currentNode.DetectWallSide(_tailleCaseX))
//        {
//            neighbourList.Add(allNodes[p_currentNode.transform.position.x + 1][p_currentNode.transform.position.y]);
//        }
//        if (p_currentNode.DetectWallUp(-_tailleCaseY))
//        {
//            neighbourList.Add(allNodes[p_currentNode.transform.position.x - 1][p_currentNode.transform.position.y - 1]);
//        }
//        if (p_currentNode.DetectWallUp(_tailleCaseY))
//        {
//            neighbourList.Add(allNodes[p_currentNode.transform.position.x][p_currentNode.transform.position.y + 1]);
//        }
//        return neighbourList;
//    }

//    private PathNode GetNodeAtPosition(float p_startX, float p_startY)
//    {
//        foreach(List<PathNode> listNode in allNodes)
//        {
//            foreach(PathNode currentNode in listNode)
//            {
//                if(currentNode.transform.position.x == p_startX && currentNode.transform.position.y == p_startY)
//                {
//                    return currentNode;
//                }
//            }
//        }
        

//        return null;
//    }

//    public List<PathNode> FindPath(float p_startX, float p_startY)
//    {
//        PathNode startNode = GetNodeAtPosition(p_startX, p_startY);
//        PathNode endNode = allNodes[_endX][_endY];

//        openList = new List<PathNode> { startNode };
//        closedList = new List<PathNode>();

//        startNode.gCost = 0;
//        startNode.hCost = CalculateDistanceCost(startNode, endNode);
//        startNode.CalculateFCost();

//        while (openList.Count > 0)
//        {
//            PathNode currentNode = GetLowestFCostNode(openList);
//            if (currentNode == endNode)
//            {
//                return CalculatePath(endNode);
//            }

//            openList.Remove(currentNode);
//            closedList.Add(currentNode);

//            foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
//            {
//                if (closedList.Contains(neighbourNode))
//                {
//                    int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
//                    if (tentativeGCost < neighbourNode.gCost)
//                    {
//                        neighbourNode.cameFromNode = currentNode;
//                        neighbourNode.gCost = tentativeGCost;
//                        neighbourNode.CalculateFCost();
//                    }
//                }
//                else
//                {
//                    openList.Add(neighbourNode);
//                }


//            }
//        }

//        return null;
//    }
//}

