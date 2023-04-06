using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Geant : MonoBehaviour
{

    [SerializeField] private float _VitesseMarche;
    [SerializeField] private float _VitesseCourse;
    [SerializeField] private float _distanceVision;
    [SerializeField] private float _rotationSpeed;
    private Rigidbody2D _rb;
    private int _rand;
    private Vector3 velocity;
    private float[] _UniteMouvement = new float[4];

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void PartageDonnées(float Donné, int x)
    {
        _UniteMouvement[x] = Donné;
    }

    //Update is called once per frame
    void Update()
    {
        _rand = Random.Range(1, 5);



        RaycastHit2D Up = Physics2D.Raycast(this.transform.position, Vector2.up, _UniteMouvement[2], LayerMask.GetMask("DetectionMur"));
        RaycastHit2D Down = Physics2D.Raycast(this.transform.position, Vector2.down, _UniteMouvement[2], LayerMask.GetMask("DetectionMur"));
        RaycastHit2D Left = Physics2D.Raycast(this.transform.position, Vector2.left, _UniteMouvement[3], LayerMask.GetMask("DetectionMur"));
        RaycastHit2D Right = Physics2D.Raycast(this.transform.position, Vector2.right, _UniteMouvement[3], LayerMask.GetMask("DetectionMur"));

        switch (_rand)
        {
            case 1:
                if (Up != true)
                {
                    //Quaternion targetRotation = Quaternion.LookRotation(transform.forward, Vector2.up);
                    //Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
                    //_rb.MoveRotation(rotation);

                    Vector2 direction = new Vector2(0, 1) * _VitesseMarche * Time.deltaTime;
                    transform.position += new Vector3(direction.x, direction.y, 0);
                }
                break;

            case 2:
                if (Down != true)
                {
                    //Quaternion targetRotation = Quaternion.LookRotation(transform.forward, Vector2.down);
                    //Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
                    //_rb.MoveRotation(rotation);

                    Vector2 direction = new Vector2(0, -1) * _VitesseMarche * Time.deltaTime;
                    transform.position += new Vector3(direction.x, direction.y, 0);

                }
                break;

            case 3:
                if (Left != true)
                {
                    //Quaternion targetRotation = Quaternion.LookRotation(transform.forward, Vector2.left);
                    //Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
                    //_rb.MoveRotation(rotation);

                    Vector2 direction = new Vector2(-1,0) * _VitesseMarche * Time.deltaTime;
                    transform.position += new Vector3(direction.x, direction.y, 0);

                }
                break;

            case 4:
                if (Right != true)
                {
                    //Quaternion targetRotation = Quaternion.LookRotation(transform.forward, Vector2.right);
                    //Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
                    //_rb.MoveRotation(rotation);

                    Vector2 direction = new Vector2(1,0) * _VitesseMarche * Time.deltaTime;
                    transform.position += new Vector3(direction.x, direction.y, 0);


                }
                break;

        }

    }

}
