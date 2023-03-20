using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _soudtrack = default;

    void Start()
    {
        _soudtrack.Play(); //Joue la musique de fond associée au game object de la scène

    }
}
