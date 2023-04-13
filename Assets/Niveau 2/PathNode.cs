//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PathNode : MonoBehaviour
//{
//    public int x;
//    public int y;

//    public int gCost;
//    public int hCost;
//    public int fCost;

//    public PathNode cameFromNode;

//    public PathNode(int x, int y, float posX, float posY)
//    {
//        this.x = x;
//        this.y = y;

//        transform.position = new Vector2(posX, posY);
//    }

//    public void CalculateFCost()
//    {
//        fCost = gCost + hCost;
//    }
//}
