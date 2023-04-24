//using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
<<<<<<< HEAD
//using System.Data;
//using TMPro;
//using Unity.Mathematics;
//using Unity.VisualScripting;
//using Unity.VisualScripting.Antlr3.Runtime.Tree;
//using UnityEditor;
using UnityEngine;
//using UnityEngine.UIElements;
//using static System.Net.WebRequestMethods;
//using static UnityEngine.GraphicsBuffer;
=======
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
>>>>>>> parent of 88a1ab1 (tentative de correction du geant)

public class Geant : MonoBehaviour
{

<<<<<<< HEAD
//    [SerializeField] private float _VitesseMarche;
//    [SerializeField] private float _VitesseCourse;
//    [SerializeField] private float _distanceVision;
//    [SerializeField] private float _rotationSpeed;
//    [SerializeField] private GameObject _yeux;
//    [SerializeField] private GameObject _geant;


//    private float[] _UniteMouvement = new float[4];
=======
    [SerializeField] private float _VitesseMarche;
    [SerializeField] private float _VitesseCourse;
    [SerializeField] private float _distanceVision;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private GameObject _yeux;
    [SerializeField] private GameObject _geant;


    private float[] _UniteMouvement = new float[4];

>>>>>>> parent of 88a1ab1 (tentative de correction du geant)


<<<<<<< HEAD
//    private float _VecteurX;
//    private float _VecteurY;

//    private float _VecteurDroiteX;
//    private float _VecteurDroiteY;



=======
    private float _VecteurDroiteX;
    private float _VecteurDroiteY;




    Coroutine _confirmMovement;
    // Start is called before the first frame update
    void Awake()
    {

>>>>>>> parent of 88a1ab1 (tentative de correction du geant)

//    Coroutine _confirmMovement;
//    // Start is called before the first frame update
//    void Awake()
//    {


//    }
    public void PartageDonnées(float Donné, int x)
   {
//        _UniteMouvement[x] = Donné;
    }

//    //Update is called once per frame
//    void Update()
//    {




<<<<<<< HEAD
//        _VecteurX = 5 * Mathf.Cos(Mathf.Deg2Rad * (transform.eulerAngles.z));
//        _VecteurY = 5 * Mathf.Sin(Mathf.Deg2Rad * (transform.eulerAngles.z));


=======
        _VecteurX = 5 * Mathf.Cos(Mathf.Deg2Rad * (transform.eulerAngles.z));
        _VecteurY = 5 * Mathf.Sin(Mathf.Deg2Rad * (transform.eulerAngles.z));
>>>>>>> parent of 88a1ab1 (tentative de correction du geant)


//        Vector2 dir = new Vector3(_VecteurX + transform.position.x, _VecteurY + transform.position.y);


<<<<<<< HEAD
//        _VecteurDroiteX = 5 * Mathf.Cos(Mathf.Deg2Rad * (transform.eulerAngles.z - 90));
//        _VecteurDroiteY = 5 * Mathf.Sin(Mathf.Deg2Rad * (transform.eulerAngles.z - 90));


=======
        Vector2 dir = new Vector3(_VecteurX + transform.position.x, _VecteurY + transform.position.y);


        _VecteurDroiteX = 5 * Mathf.Cos(Mathf.Deg2Rad * (transform.eulerAngles.z - 90));
        _VecteurDroiteY = 5 * Mathf.Sin(Mathf.Deg2Rad * (transform.eulerAngles.z - 90));
>>>>>>> parent of 88a1ab1 (tentative de correction du geant)




//        Vector2 dirDroite = new Vector3(_VecteurDroiteX + transform.position.x, _VecteurDroiteY + transform.position.y);


<<<<<<< HEAD
//        Debug.DrawLine(transform.position, dir, Color.red);
//        Debug.DrawLine(transform.position, dirDroite, Color.green);


//        RaycastHit2D hitDevant = Physics2D.Raycast(this.transform.position, transform.TransformDirection(Vector2.right), _UniteMouvement[2], LayerMask.GetMask("DetectionMur"));
//        RaycastHit2D hitDroite = Physics2D.Raycast(this.transform.position, transform.TransformDirection(Vector2.down), _UniteMouvement[3], LayerMask.GetMask("DetectionMur"));
=======
        Vector2 dirDroite = new Vector3(_VecteurDroiteX + transform.position.x, _VecteurDroiteY + transform.position.y);


        Debug.DrawLine(transform.position, dir, Color.red);
        Debug.DrawLine(transform.position, dirDroite, Color.green);
>>>>>>> parent of 88a1ab1 (tentative de correction du geant)

//        StopAllCoroutines();

<<<<<<< HEAD
//        if (hitDevant == false && hitDroite == false)
//        {
//            //dir = -1
//            //confirm =1
//            StopAllCoroutines();
//            StartCoroutine(Bouge(-1, 1));

//        }
//        if (hitDevant == false && hitDroite == true)
//        {
//            //dir = 0
//            //confirm =1
//            StopAllCoroutines();
//            StartCoroutine(Bouge(0, 1));
//        }
//        if (hitDevant == true && hitDroite == false)
//        {
//            //dir = -1
//            //confirm =1
//            StopAllCoroutines();
//            StartCoroutine(Bouge(-1, 1));
//        }
//        if (hitDevant == true && hitDroite == true)
//        {
//            //dir = 1
            
//            StopAllCoroutines();
//            StartCoroutine(Bouge(1, 0));
//        }
=======
        RaycastHit2D hitDevant = Physics2D.Raycast(this.transform.position, transform.TransformDirection(Vector2.right), _UniteMouvement[2], LayerMask.GetMask("DetectionMur"));
        RaycastHit2D hitDroite = Physics2D.Raycast(this.transform.position, transform.TransformDirection(Vector2.down), _UniteMouvement[3], LayerMask.GetMask("DetectionMur"));

        StopAllCoroutines();

        if (hitDevant == false && hitDroite == false)
        {
            //dir = -1
            //confirm =1
            StopAllCoroutines();
            StartCoroutine(Bouge(-1, 1));

        }
        if (hitDevant == false && hitDroite == true)
        {
            //dir = 0
            //confirm =1
            StopAllCoroutines();
            StartCoroutine(Bouge(0, 1));
        }
        if (hitDevant == true && hitDroite == false)
        {
            //dir = -1
            //confirm =1
            StopAllCoroutines();
            StartCoroutine(Bouge(-1, 1));
        }
        if (hitDevant == true && hitDroite == true)
        {
            //dir = 1
            
            StopAllCoroutines();
            StartCoroutine(Bouge(1, 0));
        }



    }
    IEnumerator Bouge(int dir, int Confirm)
    {
        yield return StartCoroutine(Rotate(dir));

        yield return StartCoroutine(avance(Confirm));

        yield break;
    }
    //IEnumerator Rotate(int dir)
    //{

