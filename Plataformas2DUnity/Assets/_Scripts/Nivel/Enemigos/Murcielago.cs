using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Murcielago : Enemigo
{
    private Transform player;

    [SerializeField] private float danioAtaque;

    private new void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("PlayerHitBox").GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerDetection"))
        {
            perseguir = true;
            StartCoroutine(Perseguir());
        }
        else if (collision.gameObject.CompareTag("PlayerHitBox"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.RecibirDanio(danioAtaque);
            CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();
            if (cameraShake != null)
            {
                cameraShake.TriggerShake();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerDetection"))
        {
            perseguir = false;
            StartCoroutine(Patrol());
        }
    }

    private IEnumerator Perseguir()
    {

        while (perseguir)
        {
           Vector3 direction = (player.position - transform.position).normalized;
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.MovePosition(rb.position + (Vector2)direction * speedPatrol * Time.deltaTime);

            yield return null;
        }

    }

}
