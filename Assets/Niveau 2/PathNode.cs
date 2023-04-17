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
     * Rôle : Détecter s'il y a un mur au-dessus ou en-dessus du noeud
     * Entrée : 1 entier qui indique à quelle distance du noeud se trouverait le mur
     * Sortie : 1 booléen qui indique si le chemin est dégagé
     */
    public bool DetectWallUp(float p_dist)
    {
        bool espaceLibre = true; //Booléen qui indique si le chemin est dégagé

        //Envoie un raycast pour voir s'il y a un mur
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, p_dist, LayerMask.GetMask("DetectionMur"));

        //Si le raycast frappe quelque chose
        if(hit.collider != null)
        {
            espaceLibre = false; //On indique que le chemin ne passe pas par là
        }

        return espaceLibre;
    }
    
    /*
     * Rôle : Détecter s'il y a un mur à droite ou à gauche du noeud
     * Entrée : 1 entier qui indique à quelle distance du noeud se trouverait le mur
     * Sortie : 1 booléen qui indique si le chemin est dégagé
     */
    public bool DetectWallSide(float p_dist)
    {
        bool espaceLibre = true; //Booléen qui indique si le chemin est dégagé

        //Envoie un raycast pour voir s'il y a un mur
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, p_dist, LayerMask.GetMask("DetectionMur"));

        //Si le raycast frappe quelque chose
        if(hit.collider != null)
        {
            espaceLibre = false; //On indique que le chemin ne passe pas par là
        }

        return espaceLibre;
    }
}
