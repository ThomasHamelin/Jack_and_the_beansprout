using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoueurNiv2 : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 600f;
    [SerializeField] private float _rotationSpeed = 700f;
    [SerializeField] private Camera _cam = default;

    private Pathfinder _pathfinder = default;
    private Rigidbody2D _rb;
    private Vector2 _direction;
    private bool _jeuDebute = false;
    private float _posX, _posY;
    private float _distanceX, _distanceY;
    public Animator _animator;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _cam.transform.position = new Vector3(this.gameObject.transform.position.x, (this.gameObject.transform.position.y), _cam.gameObject.transform.position.z);
        _pathfinder = FindObjectOfType<Pathfinder>().GetComponent<Pathfinder>();
    }

    void FixedUpdate()
    {
        if (_jeuDebute) //Quand le niveau aura commenc�
        {
            MouvementsJoueurs();
            RotateInDirectionOfInput();
        }
    }

    /*
     * R�le : D�placer le joueur dans la direction voulue
     * Entr�e : Aucune
     */
    private void MouvementsJoueurs()
    {
        if (this.CompareTag("Player1")) //Si c'est le joueur 1
        {
            _posX = Input.GetAxisRaw("Horizontal_P1"); //Regarde dans quelle direction horizontale le joueur veut se d�placer
            _posY = Input.GetAxisRaw("Vertical_P1"); //Regarde dans quelle direction verticale le joueur veut se d�placer
            _direction = new Vector2(_posX, _posY) * _moveSpeed * Time.deltaTime; //D�termine le vecteur de direction
        }
        else if (this.CompareTag("Player2")) //Si c'est le joueur 2
        {
            _posX = Input.GetAxis("Horizontal_P2"); //Regarde dans quelle direction horizontale le joueur veut se d�placer
            _posY = Input.GetAxis("Vertical_P2"); //Regarde dans quelle direction verticale le joueur veut se d�placer
            _direction = new Vector2(_posX, _posY) * _moveSpeed * Time.deltaTime; //D�termine le vecteur de direction
        }

        if (_direction != Vector2.zero) //Si le joueur se d�place
        {
            _cam.transform.position += new Vector3(_direction.x, _direction.y, 0); //D�place la cam�ra avec le joueur
            transform.position += new Vector3(_direction.x, _direction.y, 0); //D�place le joueur dans la direction voulue
        }
        //_rb.constraints = RigidbodyConstraints2D.FreezePosition; //On bloque la position
        
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
            _animator.SetBool("IsWalking", true); //Fait jouer l'animation du joueur qui se d�place
        }
        else //Si le joueur ne se d�place pas
        {
            _rb.freezeRotation = true; //On bloque la rotation
            _animator.SetBool("IsWalking", false); //Arr�te l'animation du joueur qui se d�place
        }

    }

    /*
     * R�le : Indiquer que le joueur peut commencer � se d�placer
     * Entr�e : Aucune
     * Sortie : Aucune
     */
    public void DebuterJeu()
    {
        _jeuDebute = true;
    }

    public void FinNiveau()
    {
        _jeuDebute = false;
        _pathfinder.FindPath(transform.position.x, transform.position.y);
    }

    public void SuivreChemin(List<PathNode> p_chemin)
    {
        
        foreach(PathNode pointSuivant in p_chemin)
        {
            do
            {
                _distanceX = pointSuivant.transform.position.x - transform.position.x;
                _distanceY = pointSuivant.transform.position.y - transform.position.y;

                if (_distanceX != 0f)
                {
                    _posY = 0f;

                    if (_distanceX > 0f)
                    {
                        _posX = 1f;
                    }
                    else if (_distanceX < 0f)
                    {
                        _posX = -1f;
                    }
                }
                else if (_distanceY != 0f)
                {
                    _posX = 0f;
                    if (_distanceY > 0f)
                    {
                        _posY = 1f;
                    }
                    else if (_distanceY < 0f)
                    {
                        _posY = -1f;
                    }
                }
                
                _direction = new Vector2(_posX, _posY) * _moveSpeed * Time.deltaTime; //D�termine le vecteur de direction
                transform.position += new Vector3(_direction.x, _direction.y, 0); //D�place le joueur dans la direction voulue

            } while (_distanceX != 0f && _distanceY != 0f);
            
            
        }
    }
}
