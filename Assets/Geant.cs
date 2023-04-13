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

    private byte _AncienneDirection;

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
        if(_AncienneDirection == 1)
        {
            RaycastHit2D hitup = Physics2D.Raycast(transform.position, Vector2.up, _UniteMouvement[3]);
            RaycastHit2D hitleft = Physics2D.Raycast(transform.position, Vector2.left, _UniteMouvement[2]);
            RaycastHit2D hitright = Physics2D.Raycast(transform.position, Vector2.right, _UniteMouvement[2]);


        }
        else if (_AncienneDirection == 2)
        {

        }
        else if (_AncienneDirection == 3)
        {

        }
        else if (_AncienneDirection == 4)
        {

        }










    }

}
