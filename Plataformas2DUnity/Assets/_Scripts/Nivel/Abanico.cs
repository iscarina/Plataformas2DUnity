using UnityEngine;

public class Abanico : MonoBehaviour
{
    [SerializeField] private int abanicoIndex;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerHitBox"))
        {
            ControladorNivel.Instance.RecogerAbanico(abanicoIndex);
            AudioManager.Instance.PlaySFX(AudioManager.SOUNDS[AudioManager.SOUNDS_ENUM.Abanico]);
            Destroy(gameObject);
        }
    }
}
