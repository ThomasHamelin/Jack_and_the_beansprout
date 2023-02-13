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
     * R�le : Charger la scene suivante
     * Entr�e : Aucune
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
     * R�le : Aller au premier niveau
     * Entr�e : Aucune
     * Sortie : Aucune
     */
    public void ChargerSceneDepart()
    {
        SceneManager.LoadScene(1);
    }

    /*
     * R�le : Quitter le jeu
     * Entr�e : Aucune
     * Sortie : Aucune
     */
    public void Quitter()
    {
        Application.Quit();
    }
}
