using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TresorNiv2 : MonoBehaviour
{
    [SerializeField] int _pointsTresors;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player1")
        {
            if (other.GetComponent<JoueurNiv2>()._jeuDebute == true)
            {
                GestionUIJeu UICommand = FindObjectOfType<GestionUIJeu>().GetComponent<GestionUIJeu>();
                UICommand.AjouterScore(_pointsTresors, 1);
                Destroy(this.gameObject);
            }
        }
        else if (other.gameObject.tag == "Player2")
        {
            if (other.GetComponent<JoueurNiv2>()._jeuDebute == true)
            {
                GestionUIJeu UICommand = FindObjectOfType<GestionUIJeu>().GetComponent<GestionUIJeu>();
                UICommand.AjouterScore(_pointsTresors, 2);
                Destroy(this.gameObject);
            }
        }
        
    }
}
