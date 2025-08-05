using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    Vector2 dirArma;
    float angle;

    [SerializeField] SpriteRenderer SrGan;
    [SerializeField] SpriteRenderer spriteArma;  // Para o sprite da arma
    [SerializeField] SpriteRenderer SrPlayer;    // Para o sprite do jogador

    [SerializeField] float tempoEntreTiros;
    bool podeAtirar = true;

    [SerializeField] Transform pontoDeFogo;
    [SerializeField] GameObject tiro;

    // Referência ao joystick (no caso, Joystick virtual)
    [SerializeField] Joystick joystick; // Atribua o seu joystick virtual aqui!

    void Start()
    {
        // Desativa a arma por padrão
        spriteArma.gameObject.SetActive(false);
    }

    void Update()
    {
        // Se o joystick for movido
        if (dirArma.magnitude > 0.1f && podeAtirar)  // Verifica se o joystick está se movendo
        {
            if (podeAtirar)
            {
                podeAtirar = false;
                Instantiate(tiro, pontoDeFogo.position, pontoDeFogo.rotation);
                Invoke("CDTiro", tempoEntreTiros);
            }
        }
    }

    void FixedUpdate()
    {
        // Pega a direção do joystick
        dirArma = new Vector2(joystick.Horizontal, joystick.Vertical);

        // Se o joystick estiver sendo movido
        if (dirArma.magnitude > 0.1f) // Evita movimentos pequenos ou nulos
        {
            // Calcula o ângulo para a direção do joystick
            angle = Mathf.Atan2(dirArma.y, dirArma.x) * Mathf.Rad2Deg;

            // Aplica a rotação para virar a arma conforme a direção do joystick
            transform.rotation = Quaternion.Euler(0, 0, angle);

            // Determina se o sprite da arma deve ser flipado, baseado no ângulo
            if (angle > 90f || angle < -90f)
            {
                SrGan.flipY = true;   // Virada para trás
                spriteArma.flipY = true; // Alinha o sprite da arma
            }
            else
            {
                SrGan.flipY = false;   // Arma virada para frente
                spriteArma.flipY = false; // Alinha o sprite da arma
            }

            // Verifica se a arma deve ser ativada ou desativada com base no ângulo
            if (angle > -30f && angle < 200f)
            {
                spriteArma.gameObject.SetActive(false);
            }
            else
            {
                spriteArma.gameObject.SetActive(true);
            }
        }
    }

    void CDTiro()
    {
        podeAtirar = true;
    }
}
