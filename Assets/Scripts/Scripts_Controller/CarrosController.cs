using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrosController : MonoBehaviour
{
    public float Speed;

    void FixedUpdate()
    {
        // Move o objeto para a direita com base na velocidade
        transform.position += Vector3.right * Speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Parede"))
        {
            Destroy(gameObject);
        }
    }
}
