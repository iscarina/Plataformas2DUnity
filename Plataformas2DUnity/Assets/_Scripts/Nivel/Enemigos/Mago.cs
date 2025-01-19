using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mago : Enemigo
{

    [SerializeField] private GameObject bolaFuego;
    [SerializeField] private Transform puntoSpawn;
    [SerializeField] private float tiempoAtaques;

    [SerializeField] private float danioAtaque;

    private bool mirandoDerecha = true;
    private bool lanzarBolas = false;

    private Transform player;

    private Animator anim;

    new void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("PlayerHitBox").GetComponent<Transform>();
        anim = GetComponent<Animator>();
    }

    IEnumerator RutinaAtaque()
    {
        while (lanzarBolas)
        {
            MirarJugador();
            anim.SetTrigger("atacar");
            yield return new WaitForSeconds(tiempoAtaques);
        }
    }

    private void LanzarBola()
    {
        Instantiate(bolaFuego, puntoSpawn.position, transform.rotation);
        AudioManager.Instance.PlaySFX(AudioManager.SOUNDS[AudioManager.SOUNDS_ENUM.BossAtt]);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerDetection"))
        {
            lanzarBolas = true;
            StartCoroutine(RutinaAtaque());
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
            lanzarBolas = false;
        }   
    }

    public void MirarJugador()
    {
        if ((player.position.x > transform.position.x && !mirandoDerecha) || (player.position.x < transform.position.x && mirandoDerecha))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
    }

}
