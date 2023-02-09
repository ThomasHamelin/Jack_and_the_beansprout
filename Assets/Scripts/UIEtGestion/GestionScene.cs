using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionScene : MonoBehaviour
{
    /*
     * Rôle : Charger la scene suivante
     * Entrée : Aucune
     * Sortie : Aucune
     */
    public void ChangerScene()
    {
        int indexSceneCourante = SceneManager.GetActiveScene().buildIndex;
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
