using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationTresors : MonoBehaviour
{
    //[SerializeField] private Tresor _tresors;
    //[SerializeField] private Harpe _harpe;
    //[SerializeField] private Geant _geant;
    [SerializeField] private int _frequenceTresors;
    [SerializeField] private float CoordonneDepartX;
    [SerializeField] float CoordonneDepartY;

    float longueur;
    float largeur;
    int tailleGrille;
    float _positionGeantX;
    float _positionGeantY;
    float _tailleCube;
    int nombreAleatoire;
    float x, y;

    void Start()
    {
        genererCases();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void genererCases()
    {   
        _tailleCube = longueur / tailleGrille;
        _positionGeantX = ((float)Random.Range(0, tailleGrille)) * _tailleCube + CoordonneDepartX;
        _positionGeantY = ((float)Random.Range(0, tailleGrille)) * _tailleCube + CoordonneDepartY;
        x = CoordonneDepartX;
        y = CoordonneDepartY;
        while (x < longueur)
        {
            while (y < largeur)
            {
                if(x > (longueur - (2 * _tailleCube)) && y > (largeur - (2 * _tailleCube))) //Si position au coin opposé
                {
                    Vector3 position = new Vector3(x + CoordonneDepartX, y + CoordonneDepartY);
                    //var tresorGenere = Instantiate(_harpe, position, Quaternion.Identity());
                }
                else if(x == _positionGeantX && y == _positionGeantY)
                {
                    //var tresorGenere = Instantiate(_harpe, new Vector3(_positionGeantX, _positionGeantY), Quaternion.Identity());
                }
                else
                {
                    nombreAleatoire = Random.Range(0, 10);
                    if(nombreAleatoire < _frequenceTresors)
                    {
                        Vector3 position = new Vector3(x + CoordonneDepartX, y + CoordonneDepartY);
                        //var tresorGenere = Instantiate(_tresors, position, Quaternion.Identity());
                       // tresorGenere.name = $"Tresor ({x}, {y})";
                    }
                }
                x += _tailleCube;
                y += _tailleCube;
            }
        }
    }
}
