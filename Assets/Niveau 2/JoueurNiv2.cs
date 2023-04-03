using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoueurNiv2 : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 600f;
    [SerializeField] private float _rotationSpeed = 700f;
    private Rigidbody2D _rb;
    private Vector2 _direction;
  
    //bool _jeuDebute = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //if (_jeuDebute) //Quand le niveau aura commencé
        //{
            MouvementsJoueurs();
            RotateInDirectionOfInput();
        //}
    }

    /*
     * Rôle : Déplacer le joueur dans la direction voulue
     * Entrée : Aucune
     */
    private void MouvementsJoueurs()
    {
        if (this.CompareTag("Player1")) //Si c'est le joueur 1
        {
            float posX = Input.GetAxisRaw("Horizontal_P1"); //Regarde dans quelle direction horizontale le joueur veut se déplacer
            float posY = Input.GetAxisRaw("Vertical_P1"); //Regarde dans quelle direction verticale le joueur veut se déplacer
            _direction = new Vector2(posX, posY) * _moveSpeed * Time.deltaTime; //Détermine le vecteur de direction
        }
        else if (this.CompareTag("Player2")) //Si c'est le joueur 2
        {
            float posX = Input.GetAxis("Horizontal_P2"); //Regarde dans quelle direction horizontale le joueur veut se déplacer
            float posY = Input.GetAxis("Vertical_P2"); //Regarde dans quelle direction verticale le joueur veut se déplacer
            _direction = new Vector2(posX, posY) * _moveSpeed * Time.deltaTime; //Détermine le vecteur de direction
        }

        _rb.velocity = _direction; //Déplace le joueur dans la direction voulue
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
                
                Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _direction); //On détermine dans quelle direction effectuer la rotation
                Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime); //On crée quaternion de déplacement
               
                _rb.MoveRotation(rotation); //On effectue la rotation
            }
            else //Si le joueur ne se déplace pas
            {
                _rb.freezeRotation = true; //On bloque la rotation
            }
        
    }

    /*
     * Rôle : Indiquer que le joueur peut commencer à se déplacer
     * Entrée : Aucune
     * Sortie : Aucune
     */
    public void DebuterJeu()
    {
        //_jeuDebute = true;
    }

    public void FinNiveau()
    {

    }
}
