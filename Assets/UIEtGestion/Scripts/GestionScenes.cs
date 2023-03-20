using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;


public class GestionScenes : MonoBehaviour
{
    //public GameObject Transition;

    private void Start()
    {
       // Transition = GameObject.FindGameObjectWithTag("Transition").GetComponent<Animator>();
    }
    /*
     * R�le : Charger la scene suivante
     * Entr�e : Aucune
     * Sortie : Aucune
     */
    public void ChangerScene()
    {
        
        //Debug.Log("Decharge");
        int indexSceneCourante = SceneManager.GetActiveScene().buildIndex;
        //Transition.SetTrigger("DechargeNiveau");
        SceneManager.LoadScene(indexSceneCourante + 1);
        //Debug.Log("charge");
        //Transition.SetTrigger("ChargerNouvNiveau");

        //Transition.ResetTrigger("ChargerNouvNiveau");
        //Transition.ResetTrigger("DechargeNiveau");
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
 

}
