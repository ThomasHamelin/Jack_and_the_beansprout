//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Pathfinder : MonoBehaviour
//{
//    [SerializeField] private CréationLabyrinte _creationLabyrinthe = default;

//    private int _longueur, _hauteur, _tailleGrille, _tailleCaseX, _tailleCaseY;

//    private PathNode[][] allNodes = default;
//    private PathNode[] openList =default;
//    //private PathNode[] closedList=default;

//    void Start()
//    {
//        genererGrille();
        
//    }

//    private void genererGrille()
//    {
//        int x = 0;
//        int y = 0;

//        _tailleCaseX = _longueur / _tailleGrille;
//        _tailleCaseY = _hauteur / _tailleGrille;

//        //allNodes.resize(_tailleGrille / _tailleCaseX);
//        //allNodes[0].resize(_tailleGrille / _tailleCaseY);

//        while (x < _longueur)
//        {
//            while (y < _hauteur)
//            {
//                allNodes[x / _tailleCaseX][y / _tailleCaseY] = new PathNode(x, y);
//                x += _tailleCaseX;
//                y += _tailleCaseY;
//            }
//        }
//    }

//    private int CalculateDistanceCost(PathNode p_a, PathNode p_b)
//    {
//        int distanceX = Mathf.Abs(p_a.x - p_b.x);
//        int distanceY = Mathf.Abs(p_a.y - p_b.y);
//        return distanceX + distanceY;
//    }

//    public PathNode[] FindPath(int p_startX, int p_startY)
//    {
//        int endX = (int)this.transform.position.x / _tailleCaseX;
//        int endY = (int)this.transform.position.y / _tailleCaseY;

//        PathNode startNode = allNodes[p_startX][p_startY];
//        PathNode endNode = allNodes[endX][endY];

//        openList = new PathNode[] { startNode };
//        closedList = new PathNode[]();

//        startNode.gCost = 0;
//        startNode.hCost = CalculateDistanceCost(startNode, endNode);
//        startNode.CalculateFCost();

//        return null;
//    }
//}
