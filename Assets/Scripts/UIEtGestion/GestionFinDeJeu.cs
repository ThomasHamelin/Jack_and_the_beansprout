using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GestionFinDeJeu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtGagnant = default;
    [SerializeField] private TextMeshProUGUI _txtTimer = default;
    [SerializeField] private GameObject[] _imagesJoueurs = default;


    private GestionUIJeu _gestionUIJeu;
    private GestionScenes _gestionScene;
    private int _gagnant, tempsRestant;
    private bool _resultAffiches;

    void Start()
    {
        _resultAffiches = false;
        _imagesJoueurs[0].SetActive(false);
        _imagesJoueurs[1].SetActive(false);
        _imagesJoueurs[2].SetActive(false);
        _txtTimer.gameObject.SetActive(false);

        StartCoroutine(RevelationGagnant());

        _gestionScene = FindObjectOfType<GestionScenes>().GetComponent<GestionScenes>();


    }

    void Update()
    {
        //Une fois les résultats affichés, on retourne à la scène de départ quand un joueur appuie sur un bouton
        if (Input.anyKeyDown && _resultAffiches)
        {
            _gestionScene.ChargerSceneDepart();
        }
    }

    IEnumerator RevelationGagnant()
    {
        _txtGagnant.text = "Et le gagnant est...";
        
        _gestionUIJeu = FindObjectOfType<GestionUIJeu>().GetComponent<GestionUIJeu>();
        _gagnant = _gestionUIJeu.ComparerScores();
        Destroy(_gestionUIJeu);

        yield return new WaitForSeconds(3.5f);

        if (_gagnant != 0)
        {
            _txtGagnant.text = "Joueur " + _gagnant;

        }
        else
        {
            _txtGagnant.text = "Égalité";
        }
        
        _imagesJoueurs[_gagnant].SetActive(true);

        StartCoroutine(TimerAvantQuitter());

    }

    IEnumerator TimerAvantQuitter()
    {
        yield return new WaitForSeconds(3f);
        tempsRestant = 30;
        _resultAffiches = true;

        while(tempsRestant >= 0)
        {
            _txtTimer.text = tempsRestant.ToString();
            _txtTimer.gameObject.SetActive(true);
            yield return new WaitForSeconds(.6f);
            _txtTimer.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.4f);
            tempsRestant--;
        }

        _gestionScene.Quitter();

    }

}
