using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;


public class GestionScenes : MonoBehaviour
{
    



    public Animator Transition;



    private void Start()
    {
        Transition = GameObject.Find("transition").GetComponent<Animator>();
        
    }
    /*
     * Rôle : Charger la scene suivante
     * Entrée : Aucune
     * Sortie : Aucune
     */
    public void ChangerScene()
    {
        Transition.Play("Transition_out");
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

    /*
   * Rôle : permet au niveau d'être visible une fois qu'il à fini de charger (plus utile pour le deuxième niveau; il va prendre quelques secondes à générer)
   * Entrée : Aucune
   * Sortie : Aucune
   */
    public void TransitionEntreChargementEtNiveau()
    {
        Transition.SetTrigger("ChargerNouvNiveau");
    }

}
