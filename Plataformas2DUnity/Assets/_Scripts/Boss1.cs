using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    private Animator anim;
    public Rigidbody2D rb;
    public Transform player;

    private bool mirandoDerecha = false;

    [Header("vida")]
    [SerializeField] private float vida;

    [Header("vida")]
    [SerializeField] private Transform controladorAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float danioAtaque;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("PlayerHitBox").GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float distanciaJugador = Vector2.Distance(transform.position, player.position);
        anim.SetFloat("DistanciaJugador", distanciaJugador);
    }

    public void RecibirDanio(float danio)
    {
        vida -= danio;
        if (vida <= 0)
        {
            anim.SetTrigger("Muerte");
        }
        else
        {
            StartCoroutine(FlashRed());
        }
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }

    private void Muerte()
    {
        Destroy(gameObject);
    }

    public void MirarJugador()
    {
        if((player.position.x > transform.position.x && !mirandoDerecha) || (player.position.x < transform.position.x && mirandoDerecha))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
    }

    public void Ataque()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorAtaque.position, radioAtaque);

        foreach (Collider2D c in objetos)
        {
            if (c.CompareTag("PlayerHitBox"))
            {
                CameraShake cameraShake = Camera.main.GetComponent<CameraShake>();
                if (cameraShake != null)
                {
                    cameraShake.TriggerShake();
                }
                c.GetComponent<Player>().RecibirDanio(danioAtaque);
            }   
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerHitBox"))
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(controladorAtaque.position, radioAtaque);
    }

}
