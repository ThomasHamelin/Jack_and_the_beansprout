using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static System.Net.WebRequestMethods;

public class Geant : MonoBehaviour
{

    [SerializeField] private float _VitesseMarche;
    [SerializeField] private float _VitesseCourse;
    [SerializeField] private float _distanceVision;
    [SerializeField] private float _rotationSpeed;
    private Rigidbody2D _rb;
  
    private int _newPos;
    private float[] _UniteMouvement = new float[4];
    private int _ancienneposition;
    

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
        // 0 = up, 1 = down , 2 = left, 3 = right
        _newPos = Random.Range(0, 4);
        RaycastHit2D hitup = Physics2D.Raycast(transform.position, Vector2.up, _UniteMouvement[3], LayerMask.GetMask("DetectionMur"));
        RaycastHit2D hitdown = Physics2D.Raycast(transform.position, Vector2.down, _UniteMouvement[3], LayerMask.GetMask("DetectionMur"));
        RaycastHit2D hitleft = Physics2D.Raycast(transform.position, Vector2.left, _UniteMouvement[2], LayerMask.GetMask("DetectionMur"));
        RaycastHit2D hitright = Physics2D.Raycast(transform.position, Vector2.right, _UniteMouvement[2], LayerMask.GetMask("DetectionMur"));

        switch (_newPos)
        {
            case 0:
                if(_ancienneposition != 1)
                {
                    if(hitup == false)
                    {
                        if(hitleft != true)
                        {
                            _ancienneposition = 2;
                        }
                        if(hitright != true)
                        {
                            _ancienneposition = 3;
                        }
                        else
                        {
                            _ancienneposition = 0;
                        }
                    }
                    else
                    {
                        _ancienneposition = 1;
                    }
                }
                break;
            case 1:
                if (_ancienneposition != 0)
                {
                    if (hitdown == false)
                    {
                        if (hitleft != true)
                        {
                            _ancienneposition = 2;
                        }
                        if (hitright != true)
                        {
                            _ancienneposition = 3;
                        }
                        else
                        {
                            _ancienneposition = 1;
                        }
                    }
                    else
                    {
                        _ancienneposition = 0;
                    }
                }
                break;
            case 2:
                if (_ancienneposition != 3)
                {
                    if (hitleft == false)
                    {
                        if (hitup != true)
                        {
                            _ancienneposition = 0;
                        }
                        if (hitdown != true)
                        {
                            _ancienneposition = 1;
                        }
                        else
                        {
                            _ancienneposition = 2;
                        }
                    }
                    else
                    {
                        _ancienneposition = 3;
                    }
                }
                break;
            case 3:
                if (_ancienneposition != 2)
                {
                    if (hitright == false)
                    {
                        if (hitdown != true)
                        {
                            _ancienneposition = 1;
                        }
                        if (hitup != true)
                        {
                            _ancienneposition = 0;
                        }
                        else
                        {
                            _ancienneposition = 3;
                        }
                    }
                    else
                    {
                        _ancienneposition = 2;
                    }
                }
                break;


        }


    }


}


