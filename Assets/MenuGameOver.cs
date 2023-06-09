using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class MenuGameOver : MonoBehaviour
{
    [SerializeField] private GameObject menuGameOver;
    private CombateJugador combateJugador;

    

    private void Start()
    {
        combateJugador = GameObject.FindGameObjectWithTag("Player").GetComponent<CombateJugador>();
        combateJugador.MuerteJugador += ActivarMenu;
    }

    private void ActivarMenu(object sender, EventArgs e)
    {
        menuGameOver.SetActive(true);
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuInicial(int nombre)
    {
        SceneManager.LoadScene(nombre);
    }
}
