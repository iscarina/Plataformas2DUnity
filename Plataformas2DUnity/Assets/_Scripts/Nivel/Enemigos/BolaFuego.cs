using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaFuego : MonoBehaviour
{

    Rigidbody2D rb;
    [SerializeField] private float impulsoDisparo;
    [SerializeField] private float danioAtaque;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * impulsoDisparo, ForceMode2D.Impulse);
        Destroy(this.gameObject, 1.5f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerHitBox"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.RecibirDanio(danioAtaque);
            Destroy(this.gameObject);
        }
    }

}
