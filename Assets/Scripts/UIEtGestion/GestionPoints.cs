using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GestionPoints : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtScoreJ1 = default;
    [SerializeField] private TextMeshProUGUI _txtScoreJ2 = default;

    int _scoreJ1, _scoreJ2;

    void Start()
    {
        _scoreJ1 = 1;
        _scoreJ2 = 0;
        AjouterScore(1, 1);
        AjouterScore(0, 2);
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
            _txtScoreJ1.text = "Points : " + _scoreJ1.ToString();
        }
        else if (p_numJoueur == 2)
        {
            _scoreJ2 += p_points;
            _txtScoreJ2.text = "Points : " + _scoreJ2.ToString();
        }

    }

    public int ComparerScores()
    {
        if (_scoreJ1 < _scoreJ2)
        {
            return 2;
        }
        else if (_scoreJ2 < _scoreJ1)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
