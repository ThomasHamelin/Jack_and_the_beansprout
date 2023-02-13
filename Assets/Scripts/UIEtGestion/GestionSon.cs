using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _soudtrack = default;

    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music"); //V�rifie si de la musique joue d�j�

        if (objs.Length > 1) //Si de la musique joue d�j�
        {
            Destroy(this.gameObject); //L'objet est d�truit afin qu'il n'y ait qu'une seule musique en jeu
        }

        _soudtrack.Play(); //Joue la musique de d�part
        DontDestroyOnLoad(this.gameObject); //Fait en sorte que la musique continue d'une sc�ne � l'autre


    }
}
