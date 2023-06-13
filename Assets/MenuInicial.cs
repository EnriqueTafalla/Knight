using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    public void Inicio(int nombre)
    {
        SceneManager.LoadScene(nombre);
    }
}
