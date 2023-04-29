using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionSon : MonoBehaviour
{
    [SerializeField] public AudioSource[] _soundtrack = default;

    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music"); //V�rifie si un autre objet identique est d�j� pr�sent

        if (objs.Length > 1) //Si un objet identique existe d�j�
        {
            Destroy(this.gameObject); //L'objet est d�truit afin qu'il n'y ait qu'une musique en jeu
        }

        _soundtrack[0].Play();

        DontDestroyOnLoad(this.gameObject); //Fait en sorte que la musique continue d'une sc�ne � l'autre
    }

    /*
     * R�le : Arr�ter la musique en cours
     * Entr�e : 1 entier qui indique quelle musique est en cours
     * Sortie : Aucune
     */
    public void ArreterMusique(int p_numeroMusique)
    {
        _soundtrack[p_numeroMusique].Stop();
    }

    /*
     * R�le : Jouer de la musique
     * Entr�e : 1 entier qui indique quelle musique jouer
     * Sortie : Aucune
     */
    public void JouerMusique(int p_numeroMusique)
    {
        _soundtrack[p_numeroMusique].Play();
    }

}
