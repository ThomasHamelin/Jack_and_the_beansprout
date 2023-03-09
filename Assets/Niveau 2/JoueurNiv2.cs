using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoueurNiv2 : MonoBehaviour
{
    [SerializeField] private float _vitesse;
    [SerializeField] private int _numJoueur;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float deplacementX = Input.GetAxisRaw($"Horizontal_P{_numJoueur}");
        float deplacementY = Input.GetAxisRaw($"Vertical_P{_numJoueur}");
        
        Vector2 deplacement = new Vector2(deplacementX, deplacementY);
        rb.velocity = deplacement * _vitesse;

        if (deplacement != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, deplacement);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 0.1f*Time.deltaTime);
            Debug.Log(deplacement);
            rb.MoveRotation(rotation);
        }


    }

}
