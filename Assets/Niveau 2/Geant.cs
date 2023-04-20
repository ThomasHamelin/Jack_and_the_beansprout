using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;
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


    private bool _yes = false;

    // Start is called before the first frame update
    void Awake()
    {
        _yes = true;

    }
    public void PartageDonnées(float Donné, int x)
    {
        _UniteMouvement[x] = Donné;
    }

    //Update is called once per frame
    void Update()
    {

        if (_yes == true)
        {
            _VecteurX = 5 * Mathf.Cos(Mathf.Deg2Rad * (transform.eulerAngles.z));
            _VecteurY = 5 * Mathf.Sin(Mathf.Deg2Rad * (transform.eulerAngles.z));




            Vector2 dir = new Vector3(_VecteurX + transform.position.x, _VecteurY + transform.position.y);


            _VecteurDroiteX = 5 * Mathf.Cos(Mathf.Deg2Rad * (transform.eulerAngles.z-90));
            _VecteurDroiteY = 5 * Mathf.Sin(Mathf.Deg2Rad * (transform.eulerAngles.z-90));






            Vector2 dirDroite = new Vector3(_VecteurDroiteX + transform.position.x, _VecteurDroiteY + transform.position.y);


            Debug.DrawLine(transform.position, dir, Color.red);
            Debug.DrawLine(transform.position, dirDroite, Color.green);


            RaycastHit2D hitDevant = Physics2D.Raycast(this.transform.position, transform.TransformDirection(Vector2.right), _UniteMouvement[2], LayerMask.GetMask("DetectionMur"));
            RaycastHit2D hitDroite = Physics2D.Raycast(this.transform.position, transform.TransformDirection(Vector2.down), _UniteMouvement[3], LayerMask.GetMask("DetectionMur"));


            
            if (hitDevant == false && hitDroite == false)
            {
                //StartCoroutine(Bouge(1, -1));
                turn(-1);
                move(1);
            }
            if (hitDevant ==false && hitDroite == true)
            {
                //StartCoroutine(Bouge(1, 0));
                turn(0);
                move(1);
            }
            if (hitDevant == true && hitDroite == false)
            {
                //StartCoroutine(Bouge(0, 1));
                turn(-1);
                move(1);
            }
            if (hitDevant == true && hitDroite == true)
            {
                //StartCoroutine(Bouge(0, 1));
                turn(1);
                move(0);
            }
            
            
        }
    }
    IEnumerator Rotate(int dir)
    {
        turn(dir);
        yield return null;
    }
    IEnumerator Bouge(int dir, int moveCondition) 
    {
        yield return StartCoroutine(Rotate(dir));

        move(moveCondition);
        yield return null;
    }

    void turn(int dir)
    {
       
        
       //dir = 1: clockwise
       //dir = -1: anti-clockwise;
        Quaternion rotationTarget = new Quaternion(0, 0, transform.localEulerAngles.z + (dir * 90f),0);






        transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, _rotationSpeed * Time.deltaTime);



    }
    void move(int MovingConfirm)
    {

        int angle = (int)(transform.eulerAngles.z % 360);

        if (MovingConfirm == 1)
        {
            int i = 0;
            int y = 0;

            if (angle == 180)
            { 
                i = -1;
            }
            else if(angle == 0)
            {
                i = 1;
            }
            else { 
                i = 0; 
            }

            if (angle == 270)
            {
                y = -1; 
            }
            else if(angle == 90) 
            {
                y = 1; 
            }
            else
            {
                y = 0;
            }
            Vector3 target = new Vector3(transform.position.x + (_UniteMouvement[0] * i), transform.position.y + (_UniteMouvement[1] * y), 0);
            //do
            //{
            transform.position = new Vector3(target.x, target.y, 0);

            //} while (transform.position.x != target.x && transform.position.y != target.y);
        }
    }

}


