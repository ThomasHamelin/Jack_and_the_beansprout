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
    public GameObject _otherPlayer;
    public GameObject _canvasScore;

    //_canvasScore.GetComponent<GestionUIJeu>().AjouterScore(point, n_joueur );

    public bool play = false;

    private int n_joueur;
    private Vector2 playerInput;
    private bool jumpPressed = false;
    private bool wantJump = false;
    private int canJump = 0;
    private Vector2 lasttouchPosition;
    private bool waiting = false;
    private bool jumping = false;
    private Animator anim;


    private const int pointParPlateforme = 100;
    private const int pointBonusFin = 300;
    private int heightMilestone = 10;
    private int heightMilestoneAchieved = 5;

    private void Start()
    {
        if (this.CompareTag("Player1"))
        {
            n_joueur = 1;
        }
        else if (this.CompareTag("Player2"))
        {
            n_joueur = 2;
        }


        anim = GetComponent<Animator>();
        lasttouchPosition = this.GetComponent<Transform>().position;
    }


    private void Update()
    {
        if(play)
        {   
            if(n_joueur == 1)
            {
                playerInput = new Vector2(Input.GetAxis("Horizontal_P1"), 0f);
                jumpPressed = Input.GetKeyDown(KeyCode.W);
            }

            if (n_joueur == 2)
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
    
    }

    //m�me vitesse que toute la physique
    private void FixedUpdate()
    {
        if (!waiting)
        {


            // move

            if (jumping)
            {
                _rb.AddForce(playerInput * _moveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
                if (playerInput.x > 0)
                {
                    //droite
                    anim.SetBool("isjumpingG", false);
                    anim.SetBool("isjumpingD", true);
                }
                else
                {
                    //gauche
                    anim.SetBool("isjumpingD", false);
                    anim.SetBool("isjumpingG", true);
                }

            }
            else if (playerInput.x == 0)
            {
                anim.SetBool("iswalkingD", false);
                anim.SetBool("iswalkingG", false);
            }
            else
            {
                _rb.AddForce(playerInput * _moveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);

                if (playerInput.x > 0)
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

            // jump
            if (wantJump)
            {
                jumping = true;
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
    * R�le : actions quand touche plateforme
    * Entr�e : aucune 
    * Sortie : aucune 
    */
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("PlateformeFinale"))
        {
            // bonus fin
            _canvasScore.GetComponent<GestionUIJeu>().AjouterScore(pointBonusFin, n_joueur);

            _otherPlayer.GetComponent<Mouvement>().end();
            end();

        }


        if (other.gameObject.tag.Equals("Plateforme") && other.gameObject.GetComponent<Transform>().position.y >= lasttouchPosition.y - 2f)
        {
            lasttouchPosition = other.gameObject.GetComponent<Transform>().position;

            //ajouter score
            if(heightMilestoneAchieved <= other.GetComponent<Transform>().position.y)
            {
                _canvasScore.GetComponent<GestionUIJeu>().AjouterScore(pointParPlateforme, n_joueur);
                heightMilestoneAchieved += heightMilestone;
            }

            jumping = false;
            anim.SetBool("isjumpingG", false);
            anim.SetBool("isjumpingD", false);


            //peut sauter apr�s contact
            canJump = 2;

            _camera.GetComponent<FollowPlayer>().minHeight = lasttouchPosition.y;
        }
    }

    /*
     * R�le : un double jump uniquement
     * Entr�e : aucune 
     * Sortie : aucune 
     */
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Plateforme") || other.gameObject.tag.Equals("HighestPlateforme"))
        {
            canJump = 1;
        }
    } 

    public void end()
    {
        play = false;
        waiting = true;
    }
}

