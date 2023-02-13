using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plateforme : MonoBehaviour
{
//    public BoxCollider2D _platforme;

//    private bool canPass = false;

//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (canPass)
//        {
//            platform.enabled = false;
//        }
//        else
//        {
//            platform.enabled = true;
//        }    
//    }

//    function OnTriggerStay2D(other: Collider2D)
//    {
//        oneway = true;
//    }

//    function OnTriggerExit2D(other: Collider2D)
//    {
//        //Just to make sure that the platform's Box Collider does not get permantly disabled and it should be enabeled once the player get its through
//        oneway = false;
//    }


//////the collider of the main visible platform
////var platform : BoxCollider2D;
//////this variable is true when the players is just below the platform so that its Box collider can be disabled that will allow the player to pass through the platform
////var oneway : boolean;


////function Update()
////{
////    //Enabling or Disabling the platform's Box collider to allowing player to pass
////    if (oneway)
////        platform.enabled = false;
////    if (!oneway)
////        platform.enabled = true;
////}
//////Checking the collison of the gameobject we created in step 2 for checking if the player is just below the platform and nedded to ignore the collison to the platform
////function OnTriggerStay2D(other: Collider2D) {
////    oneway = true;
////}

////function OnTriggerExit2D(other: Collider2D) {
////    //Just to make sure that the platform's Box Collider does not get permantly disabled and it should be enabeled once the player get its through
////    oneway = false;
////}
}

