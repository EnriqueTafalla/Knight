using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEnemigo2D : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<CharacterController>().HP_Min > 0)
            {

            
                collision.GetComponent<CharacterController>().animator.SetTrigger("damage");
                collision.GetComponent<CharacterController>().damage_ = true;

                if (transform.position.x > collision.transform.position.x)
                {
                    collision.GetComponent<CharacterController>().empuje = -30;
                    collision.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    collision.GetComponent<CharacterController>().empuje = 30;
                    collision.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                collision.GetComponent<CharacterController>().HP_Min -= 10;
            }
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
