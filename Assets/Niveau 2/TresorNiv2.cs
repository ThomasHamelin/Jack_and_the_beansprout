using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TresorNiv2 : MonoBehaviour
{
    [SerializeField] private Sprite[] _imagesTresors;
    [SerializeField] private int[] _pointsTresors;

    int rand;
    private GestionUIJeu _gestionUIJeu;

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rand= Random.Range(0,_imagesTresors.Length);
        spriteRenderer.sprite = _imagesTresors[rand];


        // _gestionUIJeu = FindObjectOfType<GestionUIJeu>().GetComponent<GestionUIJeu>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject.tag.Equals("Joueur1")
    //    {
            
    //    }
    //    else if (other.gameObject.tag.Equals("Joueur2"))
    //    {

    //    }
    //}
}
