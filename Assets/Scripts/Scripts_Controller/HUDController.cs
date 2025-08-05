using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private Image agua; // Referência à imagem da barra de água (UI)

    private PlayerMove playerMove; // Referência ao script do jogador

    void Awake()
    {
        // Procura automaticamente o componente PlayerMove na cena
        playerMove = FindObjectOfType<PlayerMove>();
    }

    void Start()
    {
        agua.fillAmount = 0f; // Começa com a barra de água vazia
    }

    void Update()
    {
        // Atualiza a barra com base na água atual em relação ao máximo
        agua.fillAmount = (float)playerMove.currentWater / playerMove.maxWater;
    }
}
