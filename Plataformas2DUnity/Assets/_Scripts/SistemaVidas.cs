using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SistemaVidas : MonoBehaviour
{
    [SerializeField] private float vidas;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void RecibirDanio(float danioRecibido)
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
}
