using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadJefe : MonoBehaviour
{

    [SerializeField] private float da�o;

    [SerializeField] private Vector2 dimensionesCaja;

    [SerializeField] private Transform posicionCaja;

    [SerializeField] private float tiempoDevida;

    void Start()
    {
        Destroy(gameObject, tiempoDevida);
    }

    public void Golpe()
    {
        Collider2D[] objetos = Physics2D.OverlapBoxAll(posicionCaja.position, dimensionesCaja, 0f);

        foreach(Collider2D colisiones in objetos)
         {
            if (colisiones.CompareTag("Player"))
            {
                colisiones.GetComponent<CombateJugador>().TomarDa�o(da�o);
            }
         }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(posicionCaja.position, dimensionesCaja);
    }
}
