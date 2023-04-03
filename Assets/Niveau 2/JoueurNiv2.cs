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
        //if (_jeuDebute) //Quand le niveau aura commenc�
        //{
            MouvementsJoueurs();
            RotateInDirectionOfInput();
        //}
    }

    /*
     * R�le : D�placer le joueur dans la direction voulue
     * Entr�e : Aucune
     */
    private void MouvementsJoueurs()
    {
        if (this.CompareTag("Player1")) //Si c'est le joueur 1
        {
            float posX = Input.GetAxisRaw("Horizontal_P1"); //Regarde dans quelle direction horizontale le joueur veut se d�placer
            float posY = Input.GetAxisRaw("Vertical_P1"); //Regarde dans quelle direction verticale le joueur veut se d�placer
            _direction = new Vector2(posX, posY) * _moveSpeed * Time.deltaTime; //D�termine le vecteur de direction
        }
        else if (this.CompareTag("Player2")) //Si c'est le joueur 2
        {
            float posX = Input.GetAxis("Horizontal_P2"); //Regarde dans quelle direction horizontale le joueur veut se d�placer
            float posY = Input.GetAxis("Vertical_P2"); //Regarde dans quelle direction verticale le joueur veut se d�placer
            _direction = new Vector2(posX, posY) * _moveSpeed * Time.deltaTime; //D�termine le vecteur de direction
        }

        _rb.velocity = _direction; //D�place le joueur dans la direction voulue
    }

    /*
     * R�le : Faire tourner le joueur dans la direction de son d�placement
     * Entr�e : Aucune
     */
    private void RotateInDirectionOfInput()
    {
            if (_direction != Vector2.zero) //Si le joueur se d�place
            {
                _rb.freezeRotation = false; //On permet la rotation
                
                Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _direction); //On d�termine dans quelle direction effectuer la rotation
                Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime); //On cr�e quaternion de d�placement
               
                _rb.MoveRotation(rotation); //On effectue la rotation
            }
            else //Si le joueur ne se d�place pas
            {
                _rb.freezeRotation = true; //On bloque la rotation
            }
        
    }

    /*
     * R�le : Indiquer que le joueur peut commencer � se d�placer
     * Entr�e : Aucune
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
