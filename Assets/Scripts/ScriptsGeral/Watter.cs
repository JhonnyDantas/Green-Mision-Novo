using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watter : MonoBehaviour
{
    [SerializeField] private int totalWater = 1; // Quantidade de água fornecida
    private bool playerInRange; // Verifica se o player está na área de colisão

    private PlayerMove player; // Referência ao script do player

    void Start()
    {
        player = FindObjectOfType<PlayerMove>();
    }

    // Método chamado pelo botão da UI
    public void CollectWater()
    {
        if (playerInRange) // Só coleta água se o player estiver na área de colisão
        {
            player.AddWater(totalWater);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerDetect")) // Verifica se o objeto é o player
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerDetect")) // Sai da área de colisão
        {
            playerInRange = false;
        }
    }
}