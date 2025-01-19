using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieLine : MonoBehaviour
{

    [SerializeField] private GameObject GameOver;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerDetection"))
        {
            Destroy(collision.gameObject);
            GameOver.SetActive(true);
        }
    }
}
