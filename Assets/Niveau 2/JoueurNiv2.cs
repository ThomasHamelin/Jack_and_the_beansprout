using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JoueurNiv2 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtDonneDepart = default;
    [SerializeField] float _moveSpeed = 600f;
    [SerializeField] float _rotationSpeed = 700f;
    private Rigidbody2D _rb;
    private Vector2 _direction;
    bool _jeuDebute = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        StartCoroutine(DonnerDepart());
    }

    void FixedUpdate()
    {
        if (_jeuDebute)
        {
            MouvementsJoueurs();
            RotateInDirectionOfInput();
        }
    }

    private void MouvementsJoueurs()
    {
        if (this.CompareTag("Player1"))
        {
            float posX = Input.GetAxisRaw("Horizontal_P1");
            float posY = Input.GetAxisRaw("Vertical_P1");
            _direction = new Vector2(posX, posY) * _moveSpeed * Time.deltaTime;
        }
        else if (this.CompareTag("Player2"))
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

    IEnumerator DonnerDepart()
    {
        _jeuDebute = false;
        _txtDonneDepart.gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);
        if(this.CompareTag("Player1"))
        {
            _txtDonneDepart.gameObject.SetActive(true);

        }
        _txtDonneDepart.text = "3";
        yield return new WaitForSeconds(1f);
        _txtDonneDepart.text = "2";
        yield return new WaitForSeconds(1f);
        _txtDonneDepart.text = "1";
        yield return new WaitForSeconds(1f);
        _txtDonneDepart.text = "Partez!";
        _jeuDebute = true;
        yield return new WaitForSeconds(1f);
        _txtDonneDepart.gameObject.SetActive(false);
    }

}
