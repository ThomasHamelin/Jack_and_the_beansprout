using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qteHache : MonoBehaviour
{
    public int _nbrInputGame1 = 20,_nbrInputGame2 = 20;
    public Rigidbody2D _rb;

    public GameObject _directionsAffichage;
    public GameObject _character;
    public float minInput = 0.5f, maxInput = 0.5f;

    private bool play = false, needNull = false;

    private int nbrInput = 0;
    private Vector2 playerInput;
    private string input = "Null", directionNeeded = "Waiting";


    // Start is called before the first frame update
    void Start()
    {
        _nbrInputGame2 += _nbrInputGame1;
        StartCoroutine(animationDepart());
    }

    // Update is called once per frame
    void Update()
    {
        if (play)
        {
            // Prendre l'input selon le joueur
            if (this.CompareTag("Player1"))
            {
                playerInput = new Vector2(Input.GetAxis("Horizontal_P1"), Input.GetAxis("Vertical_P1"));
            }
            if (this.CompareTag("Player2"))
            {
                playerInput = new Vector2(Input.GetAxis("Horizontal_P2"), Input.GetAxis("Vertical_P2"));
            }

            // Transformer l'input en 4 directions ( et aucune)
            if (playerInput.x > minInput && playerInput.y < maxInput && playerInput.y > -maxInput)
            {
                input = "Right";
            }
            else if (playerInput.x < -minInput && playerInput.y < maxInput && playerInput.y > -maxInput)
            {
                input = "Left";
            }
            else if (playerInput.y > minInput && playerInput.x < maxInput && playerInput.x > -maxInput)
            {
                input = "Up";
            }
            else if (playerInput.y < -minInput && playerInput.x < maxInput && playerInput.x > -maxInput)
            {
                input = "Down";
            }
            else
            {
                input = "Null";
            }

            //game1 : précision
            if (input == directionNeeded && nbrInput <= _nbrInputGame1)
            {
                if (directionNeeded == "Null")
                {
                    directionNeeded = CreateInput();
                }
                else
                {
                    nbrInput++;
                    _directionsAffichage.GetComponent<directionAffichage>().changeDirection(5);
                    directionNeeded = "Null";
                }
            }

            //game2 : button mash
            if (nbrInput > _nbrInputGame1 && nbrInput <= _nbrInputGame2)
            {

                if (needNull && input == "Null")
                {
                    _directionsAffichage.GetComponent<directionAffichage>().blinkAllDirection(true);
                    needNull = false;
                }
                else
                {
                    nbrInput++;
                    _directionsAffichage.GetComponent<directionAffichage>().blinkAllDirection(false);
                    needNull = true;

                }

            }

        }



    }

    IEnumerator animationDepart()
    {
        yield return new WaitForSecondsRealtime(2.5f);

        Vector2 animInput = new Vector2(0f, 0f);

        if (this.CompareTag("Player1"))
        {
            animInput = new Vector2(700f, 0f);
        }

        if (this.CompareTag("Player2"))
        {
            animInput = new Vector2(-700f, 0f);
        }

        _rb.AddForce(animInput * Time.fixedDeltaTime, ForceMode2D.Impulse);

        yield return new WaitForSecondsRealtime(0.5f);

        _directionsAffichage.SetActive(true);
        directionNeeded = CreateInput();
        play = true;
    }

    string CreateInput()
    {
        int randomChoice = Random.Range(0, 3);
        _directionsAffichage.GetComponent<directionAffichage>().changeDirection(randomChoice);
        switch (randomChoice)
        {
            case 0:
                return "Up";
            case 1:
                return "Down";
            case 2:
                return "Left";
            case 3:
                return "Right";
        }
        return "Null";
    }


}
