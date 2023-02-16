using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class CréationLabyrinte : MonoBehaviour
{
    [SerializeField] GameObject coin1;
    [SerializeField] GameObject coin2;
    [SerializeField] GameObject coin3;
    [SerializeField] GameObject coin4;

    [SerializeField] GameObject CoinMur;
    [SerializeField] GameObject Mur;

    [SerializeField] float longueur;
    [SerializeField] float largeur;

    [SerializeField] float CoordonneDepartX;
    [SerializeField] float CoordonneDepartY;

    [SerializeField] float tailleGrille;

    private float[] UniteDeDistance = new float[4];
    private int x;

    private void Start()
    {
      
        //placement des coins selon la taille voulue
        coin1.transform.position = new Vector2(CoordonneDepartX, CoordonneDepartY);
        coin2.transform.position = new Vector2(coin1.transform.position.x + longueur, coin1.transform.position.y);
        coin3.transform.position = new Vector2(coin1.transform.position.x, coin1.transform.position.y + largeur);
        coin4.transform.position = new Vector2(coin1.transform.position.x + longueur, coin1.transform.position.y + largeur);


        UniteDeDistance[0] = longueur / tailleGrille;
        UniteDeDistance[1] = largeur / tailleGrille;

        UniteDeDistance[2] = (longueur / tailleGrille)/2;
        UniteDeDistance[3] = (largeur / tailleGrille)/2;

        //coins de murs
        for (int y = 0; y <= tailleGrille; y++)
        {
            
            for (int i = 0; i <= tailleGrille; i++) {
                    Vector3 position = new Vector3(UniteDeDistance[0]*i, UniteDeDistance[1]*y,0);
                    Instantiate(CoinMur,position, transform.rotation);


                    
            }
        }
        //murs horizautaux
        for (int y = 0; y <= (tailleGrille + 0.5f); y++)
        {

            for (int i = 0; i < tailleGrille; i++)
            {
              


                Vector3 position2 = new Vector3((UniteDeDistance[0] * (i + 1) - UniteDeDistance[2]), UniteDeDistance[1] * y, 0);
                GameObject MurHorizontaux = Instantiate(Mur, position2, transform.rotation);
                MurHorizontaux.transform.localScale = new Vector3(UniteDeDistance[0], 1, 0);
            }
        }

        //murs Verticaux
        for (int y = 0; y <= tailleGrille; y++)
        {

            for (int i = 0; i < (tailleGrille) ; i++)
            {



                Vector3 position3 = new Vector3(UniteDeDistance[0] * y,(UniteDeDistance[1] * (i+1)  - UniteDeDistance[3]),  0);
                GameObject MurVerticaux = Instantiate(Mur, position3, transform.rotation);
                MurVerticaux.transform.localScale = new Vector3(1, UniteDeDistance[1], 0);
            }
        }



        transform.position = new Vector3(UniteDeDistance[2], UniteDeDistance[3], 0);
    }

    void Update()
    {
      
       
        
    }

}