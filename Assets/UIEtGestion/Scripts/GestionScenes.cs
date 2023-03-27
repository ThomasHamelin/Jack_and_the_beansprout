using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GestionScenes : MonoBehaviour
{

    public Animator fonduAuNoir;

    /*
     * R�le : Charger la scene suivante
     * Entr�e : Aucune
     */
    public IEnumerator ChangerScene()
    {
        fonduAuNoir.SetTrigger("Start"); //G�n�re l'animation pour rendre les transitions plus smooth
        yield return new WaitForSeconds(1.4f); //Attends la fin de l'animation
        int indexSceneCourante = SceneManager.GetActiveScene().buildIndex; //Trouve le num�ro de la sc�ne en cours
        SceneManager.LoadScene(indexSceneCourante + 1); //Charge la sc�ne avec le num�ro juste apr�s celui de la sc�ne en cours
    }

    /*
     * R�le : Aller au premier niveau
     * Entr�e : Aucune
     * Sortie : Aucune
     */
    public IEnumerator ChargerSceneDepart()
    {
        fonduAuNoir.SetTrigger("Start"); //G�n�re l'animation pour rendre les transitions plus smooth
        yield return new WaitForSeconds(1f); //Attends la fin de l'animation
        SceneManager.LoadScene(1); //Charge la sc�ne #1 (Sc�ne des instructions du niveau 1)
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

    //public IEnumerator DonnerDepart()
    //{
    //    _jeuDebute = false;
    //    _txtDonneDepart.gameObject.SetActive(true);
    //    _txtDonneDepart.text = "� vos marques";
    //    yield return new WaitForSeconds(.3f);
    //    _txtDonneDepart.text = "Pr�ts?";
    //    yield return new WaitForSeconds(.3f);
    //    _txtDonneDepart.text = "Partez!";
    //    _jeuDebute = true;
    //    yield return new WaitForSeconds(.3f);
    //}


}
