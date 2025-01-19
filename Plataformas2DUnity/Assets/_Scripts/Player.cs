using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Otros elementos")]
    [SerializeField] private float vida;
    [SerializeField] private TextMeshProUGUI lifeTMP;

    [Header("Sistema Movimiento")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private LayerMask queEsSaltable;
    [SerializeField] private float distanciaDeteccionSuelo;
    [SerializeField] private Transform pies;

    [Header("Sistema Ataque")]
    [SerializeField] private Transform puntoAtaque;
    [SerializeField] private float radioAtaque;
    [SerializeField] private LayerMask queEsDaniable;
    [SerializeField] private float danioAtaque;

    [Header("Sistema Dash")]
    private bool canDash = true;
    private bool isDashing;
    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;
    [SerializeField] private TrailRenderer tr;


    [SerializeField] private GameObject GameOver;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private float inputH;

    private Animator anim;

    void Start()
    {
        lifeTMP.text = "Life: " + vida;

        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDashing)
        {
            return;
        }

        Movement();
        Jump();
        LanzarAtaque();
        StartCoroutine(Dash());
    }

    void Movement()
    {
        inputH = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(inputH * speed, rb.velocity.y);

        if (inputH != 0)
        {
            anim.SetBool("running", true);
            if (inputH > 0)
            {
                transform.eulerAngles = Vector3.zero;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        else
        {
            anim.SetBool("running", false);
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && EstoyEnSuelo())
        {
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            anim.SetTrigger("jump");
        }
    }

    private bool EstoyEnSuelo()
    {
        return Physics2D.Raycast(pies.position, Vector3.down, distanciaDeteccionSuelo, queEsSaltable);
    }

    void LanzarAtaque()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AudioManager.Instance.PlaySFX(AudioManager.SOUNDS[AudioManager.SOUNDS_ENUM.Latigo]);
            anim.SetTrigger("attack");
        }
    }

    //Se ejecuta desde evento de animación
    void Ataque()
    {
        Collider2D[] collidersTocados = Physics2D.OverlapCircleAll(puntoAtaque.position, radioAtaque, queEsDaniable);

        foreach (Collider2D c in collidersTocados)
        {
            
            if (c.CompareTag("BOSS"))
            {
                Boss1 boss = c.gameObject.GetComponent<Boss1>();
                boss.RecibirDanio(danioAtaque);
            }
            else
            {
                Enemigo e = c.gameObject.GetComponent<Enemigo>();
                e.TakeDamage(danioAtaque);
            }
            
        }
    }

    private IEnumerator Dash()
    {
        if ( Input.GetMouseButtonDown(1) && canDash)
        {
            AudioManager.Instance.PlaySFX(AudioManager.SOUNDS[AudioManager.SOUNDS_ENUM.Dash]);
            canDash = false;
            isDashing = true;
            float originalGravity = rb.gravityScale;
            gameObject.GetComponent<Collider2D>().enabled = false;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(transform.right.x * dashingPower, 0f);
            tr.emitting = true;
            yield return new WaitForSeconds(dashingTime);
            tr.emitting = false;
            rb.gravityScale = originalGravity;
            gameObject.GetComponent<Collider2D>().enabled = true;
            isDashing = false;
            yield return new WaitForSeconds(dashingCooldown);
            canDash = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(puntoAtaque.position, radioAtaque);
    }

    public void RecibirDanio(float danioRecibido)
    {
        vida -= danioRecibido;
        lifeTMP.text = "Life: " + vida;
        AudioManager.Instance.PlaySFX(AudioManager.SOUNDS[AudioManager.SOUNDS_ENUM.HitPlayer]);
        StartCoroutine(FlashRed());
        if (vida <= 0)
        {
            Destroy(this.gameObject);
            GameOver.SetActive(true);
        }
    }

    private IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;
    }


}
