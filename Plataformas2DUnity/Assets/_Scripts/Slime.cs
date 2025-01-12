using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{

    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speedPatrol;
    [SerializeField] private float danioAtaque;

    private Vector3 destinoActual;
    private int indiceActual = 0;

    void Start()
    {
        destinoActual = waypoints[indiceActual].position;
        StartCoroutine(Patrol());
    }

    void Update()
    {
        
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

        }
        else if (collision.gameObject.CompareTag("PlayerHitBox"))
        {
            SistemaVidas sistemaVidasPlayer = collision.gameObject.GetComponent<SistemaVidas>();
            sistemaVidasPlayer.RecibirDanio(danioAtaque);
        }
    }

}
