using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemigo : MonoBehaviour
{

    [SerializeField] private float vidas;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Transform[] waypoints;
    [SerializeField] protected float speedPatrol;

    private Vector3 destinoActual;
    private int indiceActual = 0;
    public bool perseguir = false;

    protected void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (waypoints.Length > 0)
        {
            destinoActual = waypoints[indiceActual].position;
            StartCoroutine(Patrol());
        }
    }

    protected IEnumerator Patrol()
    {
        while (!perseguir)
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

    public void TakeDamage(float danioRecibido)
    {
        vidas -= danioRecibido;
        AudioManager.Instance.PlaySFX(AudioManager.SOUNDS[AudioManager.SOUNDS_ENUM.HitEnemy]);
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

}
