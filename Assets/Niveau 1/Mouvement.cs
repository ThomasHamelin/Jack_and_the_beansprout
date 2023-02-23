using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement : MonoBehaviour
{

    
    public float _moveSpeed = 3.25f;
    public float _jumpSpeed;
    public GameObject _character;
    public Rigidbody2D _rb;

    private Vector2 playerInput;
    private bool wantJump;
    private int canJump = 0;
    Vector2 lasttouchPosition;

    private void Start()
    {
        //_rb = _character.GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        playerInput = new Vector2(Input.GetAxis("Horizontal"), 0f);

        if (canJump > 0 && Input.GetKeyDown(KeyCode.Space))
        {
            canJump--;
            wantJump = true;
        }
    }

    //m�me vitesse que toute la physique
    private void FixedUpdate()
    {
        // move
        if (playerInput != Vector2.zero)
        {
            _rb.AddForce(playerInput * _moveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }

        // jump
        if (wantJump)
        {
            _rb.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
            wantJump = false;
        }
    }

    /*
   * R�le : actions quand touche plateforme
   * Entr�e : aucune 
   * Sortie : aucune 
   */
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag.Equals("Plateforme"))
        {
            lasttouchPosition = other.gameObject.GetComponent<Transform>().position;
            //peut sauter apr�s contact
            canJump = 2;
            _character.transform.tag = "onFloor";
            GameObject.Find("Main Camera").GetComponent<FollowPlayer>().minHeight = lasttouchPosition.y;
        }
    }

    /*
     * R�le : enlever l'ennemi une fois d�truit 
     * Entr�e : aucune 
     * Sortie : aucune 
     */
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Plateforme") || other.gameObject.tag.Equals("HighestPlateforme"))
        {
            canJump = 1;
            _character.transform.tag = "Jumping";
        }
    }


    //private void OnCollisionEnter2D(Collision2D col)
    //{
    //    //peut sauter apr�s contact
    //    canJump = 2;
    //    _character.transform.tag = "onFloor";
    //}

    //private void OnCollisionExit2D(Collision2D col)
    //{
    //    canJump = 1;
    //    _character.transform.tag = "Jumping";
    //}
}

