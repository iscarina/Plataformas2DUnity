using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadManoBOSS : MonoBehaviour
{

    [SerializeField] private float danioMano;
    [SerializeField] private Vector2 dimensionesCaja;
    [SerializeField] private Transform posicionCaja;
    [SerializeField] private float tiempoVida;

    void Start()
    {
        Destroy(gameObject, tiempoVida);
    }

    public void Golpe()
    {
        Collider2D[] objetos = Physics2D.OverlapBoxAll(posicionCaja.position, dimensionesCaja, 0f);
        AudioManager.Instance.PlaySFX(AudioManager.SOUNDS[AudioManager.SOUNDS_ENUM.BossMano]);
        foreach (Collider2D c in objetos)
        {
            if (c.CompareTag("PlayerHitBox"))
            {
                CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();
                if (cameraShake != null)
                {
                    cameraShake.TriggerShake();
                }
                c.GetComponent<Player>().RecibirDanio(danioMano);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(posicionCaja.position, dimensionesCaja);
    }

}
