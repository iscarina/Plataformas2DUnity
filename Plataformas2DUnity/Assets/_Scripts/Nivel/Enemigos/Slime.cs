using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemigo
{

    [SerializeField] private float danioAtaque;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerDetection"))
        {
            
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

}
