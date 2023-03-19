using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoueurNiv2 : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 600f;
    [SerializeField] float _rotationSpeed = 700f;
    private Rigidbody2D _rb;
    private Vector2 _direction;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        MouvementsJoueurs();
        RotateInDirectionOfInput();
    }

    private void MouvementsJoueurs()
    {
        if (this.CompareTag("Joueur1"))
        {
            float posX = Input.GetAxisRaw("Horizontal_P1");
            float posY = Input.GetAxisRaw("Vertical_P1");
            _direction = new Vector2(posX, posY) * _moveSpeed * Time.deltaTime;
        }
        else if (this.CompareTag("Joueur2"))
        {
            float posX = Input.GetAxis("Horizontal_P2");
            float posY = Input.GetAxis("Vertical_P2");
            _direction = new Vector2(posX, posY) * _moveSpeed * Time.deltaTime;
        }

        _rb.velocity = _direction;
    }

    private void RotateInDirectionOfInput()
    {
        if (_direction != Vector2.zero)
        {
            _rb.freezeRotation = false;
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _direction);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            _rb.MoveRotation(rotation);
        }
        else
        {
            _rb.freezeRotation = true;
        }
    }

}
