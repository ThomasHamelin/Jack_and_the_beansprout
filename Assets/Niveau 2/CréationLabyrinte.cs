using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class CréationLabyrinte : MonoBehaviour
{
    [SerializeField] GameObject coin1;
    [SerializeField] GameObject coin2;
    [SerializeField] GameObject coin3;
    [SerializeField] GameObject coin4;

    [SerializeField] GameObject mur;


    [SerializeField] float longueur;
    [SerializeField] float largeur;

    [SerializeField] float CoordonneDepartX;
    [SerializeField] float CoordonneDepartY;

    [SerializeField] float tailleGrille;

    private float[] UniteDeDistance = new float[2];
    private int x;

    private void Start()
    {
      
        
        coin1.transform.position = new Vector2(CoordonneDepartX, CoordonneDepartY);
        coin2.transform.position = new Vector2(coin1.transform.position.x + longueur, coin1.transform.position.y);
        coin3.transform.position = new Vector2(coin1.transform.position.x, coin1.transform.position.y + largeur);
        coin4.transform.position = new Vector2(coin1.transform.position.x + longueur, coin1.transform.position.y + largeur);


        UniteDeDistance[0] = longueur / tailleGrille;
        UniteDeDistance[1] = largeur / tailleGrille;



        for (int y = 0; y <= tailleGrille; y++)
        {
            
            for (int i = 0; i <= tailleGrille; i++) {
                    Vector3 position = new Vector3(UniteDeDistance[0]*i, UniteDeDistance[1]*y,0);
                    Instantiate(mur,position, transform.rotation);
                
            }
        }
       

    }

    void Update()
    {
      
       
        
    }

}