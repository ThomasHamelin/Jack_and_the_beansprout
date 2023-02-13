using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionScene : MonoBehaviour
{
    /*
     * R�le : Charger la scene suivante
     * Entr�e : Aucune
     * Sortie : Aucune
     */
    public void ChangerScene()
    {
        int indexSceneCourante = SceneManager.GetActiveScene().buildIndex;
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
