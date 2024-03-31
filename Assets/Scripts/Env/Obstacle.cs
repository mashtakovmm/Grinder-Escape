using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    GameManager gameManager;
    float speed = 1f;
    float diffScale;
    Rigidbody2D rb;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 20f);
    }

    void Update()
    {
        diffScale = gameManager.DiffScale;
        float diff = diffScale;
        if (diff > 5)
        {
            diff = 5f;
        }
        rb.velocity = new Vector2(0, -1 - diff) * speed;
        Debug.Log(rb.velocity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Enemy")
            Destroy(gameObject);
    }
}
