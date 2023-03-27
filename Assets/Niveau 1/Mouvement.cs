using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouvement : MonoBehaviour
{

    
    public float _moveSpeed = 3.25f;
    public float _jumpSpeed;
    public GameObject _character;
    public Rigidbody2D _rb;
    public float respawn_interval;
    public GameObject _camera;

    private Vector2 playerInput;
    private bool jumpPressed = false;
    private bool wantJump = false;
    private int canJump = 0;
    private Vector2 lasttouchPosition;
    private bool waiting = false;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        lasttouchPosition = this.GetComponent<Transform>().position;
    }


    private void Update()
    {

        if(this.CompareTag("Player1"))
        {
            playerInput = new Vector2(Input.GetAxis("Horizontal_P1"), 0f);
            jumpPressed = Input.GetKeyDown(KeyCode.W);
        }

        if (this.CompareTag("Player2"))
        {
            playerInput = new Vector2(Input.GetAxis("Horizontal_P2"), 0f);
            jumpPressed = Input.GetKeyDown(KeyCode.UpArrow);
        }



        if (canJump > 0 && jumpPressed)
        {
            jumpPressed = false;
            canJump--;
            wantJump = true;
        }
    }

    //même vitesse que toute la physique
    private void FixedUpdate()
    {
        if (!waiting)
        {
            

            // move
            if (playerInput.x != 0)
            {
                
                _rb.AddForce(playerInput * _moveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
                
                if(playerInput.x > 0)
                {
                    //droite
                    anim.SetBool("iswalkingD", true);
                }
                else
                { 
                    //gauche
                    anim.SetBool("iswalkingG", true);
                }

            }
            else
            {
                anim.SetBool("iswalkingD", false);
                anim.SetBool("iswalkingG", false);
            }

            // jump
            if (wantJump)
            {
                anim.SetBool("jumping", true);
                _rb.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);

                //anim jump
                if (playerInput.x > 0)
                {
                    //droite
                    anim.SetBool("isjumpingD", true);
                }
                else if (playerInput.x < 0)
                {
                    //gauche
                    anim.SetBool("isjumpingG", true);
                }




                wantJump = false;
            }
            

            if(this.GetComponent<Transform>().position.y < lasttouchPosition.y - 2f)
            {
                StartCoroutine(respawn());
                
            }
        }
        

    }

    IEnumerator respawn()
    {
        waiting = true;//stoper le mouvement durant le respawn

        this.GetComponent<CapsuleCollider2D>().isTrigger = true;//deactiver collider

        //add animation respawn 
        yield return new WaitForSecondsRealtime(respawn_interval);
        
        this.GetComponent<CapsuleCollider2D>().isTrigger = false;//activer collider

        this.GetComponent<Transform>().position = new Vector2(lasttouchPosition.x, lasttouchPosition.y + 0.5f);
        waiting = false;
    }

    /*
    * Rôle : actions quand touche plateforme
    * Entrée : aucune 
    * Sortie : aucune 
    */
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag.Equals("Plateforme") && other.gameObject.GetComponent<Transform>().position.y >= lasttouchPosition.y - 2f)
        {
            lasttouchPosition = other.gameObject.GetComponent<Transform>().position;
            
            //peut sauter après contact
            canJump = 2;


            //jump false
            anim.SetBool("isjumpingG", false);
            anim.SetBool("isjumpingD", false);
            anim.SetBool("jumping",false);

            _camera.GetComponent<FollowPlayer>().minHeight = lasttouchPosition.y;
        }
    }

    /*
     * Rôle : un double jump uniquement
     * Entrée : aucune 
     * Sortie : aucune 
     */
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Plateforme") || other.gameObject.tag.Equals("HighestPlateforme"))
        {
            canJump = 1;
        }
    } 

}

