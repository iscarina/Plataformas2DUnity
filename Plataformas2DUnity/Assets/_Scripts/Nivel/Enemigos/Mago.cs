using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mago : Enemigo
{

    [SerializeField] private GameObject bolaFuego;
    [SerializeField] private Transform puntoSpawn;
    [SerializeField] private float tiempoAtaques;

    private Animator anim;

    new void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
        StartCoroutine(RutinaAtaque());
    }

    IEnumerator RutinaAtaque()
    {
        while (true)
        {
            anim.SetTrigger("atacar");
            yield return new WaitForSeconds(tiempoAtaques);
        }
    }

    private void LanzarBola()
    {
        Instantiate(bolaFuego, puntoSpawn.position, transform.rotation);
    }

    protected override void Perseguir()
    {
    }
}
