using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;

    public PathNode cameFromNode;

    public PathNode(int x, int y, float posX, float posY)
    {
        this.x = x;
        this.y = y;

        transform.position = new Vector2(posX, posY);
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    /*
     * R�le : D�tecter s'il y a un mur au-dessus ou en-dessus du noeud
     * Entr�e : 1 entier qui indique � quelle distance du noeud se trouverait le mur
     * Sortie : 1 bool�en qui indique si le chemin est d�gag�
     */
    public bool DetectWallUp(float p_dist)
    {
        bool espaceLibre = true; //Bool�en qui indique si le chemin est d�gag�

        //Envoie un raycast pour voir s'il y a un mur
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, p_dist, LayerMask.GetMask("DetectionMur"));

        //Si le raycast frappe quelque chose
        if(hit.collider != null)
        {
            espaceLibre = false; //On indique que le chemin ne passe pas par l�
        }

        return espaceLibre;
    }
    
    /*
     * R�le : D�tecter s'il y a un mur � droite ou � gauche du noeud
     * Entr�e : 1 entier qui indique � quelle distance du noeud se trouverait le mur
     * Sortie : 1 bool�en qui indique si le chemin est d�gag�
     */
    public bool DetectWallSide(float p_dist)
    {
        bool espaceLibre = true; //Bool�en qui indique si le chemin est d�gag�

        //Envoie un raycast pour voir s'il y a un mur
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, p_dist, LayerMask.GetMask("DetectionMur"));

        //Si le raycast frappe quelque chose
        if(hit.collider != null)
        {
            espaceLibre = false; //On indique que le chemin ne passe pas par l�
        }

        return espaceLibre;
    }
}
