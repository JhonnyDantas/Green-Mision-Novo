using UnityEngine;

public class Regador : MonoBehaviour
{
    [Header("Configuração do Regador")]
    private Item regadorItem;  // Configure este campo no Inspector com o item que representa o regador

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Procura o InventoryManager no jogador (certifique-se de que o jogador possua esse componente)
            InventoryManager inventoryManager = other.GetComponent<InventoryManager>();
            if (inventoryManager != null)
            {
                inventoryManager.AddItem(regadorItem);
                Debug.Log("Regador coletado: item adicionado ao inventário.");
            }
            else
            {
                Debug.LogWarning("InventoryManager não encontrado no jogador!");
            }
            
            // Destrói o objeto do mundo após a coleta
            Destroy(gameObject);
        }
    }
}