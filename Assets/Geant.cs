using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Geant : MonoBehaviour
{

    [SerializeField] private float _VitesseMarche;
    [SerializeField] private float _VitesseCourse;
    [SerializeField] private float _distanceVision;

    private int _rand;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _rand = Random.Range(1, 5);

        RaycastHit2D detect = Physics2D.Raycast(this.transform.position, Vector2.up, LayerMask.GetMask("DetectionMur"));


        float distance = detect.distance;

        RaycastHit2D Up = Physics2D.Raycast(this.transform.position, Vector2.up, distance);
        RaycastHit2D Down = Physics2D.Raycast(this.transform.position, Vector2.down, distance);
        RaycastHit2D Left = Physics2D.Raycast(this.transform.position, Vector2.left, distance);
        RaycastHit2D Right = Physics2D.Raycast(this.transform.position, Vector2.right, distance);

        switch (_rand)
        {
            case 1:
                if (Up == true)
                {

                }
                break;

            case 2:
                if (Down == true)
                {

                }
                break;

            case 3:
                if (Left == true)
                {

                }
                break;                                     

            case 4:
                if (Right == true)
                {

                }
                break;

        }

    }
}
