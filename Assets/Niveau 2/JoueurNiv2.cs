using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoueurNiv2 : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 50f;
    [SerializeField] private float _runSpeed = 50f;
    [SerializeField] private float _rotationSpeed = 700f;
    [SerializeField] private Camera _cam = default;
    [SerializeField] private Pathfinder _pathfinder = default;

    private Rigidbody2D _rb;
    private Vector2 _direction = Vector2.zero;
    private bool _jeuDebute = false;
    private bool _suivreChemin = false;
    private float _posX, _posY;
    private int _distanceX, _distanceY;
    private List<PathNode> _chemin;

    public Animator _animator;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>(); //On accède au rigidbody du joueur
        _jeuDebute = false;
        _suivreChemin = false;
    }

    void FixedUpdate()
    {
        if (_jeuDebute) //Si le jeu est en cours
        {
            MouvementsJoueurs();
            RotateInDirectionOfInput();
        }
        else if (_suivreChemin) //Si le joueur soit suivre le chemin généré par l'algorithme du chemin le plus court
        {
            SuivreChemin();
            RotateInDirectionOfInput();
        }
        _cam.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -10); //Déplace la caméra avec le joueur
    }

    /*
     * Rôle : Déplacer le joueur dans la direction voulue
     * Entrée : Aucune
     */
    private void MouvementsJoueurs()
    {
        if (_direction != Vector2.zero) //Si le joueur se déplace
        {
            Vector3 direction3D = new Vector3(_direction.x, _direction.y, 0f);
            _rb.MovePosition(transform.position + direction3D); //Déplace le joueur dans la direction voulue
        }
        if (this.CompareTag("Player1")) //Si c'est le joueur 1
        {
            _posX = Input.GetAxisRaw("Horizontal_P1"); //Regarde dans quelle direction horizontale le joueur veut se déplacer
            _posY = Input.GetAxisRaw("Vertical_P1"); //Regarde dans quelle direction verticale le joueur veut se déplacer
            _direction = new Vector2(_posX, _posY) * _moveSpeed * Time.deltaTime; //Détermine le vecteur de direction
        }
        else if (this.CompareTag("Player2")) //Si c'est le joueur 2
        {
            _posX = Input.GetAxis("Horizontal_P2"); //Regarde dans quelle direction horizontale le joueur veut se déplacer
            _posY = Input.GetAxis("Vertical_P2"); //Regarde dans quelle direction verticale le joueur veut se déplacer
            _direction = new Vector2(_posX, _posY) * _moveSpeed * Time.deltaTime; //Détermine le vecteur de direction
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
            _rb.constraints = RigidbodyConstraints2D.None;
            _rb.freezeRotation = false; //On permet la rotation
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _direction); //On détermine dans quelle direction effectuer la rotation
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime); //On crée quaternion de déplacement

            _rb.MoveRotation(rotation); //On effectue la rotation
            _animator.SetBool("IsWalking", true); //Fait jouer l'animation du joueur qui se déplace
        }
        else //Si le joueur ne se déplace pas
        {
            _rb.constraints = RigidbodyConstraints2D.FreezePosition; //On bloque la position
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

    /*
     * Rôle : Indique au joueur que le jeu est terminé
     * Entrée : Aucune
     * Sortie : Aucune
     */
    public void FinNiveau()
    {
        //Trouve le chemin le plus court qui permet de retourner au début du labyrinthe
        _chemin = _pathfinder.FindPath(this.transform.position.x, this.transform.position.y);
        _jeuDebute = false; //Indique que le jeu n'est plus en cours

        _suivreChemin = true; //Pour que le joueur commence à suivre le chemin
        
        //On regarde si le joueur est le joueur 1, car seul le joueur 1 est appelé par un script extérieur pour que les deux joueurs n'utilisent pas la grille de PathNodes en même temps
        if(this.CompareTag("Player1"))
        {
            GameObject.FindWithTag("Player2").GetComponent<JoueurNiv2>().FinNiveau(); //On appelle la fonction de fin de niveau du joueur 2
        }
        
    }

    /*
     * Rôle : Faire en sorte que le joueur suive le chemin le plus court vers la sortie du labyrinthe
     * Entrée : Aucune
     * Sortie : Aucune
     */
    private void SuivreChemin()
    {
        if (_chemin.Count > 0) //Si on n'a pas atteint la fin du chemin
        {
            float distance = (_chemin[0].transform.position - transform.position).magnitude; //On calcule la distance entre la position du joueur et le prochain noeud 
            _direction = (_chemin[0].transform.position - transform.position)  / distance; //On calcule un vecteur qui indique dans quelle direction se diriger

            if(distance < 0.2f) //Si le noeud est atteint
            {
                _chemin.Remove(_chemin[0]); //On passe au prochain noeud
            }
            else //Si le noeud n'est pas atteint
            {
                //On déplace le joueur en direction du noeud
                Vector3 direction3D = new Vector3(_direction.x, _direction.y, 0f);
                transform.position = Vector3.MoveTowards(transform.position, _chemin[0].transform.position, _moveSpeed * Time.deltaTime);
            }

        }
        else //Si on est à la fin du chemin
        {
            Destroy(this); //Le joueur disparaît
        }
        

    }
}
