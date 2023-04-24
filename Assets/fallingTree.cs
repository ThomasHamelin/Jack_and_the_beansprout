using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallingTree : MonoBehaviour
{
    public float finishedFallingHeight = -35f;

    public void fall()
    {
        this.GetComponent<BoxCollider2D>().isTrigger = true;
    }
}
