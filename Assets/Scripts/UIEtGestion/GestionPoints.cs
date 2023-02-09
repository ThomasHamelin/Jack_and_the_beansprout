using System.Collections;
using System.Collections.Generic;
//////////////
using TMPro;
//////////////
using UnityEngine;



public class GestionPoints : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtScoreJ1 = default;
    [SerializeField] private TextMeshProUGUI _txtScoreJ2 = default;

    int _scoreJ1, _scoreJ2;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject); //Fait en sorte que les points s'accumulent d'une scène à l'autre
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
     * Role : Augmenter le score d'un des joueurs
     * Entree : Un int correspondant au nombre de points a ajouter, un int correspondant au numéro du joueur
     * Sortie : Aucune
     */
    public void AjouterScore(int p_points, int p_numJoueur)
    {
        if (p_numJoueur == 1)
        {
            _scoreJ1 += p_points;
            //_txtScoreJ1 = "Points : " + _scoreJ1;
        }
        else if (p_numJoueur == 2)
        {
            _scoreJ2 += p_points;
           // _txtScoreJ2 = "Points : " + _scoreJ1;
        }

    }
}
