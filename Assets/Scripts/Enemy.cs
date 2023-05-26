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
    [SerializeField] private float vida;
    private Animator animator;
    



    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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

    public void TomarDaño(float daño)
    {
        vida -= daño;
        if (vida <= 0)
        {
            Muerte();

        }
    }
    private IEnumerator MuerteCoroutine()
    {
        // Reproducir la animación de muerte
        animator.SetTrigger("Muerte");

        // Esperar a que la animación de muerte termine (puedes ajustar el tiempo según la duración de tu animación)
        yield return new WaitForSeconds(0.28f);

        // Destruir el objeto del enemigo
        Destroy(gameObject);
    }

    private void Muerte()
    {
        StartCoroutine(MuerteCoroutine());
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

