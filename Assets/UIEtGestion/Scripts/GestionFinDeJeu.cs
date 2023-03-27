using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GestionFinDeJeu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtGagnant = default;
    [SerializeField] private TextMeshProUGUI _txtRejouer = default;
    [SerializeField] private TextMeshProUGUI _txtTimer = default;
    [SerializeField] private GameObject _imagesGagnant = default;


    private GestionUIJeu _gestionUIJeu;
    private GestionScenes _gestionScene;
    private int _gagnant, tempsRestant;
    private bool _resultAffiches;

    void Start()
    {
        _resultAffiches = false;
        _imagesGagnant.SetActive(false);
        _txtRejouer.gameObject.SetActive(false);
        _txtTimer.gameObject.SetActive(false);

        StartCoroutine(RevelationGagnant());

        _gestionScene = FindObjectOfType<GestionScenes>().GetComponent<GestionScenes>();


    }

    void Update()
    {
        //Une fois les résultats affichés, on retourne à la scène de départ quand un joueur appuie sur un bouton
        if (Input.anyKeyDown && _resultAffiches)
        {
            StartCoroutine(_gestionScene.ChargerSceneDepart());
        }
    }

    /*
     * Rôle : Créer un délai avant d'afficher le résultat
     * Entrée : Aucune
     */
    IEnumerator RevelationGagnant()
    {
        _txtGagnant.text = "Et le gagnant est...";
        
        //On compare les points en faisant appel au script gestionUIJeu
        _gestionUIJeu = FindObjectOfType<GestionUIJeu>().GetComponent<GestionUIJeu>();
        _gagnant = _gestionUIJeu.ComparerScores();
        Destroy(_gestionUIJeu); //On détruit le UI pour ne pas qu'il soit en double si le jeu recommence

        yield return new WaitForSeconds(5.5f); //Délai pour suspense

        if (_gagnant != 0) //Si ce n'est pas une égalité
        {
            _txtGagnant.text = "Joueur " + _gagnant; //Affiche le numéro du gagnant

        }
        else //Si égalité
        {
            _txtGagnant.text = "Égalité"; //Indique qu'il y a égalité
        }
        
        _imagesGagnant.SetActive(true); //Générer l'image correspondant au bon gagnant
                                                  
        yield return new WaitForSeconds(3f); //On laisse les résultats affichés pendant quelques secondes

        StartCoroutine(TimerAvantQuitter()); //Commencer le timer avant de quitter

    }

    /*
     * Rôle : Gérer et afficher le temps qu'il reste avant que le programme se ferme et faire clignoter le texte qui indique le temps restant
     * Entrée : Aucune
     */
    IEnumerator TimerAvantQuitter()
    {
        tempsRestant = 30;
        _txtRejouer.gameObject.SetActive(true); //On affiche le texte pour demander aux joueurs s'ils veulent rejouer
        _resultAffiches = true; //On peut maintenant recommencer le jeu

        while(tempsRestant >= 0) //Tant qu'il reste du temps
        {
            _txtTimer.text = tempsRestant.ToString(); //On affiche le temps qui reste

            //On fait clignoter le texte
            _txtTimer.gameObject.SetActive(true);
            yield return new WaitForSeconds(.6f);
            _txtTimer.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.4f);
            tempsRestant--;
        }

        //Si le temps est écoulé et qu'on n'a pas recommencer le jeu
        _gestionScene.Quitter(); //On quitte le jeu

    }

}
