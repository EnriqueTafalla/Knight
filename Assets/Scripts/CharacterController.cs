using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float velocidad;
    public float fuerzaSalto;
    public float fuerzaGolpe;
    public LayerMask capaSuelo;
    

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    private bool mirandoDerecha = true;
    private Animator animator;

    public BarraDeVida barraDeVida;
    public int initialHealth;
    public int actualHealth;
    public int enemyDamage;
    GameObject tryAgainButton;

    RaycastHit2D hit;
    public Vector3 v3;
    public LayerMask layer;
    public float distance;
    

    bool playerAlive = true;

    

    private void Start()
    {
        actualHealth = initialHealth;
        barraDeVida.SetMaxHealth(initialHealth);
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        tryAgainButton = GameObject.Find("TryAgainButton");
        tryAgainButton.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position + v3, Vector3.up * -1 * distance);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAlive)
        {
            Detector_Plataforma();
            ProcesarMovimiento();
            ProcesarSalto();
            Die();
        }
        
    }

    public void Detector_Plataforma()
    {
        if (CheckCollision)
        {
            transform.parent = hit.collider.transform;
        }
        else
        {
            transform.parent = null;
        }
    }
    bool CheckCollision
    {
        get
        {
            hit = Physics2D.Raycast(transform.position + v3, transform.up * -1, distance, layer);
            return hit.collider != null;
        }
    }

    bool EstaEnSuelo()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);
        return raycastHit.collider != null;
    }
    void ProcesarSalto()
    {
        if(Input.GetKeyDown(KeyCode.Space) && EstaEnSuelo())
        {
            rigidBody.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }
    }
    void ProcesarMovimiento()
    {
        
        //Logica de Movimiento
        float inputMovimiento = Input.GetAxis("Horizontal");

        if(inputMovimiento != 0f)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        rigidBody.velocity = new Vector2(inputMovimiento * velocidad, rigidBody.velocity.y);

        GestionarOrientacion(inputMovimiento);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int enemyLayer = LayerMask.NameToLayer("enemy");

        if(collision.gameObject.layer == enemyLayer)
        {
            TakeDamage(enemyDamage);
        }
    }

    void GestionarOrientacion(float inputmovimiento)
    {
        //Si se cumple condicióm
        if ((mirandoDerecha == true && inputmovimiento < 0) || (mirandoDerecha == false && inputmovimiento > 0))
        {
            //Ejecutar codigo de volteado 
            mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }

    }
    public void AplicarGolpe()
    {
        

        Vector2 direccionGolpe;

        if(rigidBody.velocity.x > 0)
        {
            direccionGolpe = new Vector2(-1, 1);
        }
        else
        {
            direccionGolpe = new Vector2(1, 1);
        }

        rigidBody.AddForce(direccionGolpe * fuerzaGolpe);

        StartCoroutine(EsperarYActivarMovimiento());
    }
    IEnumerator EsperarYActivarMovimiento()
    {
        // Esperamos antes de comprobar si esta en el suelo.
        yield return new WaitForSeconds(0.1f);

        while (!EstaEnSuelo())
        {
            // Esperamos al siguiente frame.
            yield return null;
        }

        // Si ya está en suelo activamos el movimiento.
        
    }
    private void Die()
    {
        if (actualHealth <=0)
        {
            animator.SetTrigger("dead");
            playerAlive = false;
            tryAgainButton.SetActive(true);
        }
    }

    private void TakeDamage(int damage)
    {
        actualHealth -= damage;
        barraDeVida.SetHealth(actualHealth);
    }
}