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

    private float _VecteurGaucheX;
    private float _VecteurGaucheY;

    
    

    // Start is called before the first frame update
    void Start()
    {


    }
    public void PartageDonnées(float Donné, int x)
    {
        _UniteMouvement[x] = Donné;
    }

    //Update is called once per frame
    void Update()
    {
        

        _VecteurX = 5 * Mathf.Cos(Mathf.Deg2Rad * transform.localRotation.eulerAngles.z);
        _VecteurY = 5 * Mathf.Sin(Mathf.Deg2Rad *transform.localRotation.eulerAngles.z);

        _VecteurX += _geant.transform.position.x;
        _VecteurY += _geant.transform.position.y;

       
        Vector3 dir = new Vector3(_VecteurX, _VecteurY , 0);


        _VecteurGaucheX = 5 * Mathf.Cos(Mathf.Deg2Rad * (transform.localRotation.eulerAngles.z-90f));
        _VecteurGaucheY = 5 * Mathf.Sin(Mathf.Deg2Rad * (transform.localRotation.eulerAngles.z-90f));

        _VecteurGaucheX += _geant.transform.position.x;
        _VecteurGaucheY += _geant.transform.position.y;


        Vector3 dirGauche = new Vector3(_VecteurGaucheX, _VecteurGaucheY, 0);

        RaycastHit2D hitDevant = Physics2D.Raycast(transform.position, dir, _UniteMouvement[3], LayerMask.GetMask("DetectionMur"));
        RaycastHit2D hitGauche = Physics2D.Raycast(transform.position, dirGauche, _UniteMouvement[3], LayerMask.GetMask("DetectionMur"));

        Debug.DrawLine(transform.position, dir, Color.red);
        Debug.DrawLine(transform.position, dirGauche, Color.green);

        if(hitDevant == false && hitGauche == false)
        {
            StartCoroutine(Bouge(1, 1));
        }
        if (hitDevant == false && hitGauche == true)
        {
            StartCoroutine(Bouge(0, 1));
        }
        if (hitDevant == true && hitGauche == false)
        {
            StartCoroutine(Bouge(0, -1));
        }
        if (hitDevant == true && hitGauche == true)
        {
            StartCoroutine(Bouge(0, -1));
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
        Vector3 rotationTarget = new Vector3(0, 0, transform.localRotation.z + (dir * 90f));
       
        
        do
        {
            
            
           transform.Rotate(new Vector3(0,0,_rotationSpeed *dir ) * Time.deltaTime);
           
           
        } while(transform.rotation.eulerAngles.z != rotationTarget.z);
        
    }
    void move(int MovingConfirm)
    {
        if (MovingConfirm == 1)
        {
            int i = 0;
            int y = 0;

            if (_VecteurX < 0) { i = -1; }
            else { i = 1; }

            if (_VecteurY < 0) { y = -1; }
            else { y = 1; }
            Vector3 target = new Vector3(transform.position.x + (_UniteMouvement[0] * i), transform.position.y + (_UniteMouvement[1] * y), 0);
            do
            {
                transform.Translate(Vector2.right * _VitesseMarche * Time.deltaTime, Space.Self);

            } while (transform.position.x != target.x && transform.position.y != target.y);
        }
    }
}


