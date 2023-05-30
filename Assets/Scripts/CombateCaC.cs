using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateCaC : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float dañoGolpe;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private float tiempoSiguenteAtaque;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }


    private void Update()
    {
        if(tiempoEntreAtaques > 0)
        {
            tiempoSiguenteAtaque -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Q) && tiempoSiguenteAtaque <= 0)
        {
            Golpe();
            tiempoSiguenteAtaque = tiempoEntreAtaques;
        }
    }
    private void Golpe()
    {
        animator.SetTrigger("Golpe");

        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemigo"))
            {
                Enemy enemy = colisionador.transform.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.TomarDaño(dañoGolpe);
                }
                else
                {
                    Jefe jefe = colisionador.transform.GetComponent<Jefe>();
                    if (jefe != null)
                    {
                        jefe.TomarDaño(dañoGolpe);
                    }
                }
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }
}
