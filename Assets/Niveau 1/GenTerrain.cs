using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 Génération du terrain

y [+2 , +5]

x [-3.6 ; 3.6]
 
 
 
 */

public class GenTerrain : MonoBehaviour
{
    public GameObject plateformeDroite;
    public GameObject plateformeGauche;
    public GameObject EndG;
    public GameObject EndD;
    public float levelHeight;
    private const float minPositionX = -3.6f;
    private const float maxPositionX = 3.6f;
    private const float minPositionY = 2f;
    private const float maxPositionY = 5f;

    // Start is called before the first frame update
    void Start()
    {
        float totalHeight = 0;
        do
        {
            Vector2 randomPosition = new Vector2(Random.Range(minPositionX, maxPositionX), Random.Range(minPositionY, maxPositionY)+totalHeight);

            Vector2 randomPosition1 = new Vector2(randomPosition.x - 10f, randomPosition.y);
            Vector2 randomPosition2 = new Vector2(randomPosition.x + 10f, randomPosition.y);


            Instantiate(plateformeGauche, randomPosition1, Quaternion.identity);
            Instantiate(EndG, randomPosition1, Quaternion.identity);
            Instantiate(plateformeDroite, randomPosition2, Quaternion.identity);
            Instantiate(EndD, randomPosition2, Quaternion.identity);

            totalHeight = randomPosition.y;

        } while (totalHeight < levelHeight);

    }
}
