using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;
using static System.Net.WebRequestMethods;

public class Geant : MonoBehaviour
{

    [SerializeField] private float _VitesseMarche;
    [SerializeField] private float _VitesseCourse;
    [SerializeField] private float _distanceVision;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private GameObject _yeux;
    [SerializeField] private GameObject _geant;

    private int _rand;
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
        

        _VecteurX = 10 * Mathf.Cos(Mathf.Deg2Rad * transform.localRotation.eulerAngles.z);
        _VecteurY = 10 * Mathf.Sin(Mathf.Deg2Rad *transform.localRotation.eulerAngles.z);

        _VecteurX += _geant.transform.position.x;
        _VecteurY += _geant.transform.position.y;

        
        Vector3 dir = new Vector3(_VecteurX, _VecteurY , 0);


        _VecteurX = 10 * Mathf.Cos(Mathf.Deg2Rad * transform.localRotation.eulerAngles.z);
        _VecteurY = 10 * Mathf.Sin(Mathf.Deg2Rad * transform.localRotation.eulerAngles.z);

        _VecteurX += _geant.transform.position.x;
        _VecteurY += _geant.transform.position.y;


        Vector3 dirGauche = new Vector3(_VecteurX, _VecteurY, 0);

        RaycastHit2D hitDevant = Physics2D.Raycast(transform.position, dir, _UniteMouvement[3], LayerMask.GetMask("DetectionMur"));
        RaycastHit2D hitGauche = Physics2D.Raycast(transform.position, dirGauche, _UniteMouvement[3], LayerMask.GetMask("DetectionMur"));

        Debug.DrawLine(transform.position, dir, Color.red);
        Debug.DrawLine(transform.position, dirGauche, Color.blue);


    }
}


