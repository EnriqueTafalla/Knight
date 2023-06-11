using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int PuntosTotales { get { return puntosTotales; } }
    private int puntosTotales;
    GameObject menuYouWin;



    private void Start()
    {
        menuYouWin = GameObject.Find("menuYouWin");
        menuYouWin.SetActive(false);
    }
    public void SumarPuntos(int puntosASumar)
    {
        puntosTotales += puntosASumar;
        Debug.Log(puntosTotales);
        if(puntosTotales >= 150)
        {
            Time.timeScale = 0f;
            menuYouWin.SetActive(true);
        }
    }
}
