using UnityEngine;

public class MuerteCaida : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("He colisionado");
        var player = collision.collider.GetComponent<CharacterController>();
        if(player != null)
        {
            player.Death();    
        }
    }
}
