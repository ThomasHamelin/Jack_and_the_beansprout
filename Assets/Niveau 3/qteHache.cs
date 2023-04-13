using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qteHache : MonoBehaviour
{

    public GameObject _character;
    public Rigidbody2D _rb;

    private bool play = false;

    // Start is called before the first frame update
    void Start()
    {
        



    }

    // Update is called once per frame
    void Update()
    {
        if(play)
        {






















        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!play)
        {
            StartCoroutine(animationDepart());
        }
       
    }

    IEnumerator animationDepart()
    {
        yield return new WaitForSecondsRealtime(0.3f);

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




}
