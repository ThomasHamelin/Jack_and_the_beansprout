using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.Mathematics;

using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;
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
    float timeElapsed;

    private float _VecteurX;
    private float _VecteurY;

    private float _VecteurDroiteX;
    private float _VecteurDroiteY;
    private float _angleT = 1;

    Vector3 _target;
    Vector3 ROTtarget = new Vector3(0,0,180);

    private bool AvanceTermmine, TourneTermine, NouvDirection;

    int d, c;
    

   
    // Start is called before the first frame update
    void Start()
    {
        _target= transform.position;

    }
    public void PartageDonnées(float Donné, int x)
    {
        _UniteMouvement[x] = Donné;
    }

    //Update is called once per frame
    void Update()
    {



        if (transform.eulerAngles == ROTtarget && transform.position == _target) { 



            _VecteurX = 5 * Mathf.Cos(Mathf.Deg2Rad * (transform.eulerAngles.z));
            _VecteurY = 5 * Mathf.Sin(Mathf.Deg2Rad * (transform.eulerAngles.z));




            Vector2 dir = new Vector3(_VecteurX + transform.position.x, _VecteurY + transform.position.y);


            _VecteurDroiteX = 5 * Mathf.Cos(Mathf.Deg2Rad * (transform.eulerAngles.z - 90));
            _VecteurDroiteY = 5 * Mathf.Sin(Mathf.Deg2Rad * (transform.eulerAngles.z - 90));






            Vector2 dirDroite = new Vector3(_VecteurDroiteX + transform.position.x, _VecteurDroiteY + transform.position.y);


            Debug.DrawLine(transform.position, dir, Color.red);
            Debug.DrawLine(transform.position, dirDroite, Color.green);


            RaycastHit2D hitDevant = Physics2D.Raycast(this.transform.position, transform.TransformDirection(Vector2.right), _UniteMouvement[2], LayerMask.GetMask("DetectionMur"));
            RaycastHit2D hitDroite = Physics2D.Raycast(this.transform.position, transform.TransformDirection(Vector2.down), _UniteMouvement[3], LayerMask.GetMask("DetectionMur"));



            if (hitDevant == false && hitDroite == false)
            {
                //dir = -1
                //confirm =1

                CalculAngle(-1);
                CalculDirection();


                StartCoroutine(Move(1));
            }
            if (hitDevant == false && hitDroite == true)
            {
                //dir = 0
                //confirm =1

                CalculDirection();
                StartCoroutine(Move(1));
            }
            if (hitDevant == true && hitDroite == false)
            {
                //dir = -1
                //confirm =1 

                CalculAngle(-1);
                CalculDirection();
                StartCoroutine(Move(1));
            }
            if (hitDevant == true && hitDroite == true)
            {
                //dir = 1
                //confirm == 0;
                CalculAngle(1);
                StartCoroutine(Move(0));
            }
        }
    }






    IEnumerator Move(int confirm)
    {
        while (transform.eulerAngles != ROTtarget)
        {
            Tourne();
            yield return null;
        }
        while (transform.position != _target)
        {
            avance(confirm);
            yield return null;
        }
        yield break;
    }














    void Tourne()
    {






        if (transform.eulerAngles.z > ROTtarget.z+1)
        {


            transform.eulerAngles = Vector3.Lerp(transform.rotation.eulerAngles, ROTtarget, Time.deltaTime * _rotationSpeed);

        }
        else
        {
            transform.eulerAngles = ROTtarget;
        }







    }

    void avance(int moveCondition)
    {
        if(moveCondition == 1) { 
            if (transform.position.x > _target.x && transform.position.y > _target.y)
            {


                transform.position = Vector3.Lerp(transform.position, _target, Time.deltaTime * _VitesseMarche);

            }
            else
            {
                transform.position = _target;
            }
        }
    }
    float mod(float x, float m)
    {
        return (x % m + m) % m;
    }
    
    void CalculAngle(int dir)
    {
        _angleT = transform.eulerAngles.z + (dir* 90);
        _angleT = mod(_angleT, 360);


        Vector3 rotationTarget = new Vector3(0, 0, _angleT);
        ROTtarget = rotationTarget;
             
    }
    void CalculDirection()
    {
        
            int angle = (int)mod(this.transform.eulerAngles.z, 360);

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

            Vector3 target = new Vector3(transform.position.x + (_UniteMouvement[0] * i), transform.position.y + (_UniteMouvement[1] * y), 0);
            _target = target;
            

    }
}


