using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GestionFinDeJeu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtGagnant = default;
   // [SerializeField] private GameObject[] _imagesJoueurs = default;


    private GestionPoints _gestionPoints;
    private GestionScenes _gestionScene;
    private int _gagnant;

    void Start()
    {
        //for(int i(0); i < _imagesJoueurs; i++)
        //{
        //    _imagesJoueurs[i].SetActive(false);
        //}

        StartCoroutine(RevelationGagnant());

        _gestionScene = FindObjectOfType<GestionScenes>().GetComponent<GestionScenes>();

        
    }

    void Update()
    {
        
    }

    IEnumerator RevelationGagnant()
    {
        _txtGagnant.text = "Et le gagnant est...";
        
        _gestionPoints = FindObjectOfType<GestionPoints>().GetComponent<GestionPoints>();
        _gagnant = _gestionPoints.ComparerScores();
        Destroy(_gestionPoints.gameObject);

        yield return new WaitForSeconds(3.5f);

        if (_gagnant != 0 )
        {
            _txtGagnant.text = "Joueur " + _gagnant;
        }
        else
        {
            _txtGagnant.text = "Égalité";
        }

    }

}
