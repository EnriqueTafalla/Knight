using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jefe : MonoBehaviour
{
    private Animator animator;
    public Rigidbody2D rb2D;
    public Transform jugador;
    private bool mirandoDerecha = true;

    [Header("vida")]

    [SerializeField] private float vida;

    [SerializeField] private BarraDeVidaEnemiga barraDeVidaEnemiga;

    [Header("Ataque")]
    [SerializeField] private Transform controladorAtaque;

    [SerializeField] private float radioAtaque;

    [SerializeField] private float dañoAtaque;

    GameObject menuYouWin;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        barraDeVidaEnemiga.InicializarBarraDeVida(vida);
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        menuYouWin = GameObject.Find("menuYouWin");
        menuYouWin.SetActive(false);

    }

    private void Update()
    {
        float distanciaJugador = Vector2.Distance(transform.position, jugador.position);
        animator.SetFloat("distanciaJugador", distanciaJugador);
    }

    public void TomarDaño(float daño)
    {
        vida -= daño;

        barraDeVidaEnemiga.CambiarVidaActual(vida);
        if(vida <=0)
        {
            animator.SetTrigger("Muerte");
           
        }
    }
    private void Muerte()
    {
        menuYouWin.SetActive(true);
        Destroy(gameObject);
        
    }

    public void MirarJugador()
    {
        if((jugador.position.x> transform.position.x && !mirandoDerecha) || (jugador.position.x < transform.position.x && mirandoDerecha))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
        else
        {
            mirandoDerecha = !mirandoDerecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 0, 0);
        }
    }

    

    public void Ataque()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorAtaque.position, radioAtaque);

        foreach(Collider2D colision in objetos)
        {
            if (colision.CompareTag("Player"))
            {
                colision.GetComponent<CombateJugador>().TomarDaño(dañoAtaque);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorAtaque.position, radioAtaque);
    }

}
