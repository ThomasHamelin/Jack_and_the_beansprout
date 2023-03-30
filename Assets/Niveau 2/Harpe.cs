using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Harpe : MonoBehaviour
{
    [SerializeField] private int _ptsVictoire = default;

    private GestionUINiv2 _gestionUINiv2;
    private GestionUIJeu _gestionUIJeu;

    void Start()
    {
        _gestionUINiv2 = FindObjectOfType<GestionUINiv2>().GetComponent<GestionUINiv2>(); //Trouve l'objet avec le script qui g�n�rera une animation quand la harpe aura �t� trouv�e
        _gestionUIJeu = FindObjectOfType<GestionUIJeu>().GetComponent<GestionUIJeu>(); //Trouve l'objet avec le script qui conserve le score des deux joueurs
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Player1"))
        {
            _gestionUIJeu.AjouterScore(_ptsVictoire, 1);
        }
        else if (other.gameObject.tag.Equals("Player2"))
        {
            _gestionUIJeu.AjouterScore(_ptsVictoire, 2);
        }

        StartCoroutine(_gestionUINiv2.FinNiveau2());
    }
}