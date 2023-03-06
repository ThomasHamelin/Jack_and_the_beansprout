using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using UnityEditor;
using UnityEditor.TextCore.Text;
using UnityEngine;



using Random = UnityEngine.Random;



/*
 * Allo ma belle,
 * j'ai commenté tout mon code pour que tu puisse 
 * travailler dessus.
 * ça se pourrait que il y a des bout moins clairs faque
 * hésite pas à me poser des question si t'en à
 */




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

        //placement des coins principaux selon la taille voulue
        coin1.transform.position = new Vector2(CoordonneDepartX, CoordonneDepartY);
        coin2.transform.position = new Vector2(coin1.transform.position.x + longueur, coin1.transform.position.y);
        coin3.transform.position = new Vector2(coin1.transform.position.x, coin1.transform.position.y + largeur);
        coin4.transform.position = new Vector2(coin1.transform.position.x + longueur, coin1.transform.position.y + largeur);

       

        //calcul de la distance verticale et horizontale entre les coins des murs
        UniteDeDistance[0] = longueur / tailleGrille;
        UniteDeDistance[1] = largeur / tailleGrille;


        //calcul de la distance verticale et horizontale des murs pour qu'il soient entre les coins des murs
        UniteDeDistance[2] = (longueur / tailleGrille) / 2;
        UniteDeDistance[3] = (largeur / tailleGrille) / 2;



        //génération des coins des murs
        for (int y = 0; y <= tailleGrille; y++)
        {

            for (int i = 0; i <= tailleGrille; i++)
            {
                Vector3 position = new Vector3(UniteDeDistance[0] * i, UniteDeDistance[1] * y, 0);
                Instantiate(CoinMur, position, transform.rotation);



            }
        }

        //génération des murs horizontaux
        for (int y = 0; y <= (tailleGrille + 0.5f); y++)
        {

            for (int i = 0; i < tailleGrille; i++)
            {



                Vector3 position2 = new Vector3((UniteDeDistance[0] * (i + 1) - UniteDeDistance[2]), UniteDeDistance[1] * y, 0);
                GameObject MurHorizontaux = Instantiate(Mur, position2, transform.rotation);
                MurHorizontaux.transform.localScale = new Vector3(UniteDeDistance[0], 1, 0);
            }
        }

        // génération des murs Verticaux
        for (int y = 0; y <= tailleGrille; y++)
        {

            for (int i = 0; i < (tailleGrille); i++)
            {



                Vector3 position3 = new Vector3(UniteDeDistance[0] * y, (UniteDeDistance[1] * (i + 1) - UniteDeDistance[3]), 0);
                GameObject MurVerticaux = Instantiate(Mur, position3, transform.rotation);
                MurVerticaux.transform.localScale = new Vector3(1, UniteDeDistance[1], 0);
            }
        }


        //instruction pour que la balle rouge(la balle inteligente) soit dans le carré du bas à gauche du labyrinthe
        transform.position = new Vector3(UniteDeDistance[2], UniteDeDistance[3], 0);





    }

    void Update()
    {




    
     
    
        
        //rénitialisaton des booléen qui affirme la validité de la direction
        haut = true;
        bas = true;
        droite = true;
        gauche = true;

        //ce booléen permet de confirmer que la sphère intelligente à bel et bien bouger à la case où la balle verte(la balle visionnaire) est
        confirmationDirection = false;

        //Yeux balle représente la balle verte(la balle visionaire) dans le code
        //ici, on s'assure que Yeux Balle est bel et bien à la même position que la balle rouge
        YeuxBalle.transform.position = new Vector3(transform.position.x, transform.position.y, 0);


        //boucle principale
        do
        {
            //on détermine la prochaine direcion
            direction = Random.Range(1, 5);


            
             //TODO: rajouter le bloc de mémoire et de backtracking après que Daphnée aura réussi
            
             
             
             

            //BLOC TEMPORAIRE QUI ARRÊTE LA BALLE ROUGE POUR ÉVITER UNE BOUCLE INFINIE (UNITY PLANTE SINON)

            if (haut = true || bas == true || droite == true || gauche == true)
            {
                //Switch qui performe l'action nécéssaire selon la direction
                switch (direction)
                {
                    case 1:
                        //si la direction du haut est valide; fait ceci
                        if (haut == true)
                        {
                            //on demande à la balle verte de se placer dans la case en haut de la balle rouge
                            YeuxBalle.transform.position = new Vector3(transform.position.x, transform.position.y + UniteDeDistance[1], 0);

                            //on vérifie si la balle verte est toujours dans le labyrinthe
                            if (YeuxBalle.transform.position.y < coin3.transform.position.y)
                            {
                                //on déploie un raycast dans toute les direction pour voir si la case ne fait pas partie du chemin
                                RaycastHit2D hitup = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.up, UniteDeDistance[3]);
                                RaycastHit2D hitdown = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.down, UniteDeDistance[3]);
                                RaycastHit2D hitleft = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.left, UniteDeDistance[2]);
                                RaycastHit2D hitright = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.right, UniteDeDistance[2]);

                                //si les raycasts confirment la présence des murs, alors continue
                                if (hitup == true && hitdown == true && hitleft == true && hitright == true)
                                {


                                    //la destruction du mur du haut pourrait se faire ici

                                    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, UniteDeDistance[3], LayerMask.GetMask("DetectionMur"));
                                    Destroy(hit.collider.gameObject);




                                    //instruction qui indique que la balle rouge avance vers la balle verte jusqu'à ce qu'elle atteigne sa position
                                    while (this.gameObject.transform.position != YeuxBalle.transform.position)
                                    {
                                        transform.position = Vector3.MoveTowards(transform.position, YeuxBalle.transform.position, (0.1f * Time.deltaTime));

                                    }
                                    //on confirme que le mouvement à été fait
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
                                RaycastHit2D hitup = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.up, UniteDeDistance[3]);
                                RaycastHit2D hitdown = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.down, UniteDeDistance[3]);
                                RaycastHit2D hitleft = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.left, UniteDeDistance[2]);
                                RaycastHit2D hitright = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.right, UniteDeDistance[2]);

                                if (hitup == true && hitdown == true && hitleft == true && hitright == true)
                                {
                                    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, UniteDeDistance[3], LayerMask.GetMask("DetectionMur"));
                                    Destroy(hit.collider.gameObject);
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
                                RaycastHit2D hitup = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.up, UniteDeDistance[3]);
                                RaycastHit2D hitdown = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.down, UniteDeDistance[3]);
                                RaycastHit2D hitleft = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.left, UniteDeDistance[2]);
                                RaycastHit2D hitright = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.right, UniteDeDistance[2]);

                                if (hitup == true && hitdown == true && hitleft == true && hitright == true)
                                {

                                    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, UniteDeDistance[2], LayerMask.GetMask("DetectionMur"));
                                    Destroy(hit.collider.gameObject);

                                    while (transform.position != YeuxBalle.transform.position)
                                    {
                                        transform.position = Vector3.MoveTowards(transform.position, YeuxBalle.transform.position, (0.1f * Time.deltaTime));

                                    }
                                    confirmationDirection = true;
                                }
                                else
                                {

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
                                RaycastHit2D hitup = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.up, UniteDeDistance[3]);
                                RaycastHit2D hitdown = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.down, UniteDeDistance[3]);
                                RaycastHit2D hitleft = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.left, UniteDeDistance[2]);
                                RaycastHit2D hitright = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.right, UniteDeDistance[2]);

                                if (hitup == true && hitdown == true && hitleft == true && hitright == true)
                                {
                                    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, UniteDeDistance[2], LayerMask.GetMask("DetectionMur"));
                                    Destroy(hit.collider.gameObject);

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
                                droite = false;
                            }
                        }
                        break;

                }

            }
            else if (haut = false && bas == false && droite == false && gauche == false) {

                Destroy(this.gameObject);
                Debug.Log("Fin");
                confirmationDirection = true;
             }


        

        } while (confirmationDirection != true);
        
        
    }
    
    


}