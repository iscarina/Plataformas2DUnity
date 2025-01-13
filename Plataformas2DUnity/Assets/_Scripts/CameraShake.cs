using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration = 0.5f;  // Duraci�n del temblor de la c�mara
    public float shakeMagnitude = 0.1f; // Magnitud del temblor

    private Vector3 originalPos;  // Posici�n original de la c�mara

    void Start()
    {
        originalPos = transform.position;  // Guarda la posici�n original de la c�mara
    }

    // M�todo para hacer temblar la c�mara
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

        // Restaura la posici�n original de la c�mara despu�s del temblor
        transform.position = originalPos;
    }
}
