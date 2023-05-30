using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateJugador : MonoBehaviour
{

    [SerializeField] private float vida;

    [SerializeField] private float maximoVida;

    [SerializeField] private LifeBar lifeBar;
    public Animator animator;




    //[SerializeField] private BarraDeVida barraDeVida;

    void Start()
    {
        vida = maximoVida;
        lifeBar.InicializarBarraDeVida(vida);
        animator = GetComponent<Animator>();


    }

    public void TomarDaño(float daño)
    {
        vida -= daño;
        lifeBar.CambiarVidaActual(vida);
        if(vida <=0)
        {
            animator.SetTrigger("dead");
            Destroy(gameObject);
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
