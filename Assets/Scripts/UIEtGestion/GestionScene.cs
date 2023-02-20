using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GestionScene : MonoBehaviour
{
    [SerializeField] private GameObject _imageReadyJ1 = default;
    [SerializeField] private GameObject _imageReadyJ2 = default;
    //[SerializeField] private GameObject _uiInstruct = default;


    /*
     * R�le : Charger la scene suivante
     * Entr�e : Aucune
     * Sortie : Aucune
     */
    public void ChangerScene()
    {
        int indexSceneCourante = SceneManager.GetActiveScene().buildIndex;
        
        if(indexSceneCourante < 3)
        {
            coroutineInstruct(indexSceneCourante);
        }
        else
        {
            //SceneManager.LoadScene(indexSceneCourante + 1);
        }

        
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

    private void coroutineInstruct(int p_indexSceneCourante)
    {
        bool joueur1Pret = false;
        bool joueur2Pret = false;
        _imageReadyJ1.GetComponent<Image>().color = Color.red;
        _imageReadyJ2.GetComponent<Image>().color = Color.red;
        _imageReadyJ1.SetActive(true);
        _imageReadyJ2.SetActive(true);

        
            if (Input.GetKeyDown("space"))
            {
                joueur1Pret = true;
                _imageReadyJ1.GetComponent<Image>().color = Color.green;
            }
            if(Input.GetKeyDown("escape"))
            {
                joueur2Pret = true;
                _imageReadyJ2.GetComponent<Image>().color = Color.green;
            }


    }
    
}
