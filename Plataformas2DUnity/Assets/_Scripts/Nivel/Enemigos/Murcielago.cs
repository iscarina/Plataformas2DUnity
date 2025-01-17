using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Murcielago : Enemigo
{

    public float speed = 3.0f; // Velocidad de movimiento del enemigo.
    public float stopDistance = 2.0f; // Distancia mínima para dejar de acercarse.

    protected override void Perseguir()
    {
            Transform player = GameObject.FindGameObjectWithTag("PlayerHitBox").transform;
            // Calcula la distancia entre el enemigo y el jugador
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            // Si está fuera del rango de "stopDistance", sigue al jugador
            if (distanceToPlayer > stopDistance)
            {
                // Dirección hacia el jugador
                Vector3 direction = (player.position - transform.position).normalized;

                // Mueve al enemigo en esa dirección
                transform.position += direction * speed * Time.deltaTime;

                // Opcional: hacer que el enemigo mire hacia el jugador
                transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
            }
        
    }
}
