using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public float speed;
    public float destroy;

    void Start()
    {
        Destroy(gameObject, destroy);
    }

    void FixedUpdate()
    {
        transform.Translate(transform.right * speed * Time.fixedDeltaTime, Space.World);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Parede")
        {
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Inimigo")
        {
            // Soma os pontos ao destruir o inimigo
            Inimigo inimigo = other.gameObject.GetComponent<Inimigo>();
            if (inimigo != null)
            {
                ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
                if (scoreManager != null)
                {
                    scoreManager.AddScore(inimigo.pontos);
                }
            }

            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}