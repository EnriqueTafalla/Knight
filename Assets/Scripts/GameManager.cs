using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int PuntosTotales { get { return puntosTotales; } }
    private int puntosTotales;
    GameObject youwinbutton;


    private void Start()
    {
        youwinbutton = GameObject.Find("YouWinButton");
        youwinbutton.SetActive(false);
    }
    public void SumarPuntos(int puntosASumar)
    {
        puntosTotales += puntosASumar;
        Debug.Log(puntosTotales);
        if(puntosTotales >= 200)
        {
            youwinbutton.SetActive(true);
        }
    }
}
