using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class identitéMur : MonoBehaviour
{
    // Start is called before the first frame update

  

    void Start()
    {
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right);
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up);

        if (hitDown.collider == true)
        {
            float distanceDown = Mathf.Abs(hitDown.point.y - transform.position.y);
            longeurMur = true;
        }
        if (hitLeft.collider == true)
        {
            float distanceLeft = Mathf.Abs(hitLeft.point.x - transform.position.x);

        }
        if (hitRight.collider == true)
        {
            float distanceRight = Mathf.Abs(hitRight.point.x - transform.position.x);

        }
        if (hitUp.collider == true)
        {
            float distanceUp = Mathf.Abs(hitUp.point.y - transform.position.y);

        }
    }
}
    
