using UnityEngine;

public class Estrella : MonoBehaviour
{
    [SerializeField] private int abanicoIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitBox"))
        {
            ControladorNivel controlador = FindObjectOfType<ControladorNivel>();
            Debug.Log(abanicoIndex);
            controlador.RecogerAbanico(abanicoIndex);

            Destroy(gameObject);
        }
    }
}
