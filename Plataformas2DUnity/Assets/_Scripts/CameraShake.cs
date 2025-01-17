using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //private Vector3 originalPosition;

    //private void Awake()
    //{
    //    // Guardamos la posición inicial de la cámara
    //    originalPosition = transform.localPosition;
    //}

    //public IEnumerator Shake(float duration, float magnitude)
    //{
    //    Debug.Log("caca");
    //    float elapsed = 0f;

    //    while (elapsed < duration)
    //    {
    //        float offsetX = Random.Range(-1f, 1f) * magnitude;
    //        float offsetY = Random.Range(-1f, 1f) * magnitude;

    //        // Modificar la posición local para el efecto de shake
    //        transform.localPosition = new Vector3(originalPosition.x + offsetX, originalPosition.y + offsetY, originalPosition.z);

    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }

    //    // Restaurar la posición original de la cámara
    //    transform.localPosition = originalPosition;
    //}
    private CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        // Obtén la referencia al componente CinemachineImpulseSource
        impulseSource = GetComponent<CinemachineImpulseSource>();
        if (impulseSource == null)
        {
            Debug.LogError("No se encontró CinemachineImpulseSource en el GameObject.");
        }
    }

    public void TriggerShake()
    {
        // Genera un impulso con la intensidad dada
        if (impulseSource != null)
        {
            impulseSource.GenerateImpulse(0.2f);
        }
    }


}
