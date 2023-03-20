using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.TextCore.Text;
using UnityEditor.UIElements;
using UnityEngine;
using Random = UnityEngine.Random;






public class CréationLabyrinte : MonoBehaviour
{

    [SerializeField] GameObject Tresor;
    //[SerializeField] 

    [SerializeField] GameObject IntelliBalle;
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

    [SerializeField] float CoordonneSalleSpecialeX;
    [SerializeField] float CoordonneSalleSpecialeY;

    [SerializeField] float tailleGrille;



    private float[] UniteDeDistance = new float[4];


    private float direction;

    private bool haut = true, bas = true, gauche = true, droite = true;


    private bool confirmationDirection = false;

    private bool confirmationRecul;
    private bool confirmationNEWdir;

    private bool[] verifBackHaut = new bool[4];
    private bool[] verifBackBas = new bool[4];
    private bool[] verifBackGauche = new bool[4];
    private bool[] verifBackDroite = new bool[4];


    private List<int> MemoireACourtTerme = new List<int>();
    private List<int> MemoireALongTerme = new List<int>();

    private float nbCasestotales;
    private float nbCasesExplorés = 1;
    private int CurseurMemoire;


    private int xn, MAX;

    private int rand;
    void Start()
    {
        MemoireALongTerme.Add(0);
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

        nbCasestotales = (tailleGrille * tailleGrille);

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
        IntelliBalle.transform.position = new Vector3(UniteDeDistance[2], UniteDeDistance[3], 0);

        generationPrincipale();



    }



    private void GenerationTresor()
    {


    }
    


    private void generationPrincipale()
    {
        do
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
            YeuxBalle.transform.position = new Vector3(IntelliBalle.transform.position.x, IntelliBalle.transform.position.y, 0);


            //boucle principale
            do
            {
                //on détermine la prochaine direcion
                direction = Random.Range(1, 5);



                //TODO: rajouter le bloc de mémoire et de backtracking après que Daphnée aura réussi





                //BLOC TEMPORAIRE QUI ARRÊTE LA BALLE ROUGE POUR ÉVITER UNE BOUCLE INFINIE (UNITY PLANTE SINON)

                if (haut == true || bas == true || droite == true || gauche == true)
                {
                    //Switch qui performe l'action nécéssaire selon la direction
                    switch (direction)
                    {
                        case 1:
                            //si la direction du haut est valide; fait ceci
                            if (haut == true)
                            {
                                //on demande à la balle verte de se placer dans la case en haut de la balle rouge
                                YeuxBalle.transform.position = new Vector3(IntelliBalle.transform.position.x, IntelliBalle.transform.position.y + UniteDeDistance[1], 0);

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

                                        RaycastHit2D hit = Physics2D.Raycast(IntelliBalle.transform.position, Vector2.up, UniteDeDistance[3], LayerMask.GetMask("DetectionMur"));
                                        Destroy(hit.collider.gameObject);


                                        IntelliBalle.transform.position = new Vector3(YeuxBalle.transform.position.x, YeuxBalle.transform.position.y, 0);
                                        MemoireALongTerme.Add(1);
                                        nbCasesExplorés++;
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
                                YeuxBalle.transform.position = new Vector3(IntelliBalle.transform.position.x, IntelliBalle.transform.position.y - UniteDeDistance[1], 0);
                                if (YeuxBalle.transform.position.y > coin1.transform.position.y)
                                {
                                    RaycastHit2D hitup = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.up, UniteDeDistance[3]);
                                    RaycastHit2D hitdown = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.down, UniteDeDistance[3]);
                                    RaycastHit2D hitleft = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.left, UniteDeDistance[2]);
                                    RaycastHit2D hitright = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.right, UniteDeDistance[2]);

                                    if (hitup == true && hitdown == true && hitleft == true && hitright == true)
                                    {
                                        RaycastHit2D hit = Physics2D.Raycast(IntelliBalle.transform.position, Vector2.down, UniteDeDistance[3], LayerMask.GetMask("DetectionMur"));
                                        Destroy(hit.collider.gameObject);

                                        IntelliBalle.transform.position = new Vector3(YeuxBalle.transform.position.x, YeuxBalle.transform.position.y, 0);
                                        MemoireALongTerme.Add(2);
                                        nbCasesExplorés++;
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
                                YeuxBalle.transform.position = new Vector3(IntelliBalle.transform.position.x - UniteDeDistance[0], IntelliBalle.transform.position.y, 0);
                                if (YeuxBalle.transform.position.x > coin1.transform.position.x)
                                {
                                    RaycastHit2D hitup = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.up, UniteDeDistance[3]);
                                    RaycastHit2D hitdown = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.down, UniteDeDistance[3]);
                                    RaycastHit2D hitleft = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.left, UniteDeDistance[2]);
                                    RaycastHit2D hitright = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.right, UniteDeDistance[2]);

