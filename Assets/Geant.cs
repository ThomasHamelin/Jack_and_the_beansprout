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
    private float[] _UniteMouvement = new float[2];

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    public void PartageDonnées(float Donné, int x)
    {
        _UniteMouvement[x] = Donné;
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
                    Quaternion targetRotation = Quaternion.LookRotation(transform.forward, Vector2.up); 
                    Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
                    _rb.MoveRotation(rotation);
                    Vector3 position = new Vector3(transform.position.x, transform.position.y + _UniteMouvement[1], 0);
                    velocity = new Vector3(0, _VitesseMarche,0);
                    _rb.MovePosition(position + velocity * Time.deltaTime);
                    
                }
                break;

            case 2:
                if (Down == true)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(transform.forward, Vector2.down);
                    Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
                    _rb.MoveRotation(rotation);
                }
                break;

            case 3:
                if (Left == true)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(transform.forward, Vector2.left);
                    Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
                    _rb.MoveRotation(rotation);
                }
                break;                                     

            case 4:
                if (Right == true)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(transform.forward, Vector2.right);
                    Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
                    _rb.MoveRotation(rotation);
                }
                break;

        }

    }
}
