using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoueurNiv2 : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 50f;
    [SerializeField] private float _rotationSpeed = 700f;
    [SerializeField] private float _hauteurCam = 2.5f;
    [SerializeField] private Camera _cam = default;

    private Rigidbody2D _rb;
    private Vector2 _direction;
    private float _posX, _posY;
    bool _jeuDebute = false;

    public Animator _animator;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _cam.transform.position = new Vector3(this.gameObject.transform.position.x, (this.gameObject.transform.position.y + _hauteurCam), _cam.gameObject.transform.position.z) ;
    }

    void FixedUpdate()
    {
        if (_jeuDebute) //Quand le niveau aura commencé
        {
            MouvementsJoueurs();
            RotateInDirectionOfInput();
        }
    }

    /*
     * Rôle : Déplacer le joueur dans la direction voulue
     * Entrée : Aucune
     */
    private void MouvementsJoueurs()
    {
        if (this.CompareTag("Player1")) //Si c'est le joueur 1
        {
            _posX = Input.GetAxisRaw("Horizontal_P1"); //Regarde dans quelle direction horizontale le joueur veut se déplacer
            _posY = Input.GetAxisRaw("Vertical_P1"); //Regarde dans quelle direction verticale le joueur veut se déplacer
            _direction = new Vector2(_posX, _posY) * _moveSpeed * Time.deltaTime; //Détermine le vecteur de direction
        }
        else if (this.CompareTag("Player2")) //Si c'est le joueur 2
        {
            _posX = Input.GetAxis("Horizontal_P2"); //Regarde dans quelle direction horizontale le joueur veut se déplacer
            float _posY = Input.GetAxis("Vertical_P2"); //Regarde dans quelle direction verticale le joueur veut se déplacer
            _direction = new Vector2(_posX, _posY) * _moveSpeed * Time.deltaTime; //Détermine le vecteur de direction
        }

        if (_direction != Vector2.zero) //Si le joueur se déplace
        {
            _cam.transform.position += new Vector3(_direction.x, _direction.y, 0);

            transform.position += new Vector3(_direction.x,_direction.y, 0); //Déplace le joueur dans la direction voulue
        }
        
        
    }

    /*
     * Rôle : Faire tourner le joueur dans la direction de son déplacement
     * Entrée : Aucune
     */
    private void RotateInDirectionOfInput()
    {
            if (_direction != Vector2.zero) //Si le joueur se déplace
            {
                _rb.freezeRotation = false; //On permet la rotation
                Quaternion targetRotation = Quaternion.LookRotation(transform.forward, new Vector2(-_direction.y, _direction.x)); //On détermine dans quelle direction effectuer la rotation
                Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime); //On crée quaternion de déplacement

                _rb.MoveRotation(rotation); //On effectue la rotation

                _animator.SetBool("IsWalking", true); //Fait jouer l'animation du joueur qui se déplace
            }
            else //Si le joueur ne se déplace pas
            {
                _rb.freezeRotation = true; //On bloque la rotation
            _animator.SetBool("IsWalking", false); //Arrête l'animation du joueur qui se déplace
        }
        
    }

    /*
     * Rôle : Indiquer que le joueur peut commencer à se déplacer
     * Entrée : Aucune
     * Sortie : Aucune
     */
    public void DebuterJeu()
    {
        _jeuDebute = true;
    }

    public void FinNiveau()
    {
        _jeuDebute = false; //Indique que le niveau n'est plus en cours

        //Pour arrêter le mouvement et la rotation du joueur
        _rb.velocity = new Vector2(0, 0);
        _rb.freezeRotation = true;
    }

}
