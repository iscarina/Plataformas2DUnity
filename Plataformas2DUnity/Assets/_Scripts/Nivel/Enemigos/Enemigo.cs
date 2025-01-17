using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemigo : MonoBehaviour
{

    [SerializeField] private float vidas;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speedPatrol;
    [SerializeField] private float danioAtaque;

    private Vector3 destinoActual;
    private int indiceActual = 0;

    protected void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (waypoints.Length > 0)
        {
            destinoActual = waypoints[indiceActual].position;
            StartCoroutine(Patrol());
        }
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            while (transform.position != waypoints[indiceActual].position)
            {
                transform.position = Vector3.MoveTowards(transform.position, waypoints[indiceActual].position, speedPatrol * Time.deltaTime);
                yield return null;
            }
            DefinirNuevoDestino();
        }

    }

    private void DefinirNuevoDestino()
    {
        indiceActual++;
        if (indiceActual >= waypoints.Length)
        {
            indiceActual = 0;
        }
        destinoActual = waypoints[indiceActual].position;
        EnfocarDestino();
    }

    private void EnfocarDestino()
    {
        if (destinoActual.x > transform.position.x)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerDetection"))
        {
            Perseguir();
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

    public void TakeDamage(float danioRecibido)
    {
        vidas -= danioRecibido;
        StartCoroutine(FlashRed());
        if (vidas <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    protected abstract void Perseguir();

}
