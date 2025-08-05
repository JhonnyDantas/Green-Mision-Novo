using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText; // UI para exibir a pontuação
    private int score = 0; // Pontuação inicial

    // Método para adicionar pontos
    public void AddScore(int points)
    {
        score += points; // Soma os pontos ao total
        UpdateScoreUI(); // Atualiza a UI
    }

    // Atualiza o texto na UI
    private void UpdateScoreUI()
    {
        scoreText.text = $"{score}";
    }
}