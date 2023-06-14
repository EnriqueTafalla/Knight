using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlataforma : MonoBehaviour
{

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine("fallDown");
        }
    }

    IEnumerator fallDown()
    {
        yield return new WaitForSeconds(0.80f);
        rb.isKinematic = false;
    }
}
