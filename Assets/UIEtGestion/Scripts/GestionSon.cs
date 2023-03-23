using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _soudtrack = default;

    void Start()
    {
        _soudtrack.Play(); //Joue la musique de fond associ�e au game object de la sc�ne

    }
}
