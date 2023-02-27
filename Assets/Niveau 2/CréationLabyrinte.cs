using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.TextCore.Text;
using UnityEngine;



using Random = UnityEngine.Random;

public class CréationLabyrinte : MonoBehaviour
{
    [SerializeField] GameObject YeuxBalle; 

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
    
    
    private float direction;
 
    private bool haut = true, bas = true, gauche = true, droite = true;
    private bool confirmationDirection = false;
    void Start()
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
        haut = true;
        bas = true;
        droite = true;
        gauche = true;

        confirmationDirection = false;

        YeuxBalle.transform.position = new Vector3(transform.position.x, transform.position.y, 0);


        do
        {
            direction = Random.Range(1, 5);

            switch (direction)
            {
                case 1:
                    if (haut == true)
                    {
                        YeuxBalle.transform.position = new Vector3(transform.position.x, transform.position.y + UniteDeDistance[1], 0);
                        if (YeuxBalle.transform.position.y < coin3.transform.position.y)
                        {
                            RaycastHit2D hitup = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.up);
                            RaycastHit2D hitdown = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.down);
                            RaycastHit2D hitleft = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.left);
                            RaycastHit2D hitright = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.right);

                            if (hitup == true && hitdown == true && hitleft == true && hitright == true)
                            {
                                var ray = new Ray(this.transform.position, this.transform.up);
                                RaycastHit hit;
                                if (Physics.Raycast(ray, out hit))
                                {
                                    GameObject objecctHit = hit.transform.gameObject;
                                    Destroy(objecctHit);
                                }
                                


                                while (this.gameObject.transform.position != YeuxBalle.transform.position)
                                {
                                    transform.position = Vector3.MoveTowards(transform.position, YeuxBalle.transform.position, (0.1f * Time.deltaTime));
                                    
                                }
                                confirmationDirection = true;

                            }
                            else
                            {
                                haut = false;
                            }
                        }
                        else
                        {
                            haut = false;
                        }
                    }
                    break;


                case 2:
                    if (bas == true)
                    {
                        YeuxBalle.transform.position = new Vector3(transform.position.x, transform.position.y - UniteDeDistance[1], 0);
                        if (YeuxBalle.transform.position.y > coin1.transform.position.y)
                        {
                            RaycastHit2D hitup = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.up);
                            RaycastHit2D hitdown = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.down);
                            RaycastHit2D hitleft = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.left);
                            RaycastHit2D hitright = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.right);

                            if (hitup == true && hitdown == true && hitleft == true && hitright == true)
                            {
                                while (transform.position != YeuxBalle.transform.position)
                                {
                                    transform.position = Vector3.MoveTowards(transform.position, YeuxBalle.transform.position, (0.1f * Time.deltaTime));
                                    
                                }
                                confirmationDirection = true;
                            }
                            else
                            {
                                bas = false;
                            }
                        }
                        else
                        {
                            bas = false;
                        }

                    }
                    break;


                case 3:
                    if (gauche == true)
                    {
                        YeuxBalle.transform.position = new Vector3(transform.position.x - UniteDeDistance[0], transform.position.y, 0);
                        if (YeuxBalle.transform.position.x > coin1.transform.position.x)
                        {
                            RaycastHit2D hitup = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.up);
                            RaycastHit2D hitdown = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.down);
                            RaycastHit2D hitleft = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.left);
                            RaycastHit2D hitright = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.right);

                            if (hitup == true && hitdown == true && hitleft == true && hitright == true)
                            {
                                while (transform.position != YeuxBalle.transform.position)
                                {
                                    transform.position = Vector3.MoveTowards(transform.position, YeuxBalle.transform.position, (0.1f * Time.deltaTime));
                                    
                                }
                                confirmationDirection = true;
                            }
                            else {

                                gauche = false;
                            }
                        }
                        else
                        {
                            gauche = false;
                        }
                    }
                    break;


                case 4:
                    if (droite == true)
                    {
                        YeuxBalle.transform.position = new Vector3(transform.position.x + UniteDeDistance[0], transform.position.y, 0);
                        if (YeuxBalle.transform.position.x < coin2.transform.position.x)
                        {
                            RaycastHit2D hitup = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.up);
                            RaycastHit2D hitdown = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.down);
                            RaycastHit2D hitleft = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.left);
                            RaycastHit2D hitright = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.right);

                            if (hitup == true && hitdown == true && hitleft == true && hitright == true)
                            {
                                while (transform.position != YeuxBalle.transform.position)
                                {
                                    transform.position = Vector3.MoveTowards(transform.position, YeuxBalle.transform.position, (0.1f * Time.deltaTime));
                                   
                                }
                                confirmationDirection = true;
                            }
                            else
                            {
                                droite = false;
                            }
                        }
                        else
                        {
                            droite= false;  
                        }
                    }
                    break;

            }

        } while (confirmationDirection != true);
    }
}