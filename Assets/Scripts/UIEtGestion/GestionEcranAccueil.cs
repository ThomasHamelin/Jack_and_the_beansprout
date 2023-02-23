using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GestionEcranAccueil : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtDebuter = default;
    [SerializeField] private GameObject _gestionScene = default;
   
    void Start()
    {
        StartCoroutine(ClignotementTextDepart());
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            _gestionScene.SetActive(false);
            _gestionScene.GetComponent<GestionScene>().ChangerScene();
            Destroy(this);
        }
    }

    IEnumerator ClignotementTextDepart()
    {
        while (true)
        {
            _txtDebuter.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.6f);
            _txtDebuter.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.4f);
        }
    }
}
