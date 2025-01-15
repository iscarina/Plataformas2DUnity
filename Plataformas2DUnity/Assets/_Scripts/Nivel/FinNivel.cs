using UnityEngine;

public class FinNivel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitBox"))
        {
            ControladorNivel controlador = FindObjectOfType<ControladorNivel>();
            controlador.CompletarNivel();
        }
    }
}
