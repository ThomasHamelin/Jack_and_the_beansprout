using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TresorNiv2 : MonoBehaviour
{
    [SerializeField] private GameObject[] _imagesTresors;
    [SerializeField] private int[] _pointsTresors;

    private GestionUIJeu _gestionUIJeu;
    // Start is called before the first frame update
    void Start()
    {
        
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
