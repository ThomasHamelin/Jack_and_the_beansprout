using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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

    private void Start()
    {
      
        
        coin1.transform.position = new Vector2(CoordonneDepartX, CoordonneDepartY);
        coin2.transform.position = new Vector2(coin1.transform.position.x + (longueur * tailleGrille), coin1.transform.position.y);
        coin3.transform.position = new Vector2(coin1.transform.position.x, coin1.transform.position.y + (largeur * tailleGrille));
        coin4.transform.position = new Vector2(coin1.transform.position.x + (longueur * tailleGrille), coin1.transform.position.y + (largeur * tailleGrille));

        for (int i = 0; i <= tailleGrille; i++) {
            Vector3 position = new Vector3(((longueur * tailleGrille)/tailleGrille) * i, 0,0);
            Instantiate(mur,position, transform.rotation);
        }

    }

    void Update()
    {
      
       
        
    }

}