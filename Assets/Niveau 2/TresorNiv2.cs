using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TresorNiv2 : MonoBehaviour
{
    [SerializeField] private Sprite[] _imagesTresors;
    [SerializeField] int[] _pointsTresors;

    int rand;
    

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rand= Random.Range(0,_imagesTresors.Length);
        spriteRenderer.sprite = _imagesTresors[rand];


        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Joueur1"))
        {
            GestionUIJeu UICommand = FindObjectOfType<GestionUIJeu>();

            UICommand.AjouterScore(_pointsTresors[0],1);
        }
        else if (other.gameObject.tag.Equals("Joueur2"))
        {
            GestionUIJeu UICommand = FindObjectOfType<GestionUIJeu>();
            UICommand.AjouterScore(_pointsTresors[0],2);
        }
    }
}
