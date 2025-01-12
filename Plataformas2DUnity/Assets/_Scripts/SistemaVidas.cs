using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SistemaVidas : MonoBehaviour
{
    [SerializeField] private float vidas;

    public void RecibirDanio(float danioRecibido)
    {
        vidas -= danioRecibido;
        if(vidas <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
