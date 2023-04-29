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
        //Trouve l'objet responsable du son
        GestionSon _gestionSon = FindObjectOfType<GestionSon>().GetComponent<GestionSon>();
        
        int indexProchaineScene = SceneManager.GetActiveScene().buildIndex + 1; //Trouve le numéro de la scène en cours
        _gestionSon.ArreterMusique(indexProchaineScene - 1); //Arrete la musique en cours


        fonduAuNoir.SetTrigger("Start"); //Génère l'animation pour rendre les transitions plus smooth
        yield return new WaitForSeconds(1.4f); //Attends la fin de l'animation

        _gestionSon.JouerMusique(indexProchaineScene); //Fait jouer la musique de la scène suivante
        SceneManager.LoadScene(indexProchaineScene); //Charge la scène avec le numéro juste après celui de la scène en cours

        
    }

    /*
     * Rôle : Aller au premier niveau
     * Entrée : Aucune
     * Sortie : Aucune
     */
    public IEnumerator ChargerSceneDepart()
    {
        fonduAuNoir.SetTrigger("Start"); //Génère l'animation pour rendre les transitions plus smooth
        yield return new WaitForSeconds(1.4f); //Attends la fin de l'animation
        SceneManager.LoadScene(0); //Charge la scène #0 (Écran d'accueil)
    }

    public IEnumerator ChargerSceneInstruction1()
    {
        //Trouve l'objet responsable du son
        GestionSon _gestionSon = FindObjectOfType<GestionSon>().GetComponent<GestionSon>();

        _gestionSon.ArreterMusique(_gestionSon._soundtrack.Length - 1); //Arrete la musique en cours
        fonduAuNoir.SetTrigger("Start"); //Génère l'animation pour rendre les transitions plus smooth
        yield return new WaitForSeconds(1.4f); //Attends la fin de l'animation
        _gestionSon.JouerMusique(1); //Fait jouer la musique de la scène 1
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

}
