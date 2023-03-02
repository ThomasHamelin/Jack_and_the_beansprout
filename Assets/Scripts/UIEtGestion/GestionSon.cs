using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _soudtrack = default;

    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music"); //Vérifie si de la musique joue déjà

        if (objs.Length > 1) //Si de la musique joue déjà
        {
            Destroy(this.gameObject); //L'objet est détruit afin qu'il n'y ait qu'une seule musique en jeu
        }

        _soudtrack.Play(); //Joue la musique de départ
        DontDestroyOnLoad(this.gameObject); //Fait en sorte que la musique continue d'une scène à l'autre


    }
}
