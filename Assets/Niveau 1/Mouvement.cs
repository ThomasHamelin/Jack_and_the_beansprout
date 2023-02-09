using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement : MonoBehaviour
{
    [SerializeField] private float _speed = 15f;

    void Start()
    {

    }

    void Update()
    {
        Move();
    }


    // Déplacements et limitation des mouvements du joueur
    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0f);
        transform.Translate(direction * Time.deltaTime * _speed);
        /*
        if (horizontalInput < 0)
        {
            _anim.SetBool("Turn_Left", true);
            _anim.SetBool("Turn_Right", false);
        }
        else if (horizontalInput > 0)
        {
            _anim.SetBool("Turn_Left", false);
            _anim.SetBool("Turn_Right", true);
        }
        else
        {
            _anim.SetBool("Turn_Left", false);
            _anim.SetBool("Turn_Right", false);
        }*/

        //Gérer la zone verticale
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -11f, 11f),
        transform.position.y);
        

        //Gérer dépassement horizontaux
        //if (transform.position.x >= 11.3)
        //{
        //    transform.position = new Vector3(-11.3f, transform.position.y, 0f);
        //}
        //else if (transform.position.x <= -11.3)
        //{
        //    transform.position = new Vector3(11.3f, transform.position.y, 0f);
        //}

    }
}