                                    if (hitup == true && hitdown == true && hitleft == true && hitright == true)
                                    {

                                        RaycastHit2D hit = Physics2D.Raycast(IntelliBalle.transform.position, Vector2.left, UniteDeDistance[2], LayerMask.GetMask("DetectionMur"));
                                        Destroy(hit.collider.gameObject);


                                        IntelliBalle.transform.position = new Vector3(YeuxBalle.transform.position.x, YeuxBalle.transform.position.y, 0);
                                        MemoireALongTerme.Add(3);
                                        nbCasesExplorés++;
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
                                YeuxBalle.transform.position = new Vector3(IntelliBalle.transform.position.x + UniteDeDistance[0], IntelliBalle.transform.position.y, 0);
                                if (YeuxBalle.transform.position.x < coin2.transform.position.x)
                                {
                                    RaycastHit2D hitup = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.up, UniteDeDistance[3]);
                                    RaycastHit2D hitdown = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.down, UniteDeDistance[3]);
                                    RaycastHit2D hitleft = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.left, UniteDeDistance[2]);
                                    RaycastHit2D hitright = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.right, UniteDeDistance[2]);

                                    if (hitup == true && hitdown == true && hitleft == true && hitright == true)
                                    {
                                        RaycastHit2D hit = Physics2D.Raycast(IntelliBalle.transform.position, Vector2.right, UniteDeDistance[2], LayerMask.GetMask("DetectionMur"));
                                        Destroy(hit.collider.gameObject);


                                        IntelliBalle.transform.position = new Vector3(YeuxBalle.transform.position.x, YeuxBalle.transform.position.y, 0);
                                        MemoireALongTerme.Add(4);
                                        nbCasesExplorés++;
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




                //recul
                else if (haut == false && bas == false && droite == false && gauche == false)
                {

                    do
                    {
                        CurseurMemoire = (MemoireALongTerme.Count) - 1;

                        switch (MemoireALongTerme[CurseurMemoire])
                        {
                            case 1:
                                IntelliBalle.transform.position = new Vector3(IntelliBalle.transform.position.x, IntelliBalle.transform.position.y - UniteDeDistance[1], 0);

                                MemoireALongTerme.RemoveAt(CurseurMemoire);
                                break;

                            case 2:
                                IntelliBalle.transform.position = new Vector3(IntelliBalle.transform.position.x, IntelliBalle.transform.position.y + UniteDeDistance[1], 0);

                                MemoireALongTerme.RemoveAt(CurseurMemoire);
                                break;

                            case 3:
                                IntelliBalle.transform.position = new Vector3(IntelliBalle.transform.position.x + UniteDeDistance[0], IntelliBalle.transform.position.y, 0);

                                MemoireALongTerme.RemoveAt(CurseurMemoire);
                                break;

                            case 4:
                                IntelliBalle.transform.position = new Vector3(IntelliBalle.transform.position.x - UniteDeDistance[0], IntelliBalle.transform.position.y, 0);

                                MemoireALongTerme.RemoveAt(CurseurMemoire);
                                break;
                        }

                        YeuxBalle.transform.position = new Vector3(IntelliBalle.transform.position.x, IntelliBalle.transform.position.y + UniteDeDistance[1], 0);
                        verifBackHaut[0] = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.up, UniteDeDistance[3], LayerMask.GetMask("DetectionMur"));
                        verifBackHaut[1] = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.down, UniteDeDistance[3], LayerMask.GetMask("DetectionMur"));
                        verifBackHaut[2] = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.left, UniteDeDistance[2], LayerMask.GetMask("DetectionMur"));
                        verifBackHaut[3] = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.right, UniteDeDistance[2], LayerMask.GetMask("DetectionMur"));



                        YeuxBalle.transform.position = new Vector3(IntelliBalle.transform.position.x, IntelliBalle.transform.position.y - UniteDeDistance[1], 0);
                        verifBackBas[0] = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.up, UniteDeDistance[3], LayerMask.GetMask("DetectionMur"));
                        verifBackBas[1] = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.down, UniteDeDistance[3], LayerMask.GetMask("DetectionMur"));
                        verifBackBas[2] = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.left, UniteDeDistance[2], LayerMask.GetMask("DetectionMur"));
                        verifBackBas[3] = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.right, UniteDeDistance[2], LayerMask.GetMask("DetectionMur"));

                        YeuxBalle.transform.position = new Vector3(IntelliBalle.transform.position.x - UniteDeDistance[0], IntelliBalle.transform.position.y, 0);
                        verifBackGauche[0] = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.up, UniteDeDistance[3], LayerMask.GetMask("DetectionMur"));
                        verifBackGauche[1] = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.down, UniteDeDistance[3], LayerMask.GetMask("DetectionMur"));
                        verifBackGauche[2] = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.left, UniteDeDistance[2], LayerMask.GetMask("DetectionMur"));
                        verifBackGauche[3] = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.right, UniteDeDistance[2], LayerMask.GetMask("DetectionMur"));

                        YeuxBalle.transform.position = new Vector3(IntelliBalle.transform.position.x + UniteDeDistance[0], IntelliBalle.transform.position.y, 0);
                        verifBackDroite[0] = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.up, UniteDeDistance[3], LayerMask.GetMask("DetectionMur"));
                        verifBackDroite[1] = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.down, UniteDeDistance[3], LayerMask.GetMask("DetectionMur"));
                        verifBackDroite[2] = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.left, UniteDeDistance[2], LayerMask.GetMask("DetectionMur"));
                        verifBackDroite[3] = Physics2D.Raycast(YeuxBalle.transform.position, Vector2.right, UniteDeDistance[2], LayerMask.GetMask("DetectionMur"));


                        if (IsAllTrue(verifBackHaut) == true || IsAllTrue(verifBackBas) == true || IsAllTrue(verifBackGauche) == true || IsAllTrue(verifBackDroite) == true)
                        {

                            do
                            {

                                rand = Random.Range(0, 4);

                                switch (rand)
                                {
                                    case 0:
                                        if (IsAllTrue(verifBackHaut) == true)
                                        {
                                            haut = true;
                                            confirmationRecul = true;
                                            confirmationNEWdir = true;
                                        }
                                        else
                                        {
                                            confirmationNEWdir = false;
                                        }
                                        break;

                                    case 1:

                                        if (IsAllTrue(verifBackBas) == true)
                                        {
                                            bas = true;
                                            confirmationRecul = true;
                                            confirmationNEWdir = true;
                                        }
                                        else
                                        {
                                            confirmationNEWdir = false;
                                        }
                                        break;

                                    case 2:
                                        if (IsAllTrue(verifBackGauche) == true)
                                        {
                                            gauche = true;
                                            confirmationRecul = true;
                                            confirmationNEWdir = true;
                                        }
                                        else
                                        {
                                            confirmationNEWdir = false;
                                        }
                                        break;

                                    case 3:
                                        if (IsAllTrue(verifBackDroite) == true)
                                        {
                                            droite = true;
                                            confirmationRecul = true;
                                            confirmationNEWdir = true;
                                        }
                                        else
                                        {
                                            confirmationNEWdir = false;
                                        }
                                        break;
                                }

                            } while (confirmationNEWdir != true);

                        }

                        else
                        {
                            confirmationRecul = false;
                        }




                    } while (confirmationRecul != true);

                }




            } while (confirmationDirection != true);
        } while (nbCasesExplorés != nbCasestotales);
     
    }
    
    
    

    //vérifie  que l'array est true partout
    //je l'ai copié de reddit. TOO BAD ! 
    public bool IsAllTrue(bool[] collection)
    {
        for (int i = 0; i < collection.Length; i++)
            if (!collection[i])
            {
                return false;
            }
        return true;
    }





}
