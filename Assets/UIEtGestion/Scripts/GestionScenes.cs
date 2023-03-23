using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;


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
        yield return new WaitForSeconds(1f); //Attends la fin de l'animation
        int indexSceneCourante = SceneManager.GetActiveScene().buildIndex; //Trouve le num�ro de la sc�ne en cours
        SceneManager.LoadScene(indexSceneCourante + 1); //Charge la sc�ne avec le num�ro juste apr�s celui de la sc�ne en cours
    }

    /*
     * R�le : Aller au premier niveau
     * Entr�e : Aucune
     * Sortie : Aucune
     */
    public void ChargerSceneDepart()
    {
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
 

}
