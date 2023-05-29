using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float speed;
    private GameObject player;
    public bool chase = false;
    public Transform startingPoint;


    [SerializeField] private float vida;
    private Animator animator;
    public GameManager gameManager;
    public int valor = 1;

    private SpriteRenderer spriteRenderer;
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;
        if (chase == true)
            Chase();
        else
            ReturnStartPoint();
        Flip();
    }
    public void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
    private void ReturnStartPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, startingPoint.position, speed * Time.deltaTime);
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

        animator.SetTrigger("MuerteVolador");

        // Esperar a que la animación de muerte termine (puedes ajustar el tiempo según la duración de tu animación)
        yield return new WaitForSeconds(0.28f);


        gameManager.SumarPuntos(valor);
        Destroy(gameObject);
    }

    private void Muerte()
    {
        StartCoroutine(MuerteCoroutine());
    }
    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        else
            transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
