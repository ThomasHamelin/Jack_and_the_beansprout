using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GestionScenes : MonoBehaviour
{

    public Animator fonduAuNoir;

    /*
     * Rôle : Charger la scene suivante
     * Entrée : Aucune
     */
    public IEnumerator ChangerScene()
    {
        fonduAuNoir.SetTrigger("Start"); //Génère l'animation pour rendre les transitions plus smooth
        yield return new WaitForSeconds(1.4f); //Attends la fin de l'animation
        int indexSceneCourante = SceneManager.GetActiveScene().buildIndex; //Trouve le numéro de la scène en cours
        SceneManager.LoadScene(indexSceneCourante + 1); //Charge la scène avec le numéro juste après celui de la scène en cours
    }

    /*
     * Rôle : Aller au premier niveau
     * Entrée : Aucune
     * Sortie : Aucune
     */
    public IEnumerator ChargerSceneDepart()
    {
        fonduAuNoir.SetTrigger("Start"); //Génère l'animation pour rendre les transitions plus smooth
        yield return new WaitForSeconds(1f); //Attends la fin de l'animation
        SceneManager.LoadScene(1); //Charge la scène #1 (Scène des instructions du niveau 1)
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

    //public IEnumerator DonnerDepart()
    //{
    //    _jeuDebute = false;
    //    _txtDonneDepart.gameObject.SetActive(true);
    //    _txtDonneDepart.text = "À vos marques";
    //    yield return new WaitForSeconds(.3f);
    //    _txtDonneDepart.text = "Prêts?";
    //    yield return new WaitForSeconds(.3f);
    //    _txtDonneDepart.text = "Partez!";
    //    _jeuDebute = true;
    //    yield return new WaitForSeconds(.3f);
    //}


}
