using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D enemyRigidbody;
    private Transform currentPoint;
    public float speed;

    private bool puedeAtacar = true;
    public float cooldownAtaque;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyRigidbody = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            enemyRigidbody.velocity = new Vector2(speed, 0);
        }
        else
        {
            enemyRigidbody.velocity = new Vector2(-speed, 0);
        }

        if(Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            flip();
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            flip();
            currentPoint = pointB.transform;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Si no puede atacar salimos de la función.
            if (!puedeAtacar) return;

            // Desactivamos el ataque.
            puedeAtacar = false;
            collision.gameObject.GetComponent<CharacterController>().AplicarGolpe();

            // Cambiamos la opacidad del sprite.
            Color color = spriteRenderer.color;
            color.a = 0.5f;
            spriteRenderer.color = color;

            Invoke("ReactivarAtaque", cooldownAtaque);
        }
    }

    void ReactivarAtaque()
    {
        puedeAtacar = true;

        // Cambiamos la opacidad del sprite.
        Color c = spriteRenderer.color;
        c.a = 1f;
        spriteRenderer.color = c;
    }
    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }




}