    //    float angleT = transform.eulerAngles.z + (dir * 90);
    //    angleT = angleT % 360; 
    //    Quaternion rotationTarget = new Quaternion(0, 0, angleT, 0);

    //    Quaternion rotationBase = new Quaternion(0, 0, transform.eulerAngles.z, 0);

    
    //    if (dir == 0)
    //    {
    //        yield break;
    //    }
    //    do
    //    {
    //        transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, _rotationSpeed * Time.deltaTime);

    //        if (transform.rotation.z == rotationBase.z)
    //        {
    //            yield break;
    //        }



    //    } while (transform.eulerAngles.z != rotationBase.z);




    //}

    IEnumerator avance(int moveCondition)
    {
        
        int angle = (int)(transform.eulerAngles.z % 360);
        int i = 0;
        int y = 0;

        if (moveCondition == 1)
        {


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
        }
        Vector3 target = new Vector3(transform.position.x + (_UniteMouvement[0] * i), transform.position.y + (_UniteMouvement[1] * y), 0);

        while (this.transform.rotation.z != target.z)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, target, _VitesseMarche * Time.deltaTime);
            yield return null;
        }
        this.transform.position = target;
        yield return null;


    }

    IEnumerator Rotate(float dir)
    {
        float angleT = transform.eulerAngles.z + (dir * 90);
        angleT = math.modf(angleT, 360);
        Quaternion rotationTarget = new Quaternion(0, 0, angleT, 0);

        Quaternion rotationBase = new Quaternion(0, 0, transform.eulerAngles.z, 0);
        while (this.transform.rotation.z != rotationTarget.z)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotationTarget, 3f * Time.deltaTime);
            yield return null;
        }
        this.transform.rotation = Quaternion.Euler(0f, 0f, angleT);
        yield return null;


>>>>>>> parent of 88a1ab1 (tentative de correction du geant)



//    }
//    IEnumerator Bouge(int dir, int Confirm)
//    {
//        yield return StartCoroutine(Rotate(dir));

//        yield return StartCoroutine(avance(Confirm));

//        yield break;
//    }
//    //IEnumerator Rotate(int dir)
//    //{

//    //    float angleT = transform.eulerAngles.z + (dir * 90);
//    //    angleT = angleT % 360; 
//    //    Quaternion rotationTarget = new Quaternion(0, 0, angleT, 0);

//    //    Quaternion rotationBase = new Quaternion(0, 0, transform.eulerAngles.z, 0);

    
//    //    if (dir == 0)
//    //    {
//    //        yield break;
//    //    }
//    //    do
//    //    {
//    //        transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, _rotationSpeed * Time.deltaTime);

//    //        if (transform.rotation.z == rotationBase.z)
//    //        {
//    //            yield break;
//    //        }



//    //    } while (transform.eulerAngles.z != rotationBase.z);




//    //}

//    IEnumerator avance(int moveCondition)
//    {
        
//        int angle = (int)(transform.eulerAngles.z % 360);
//        int i = 0;
//        int y = 0;

//        if (moveCondition == 1)
//        {


//            if (angle == 180)
//            {
//                i = -1;
//            }
//            else if (angle == 0)
//            {
//                i = 1;
//            }
//            else
//            {
//                i = 0;
//            }

//            if (angle == 270)
//            {
//                y = -1;
//            }
//            else if (angle == 90)
//            {
//                y = 1;
//            }
//            else
//            {
//                y = 0;
//            }
//        }
//        Vector3 target = new Vector3(transform.position.x + (_UniteMouvement[0] * i), transform.position.y + (_UniteMouvement[1] * y), 0);

//        while (this.transform.rotation.z != target.z)
//        {
//            this.transform.position = Vector3.Lerp(this.transform.position, target, _VitesseMarche * Time.deltaTime);
//            yield return null;
//        }
//        this.transform.position = target;
//        yield return null;


//    }

//    IEnumerator Rotate(float dir)
//    {
//        float angleT = transform.eulerAngles.z + (dir * 90);
//        angleT = math.modf(angleT, 360);
//        Quaternion rotationTarget = new Quaternion(0, 0, angleT, 0);

//        Quaternion rotationBase = new Quaternion(0, 0, transform.eulerAngles.z, 0);
//        while (this.transform.rotation.z != rotationTarget.z)
//        {
//            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotationTarget, 3f * Time.deltaTime);
//            yield return null;
//        }
//        this.transform.rotation = Quaternion.Euler(0f, 0f, angleT);
//        yield return null;



//    }
}


