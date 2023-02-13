using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GestionScene : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI _txtReadyJ1 = default;
    //[SerializeField] private TextMeshProUGUI _txtReadyJ2 = default;
    [SerializeField] private GameObject _uiInstruct = default;

    /*
     * Rôle : Charger la scene suivante
     * Entrée : Aucune
     * Sortie : Aucune
     */
    public void ChangerScene()
    {
        int indexSceneCourante = SceneManager.GetActiveScene().buildIndex;
        
        switch(indexSceneCourante)
        {
            case 0:
                _uiInstruct.SetActive(true);
                break;
        }

        SceneManager.LoadScene(indexSceneCourante + 1);
    }

    /*
     * Rôle : Aller au premier niveau
     * Entrée : Aucune
     * Sortie : Aucune
     */
    public void ChargerSceneDepart()
    {
        SceneManager.LoadScene(1);
    }

    /*
     * Rôle : Quitter le jeu
     * Entrée : Aucune
     * Sortie : Aucune
     */
    public void Quitter()
    {
        Application.Quit();
    }
}
