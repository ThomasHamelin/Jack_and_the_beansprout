using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GestionUINiv1 : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _txtDonneDepart = default;
    [SerializeField] private TextMeshProUGUI _txtPlayer1 = default;
    [SerializeField] private TextMeshProUGUI _txtPlayer2 = default;
    [SerializeField] private GameObject _splitBorder = default;
    [SerializeField] private Camera _camJ1 = default;
    [SerializeField] private Camera _camJ2 = default;

    private Mouvement _joueurs;
    private GestionScenes _gestionScene;
    private int score = 0;

    


    // Start is called before the first frame update
    void Start()
    {
        _splitBorder.SetActive(false);
        StartCoroutine(DonnerDepart());
        _gestionScene = FindObjectOfType<GestionScenes>().GetComponent<GestionScenes>();
    }

    IEnumerator DonnerDepart()
    {
        //Attendre la fin de l'animation de fondu au noir
        _txtDonneDepart.gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);

        //Décompte avant le départ
        _txtDonneDepart.text = "3";
        _txtDonneDepart.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        _txtDonneDepart.text = "2";
        yield return new WaitForSeconds(1f);
        _txtDonneDepart.text = "1";
        yield return new WaitForSeconds(1f);
        _txtDonneDepart.text = "Partez!";
        yield return new WaitForSeconds(1f);

        _txtDonneDepart.gameObject.SetActive(false); //Enlever le texte du décompte
        _splitBorder.SetActive(true);

    }
        // Update is called once per frame
        void Update()
    {
        _txtPlayer1.text = "Player 1 : " + score;
        _txtPlayer2.text = "Player 2 : " + score; 
    }
}
