using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;

using System.Data;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static System.Net.WebRequestMethods;
using static UnityEngine.GraphicsBuffer;




public class Geant : MonoBehaviour
{

    [SerializeField] private float _VitesseMarche;
    [SerializeField] private float _VitesseCourse;
    [SerializeField] private float _distanceVision;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private GameObject _yeux;
    [SerializeField] private GameObject _geant;


    private float[] _UniteMouvement = new float[4];


    private float _VecteurX;
    private float _VecteurY;
    private float _VecteurDroiteX;
    private float _VecteurDroiteY;


    private int Direction, Mouvement, m,n;

    private float increment;

    private bool _finiTourne = true, _finiAvance = true;

    private Vector3 TargetPosition;
    private Vector3 PositionInit;

   

    RaycastHit2D hitDevant;
    RaycastHit2D hitDroite;

    private Vector3 ROTZtarget = Vector3.zero;
    private Vector3 ROTinit;

   
    
    void Update()
    {


        if (_finiTourne == true && _finiAvance == true)
        {
            analyse();
        }
        if (_finiTourne == false)
        {
            Rotate(Direction);
        }
        if(_finiTourne == true && _finiAvance == false)
        {
            avance(Mouvement);
        }

    }



    public void PartageDonn�es(float Donn�, int x)
    {

        _UniteMouvement[x] = Donn�;

    }

    void analyse()
    {





        _VecteurX = 5 * Mathf.Cos(Mathf.Deg2Rad * (transform.eulerAngles.z));
        _VecteurY = 5 * Mathf.Sin(Mathf.Deg2Rad * (transform.eulerAngles.z));

        Vector2 dir = new Vector3(_VecteurX + transform.position.x, _VecteurY + transform.position.y);


        _VecteurDroiteX = 5 * Mathf.Cos(Mathf.Deg2Rad * (transform.eulerAngles.z - 90));
        _VecteurDroiteY = 5 * Mathf.Sin(Mathf.Deg2Rad * (transform.eulerAngles.z - 90));

        Vector2 dirDroite = new Vector3(_VecteurDroiteX + transform.position.x, _VecteurDroiteY + transform.position.y);


      

        hitDevant = Physics2D.Raycast(this.transform.position, transform.TransformDirection(Vector2.right), _UniteMouvement[2], LayerMask.GetMask("DetectionMur"));
        hitDroite = Physics2D.Raycast(this.transform.position, transform.TransformDirection(Vector2.down), _UniteMouvement[3], LayerMask.GetMask("DetectionMur"));

        PositionInit = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        ROTinit = new Vector3(0, 0, this.transform.eulerAngles.z);
        m = 1;
        n = 1;
        _finiAvance = false;
        _finiTourne = false;

        if (hitDevant == false && hitDroite == false)//yes
        {
            //dir = -1
            //confirm =1

            Direction = -1;
            Mouvement = 1;

        }
        if (hitDevant == false && hitDroite == true)//yes
        {
            //dir = 0
            //confirm =1

            Direction = 0;
            Mouvement = 1;
        }
        if (hitDevant == true && hitDroite == false)//yes
        {
            //dir = -1
            //confirm =1
            Direction = -1;
            Mouvement = 1;
        }
        if (hitDevant == true && hitDroite == true)//yes
        {
            //dir = 1
            //confirm == 0;
            Direction = 1;
            Mouvement = 0;
        }

    }


  


    void avance(int moveCondition)
    {



        if (moveCondition == 1 && m == 1)
        {
            int angle = (int)(transform.eulerAngles.z % 360);
            int i = 0;
            int y = 0;

            if (angle == 180)
            {
                i = -1;
            }
            else if (angle == 0)
            {
                i = 1;
            }
            else
            {
                i = 0;
            }

            if (angle == 270)
            {
                y = -1;
            }
            else if (angle == 90)
            {
                y = 1;
            }
            else
            {
                y = 0;
            }
            m = 0;
            TargetPosition = new Vector3(transform.position.x + (_UniteMouvement[0] * i), transform.position.y + (_UniteMouvement[1] * y), 0);
        }
        else if (moveCondition == 0)
        {
            _finiAvance = true;
        }

       


        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, _VitesseMarche*Time.deltaTime);

        if (transform.position == TargetPosition)
        {
            _finiAvance = true;
        }
        else
        {
            _finiAvance = false;
        }
    }

    void Rotate(float dir)
    {
        if(n == 1)
        {
            Vector3 angle  = new Vector3(0,0,this.transform.eulerAngles.z + (90 * dir));

            ROTZtarget.z = Mathf.Round(mod(angle.z, 360));
            

            increment = (90 * dir) / _rotationSpeed;
            n = 0;
        }

        if (dir != 0)
        {

            float posMoment = Mathf.Round(mod(transform.eulerAngles.z, 360));



            if (posMoment == ROTZtarget.z)
            {
                _finiTourne = true;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + increment);
                _finiTourne = false;
            }
        }
        else
        {
            _finiTourne = true;
        }
    }

    float mod(float x, float y)
    {
        return (x % y + y) % y;
    }
}

