using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CréationMurs : MonoBehaviour
{
    [SerializeField] float distanceDonné;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        // Cast a ray straight down.
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down);
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, Vector2.up);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, Vector2.left);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, Vector2.right);

        // If it hits something...
        if (hitDown.collider != null)
        {

            float distance = Mathf.Abs(hitDown.point.y - transform.position.y);
               
            distanceDonné = distance;

        }
    }

}
