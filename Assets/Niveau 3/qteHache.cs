using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qteHache : MonoBehaviour
{

    public GameObject _character;
    public Rigidbody2D _rb;
    public GameObject[] listeInputs;

    private bool play = false;
    private bool waiting = false;

    private int score1 = 0, score2 = 0;
    private int input1=0,input2=0, input3 = 0, input4 = 0;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(animationDepart());
    }

    // Update is called once per frame
    void Update()
    {
        if(play)
        {
            input1 = Random.Range(0, listeInputs.Length);
            input2 = Random.Range(0, listeInputs.Length);

            // 2 inputs
            StartCoroutine(waiter(30));
            do
            {





            }while( waiting );
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

        play = true;
    }

    IEnumerator waiter(float delay)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(delay);
        waiting = false;

    }


}
