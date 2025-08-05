using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float speed; // Velocidade atual do NPC
    private float initialSpeed; // Velocidade original (para restaurar depois)
    private int index; // Índice do ponto atual no caminho
    public List<Transform> paths = new List<Transform>(); // Lista de pontos que o NPC deve seguir

    void Start()
    {
        initialSpeed = speed; // Armazena a velocidade inicial
    }

    void Update()
    {
        // Pausa o movimento se um diálogo estiver sendo exibido
        if (DialogueController.instance.isShowing)
        {
            speed = 0;
        }
        else
        {
            speed = initialSpeed;
        }

        // Move o NPC em direção ao ponto atual
        transform.position = Vector2.MoveTowards(transform.position, paths[index].position, speed * Time.deltaTime);

        // Se estiver próximo o suficiente do ponto, avança para o próximo
        if (Vector2.Distance(transform.position, paths[index].position) < 0.1f)
        {
            if (index < paths.Count - 1)
            {
                index++;
            }
            else
            {
                index = 0; // Reinicia o caminho
            }
        }

        // Define a rotação do NPC baseado na direção do movimento
        Vector2 direction = paths[index].position - transform.position;

        if (direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0); 
        }

        if (direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180); 
        }
    }
}
