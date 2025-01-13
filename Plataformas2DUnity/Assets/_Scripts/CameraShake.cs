using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.5f;  // Duración del temblor de la cámara
    public float shakeMagnitude = 0.1f; // Magnitud del temblor

    private Vector3 originalPos;  // Posición original de la cámara

    void Start()
    {
        originalPos = transform.position;  // Guarda la posición original de la cámara
    }

    // Método para hacer temblar la cámara
    public void ShakeCamera()
    {
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            // Calcula un desplazamiento aleatorio dentro del rango especificado
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.position = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Restaura la posición original de la cámara después del temblor
        transform.position = originalPos;
    }
}
