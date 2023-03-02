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
     * R�le : Charger la scene suivante
     * Entr�e : Aucune
     * Sortie : Aucune
     */
    public void ChangerScene()
    {
        Transition.Play("Transition_out");
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

    /*
   * R�le : permet au niveau d'�tre visible une fois qu'il � fini de charger (plus utile pour le deuxi�me niveau; il va prendre quelques secondes � g�n�rer)
   * Entr�e : Aucune
   * Sortie : Aucune
   */
    public void TransitionEntreChargementEtNiveau()
    {
        Transition.SetTrigger("ChargerNouvNiveau");
    }

}
