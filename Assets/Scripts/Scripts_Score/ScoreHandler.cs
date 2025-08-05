using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    public int totalPontos; // Pontos que esse objeto concederá ao ser consertado
    public int hitsParaPontuar = 5; // Número de impactos necessários para pontuar
    public int hitsRecebidos = 0; // Contador de impactos recebidos

    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();

        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager não encontrado na cena!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("C"))
        {
            hitsRecebidos++;

            if (hitsRecebidos >= hitsParaPontuar)
            {
                AdicionarPontuacao();
                hitsRecebidos = 0; // Reseta apenas após pontuar
            }
        }
    }

    private void AdicionarPontuacao()
    {
        if (scoreManager != null)
        {
            scoreManager.AddScore(totalPontos);
            Debug.Log($"Objeto {gameObject.name} somou {totalPontos} pontos.");
        }
    }
}
