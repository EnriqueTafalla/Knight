using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CharacterController : MonoBehaviour
{
    public float velocidad;
    public float fuerzaSalto;
   
    public LayerMask capaSuelo;
    

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;
    [SerializeField] private BoxCollider2D colliderMuerte;
    private bool mirandoDerecha = true;
    public Animator animator;


    public BarraDeVida barraDeVida;
   

    RaycastHit2D hit;
    public Vector3 v3;
    public LayerMask layer;
    public float distance;
   

    public bool damage_;
    public int empuje;
    public float HP_Min;
    public float HP_Max;
    public Image barra;
    public int dead;

    GameObject menuGameOverBosque;
    




    private void Start()
    {
        
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        menuGameOverBosque = GameObject.Find("menuGameOverBosque");
        menuGameOverBosque.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position + v3, Vector3.up * -1 * distance);
    }

    // Update is called once per frame
    void Update()
    {
        Vida();
        if (HP_Min > 0)
        {
            Damage();
            if(!damage_)
            { 
       
               Detector_Plataforma();
               ProcesarMovimiento();
               ProcesarSalto();
            }
        }
        else
        {
            switch (dead)
            {
                case 0:
                    Death();
                  
                    break;
            }
        }

    }
    public void Damage()
    {
        if(damage_)
        {
            transform.Translate(Vector3.right * empuje * Time.deltaTime, Space.World);
        }
    }

    public void Death()
    {
        animator.SetTrigger("dead");
        menuGameOverBosque.SetActive(true);
        dead++;
    }

    public void Finish_Damage()
    {
        damage_ = false;
    }

    public void Vida()
    {
        barra.fillAmount = HP_Min / HP_Max;
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

    

    void GestionarOrientacion(float inputmovimiento)
    {
        
        if ((mirandoDerecha == true && inputmovimiento < 0) || (mirandoDerecha == false && inputmovimiento > 0))
        {
            
            mirandoDerecha = !mirandoDerecha;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }

    }
    
}