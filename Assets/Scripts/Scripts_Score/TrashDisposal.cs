using UnityEngine;
using UnityEngine.EventSystems;

public class TrashDisposal : MonoBehaviour, IDropHandler
{
    public ScoreManager scoreManager; // Referência ao ScoreManager

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem item = eventData.pointerDrag.GetComponent<InventoryItem>();

        // Verifica se este objeto possui a tag "JogarLixo" (ou use a lógica desejada)
        if (item != null && CompareTag("JogarLixo"))
        {
            // Calcula a pontuação com base na quantidade do item
            InventoryManager invManager = FindObjectOfType<InventoryManager>();
            int itemQuantity = invManager.itemCounts[item.item];
            int itemScore = item.item.scoreValue; // Pontuação individual do item
            int totalPoints = itemQuantity * itemScore;

            scoreManager.AddScore(totalPoints); // Adiciona os pontos ao ScoreManager

            // Remove o item do inventário; se for o regador, o InventoryManager desativa os efeitos
            invManager.RemoveItem(item);

            // Se o item removido for o regador, chama DeactivateRegador para desativar a água e o painel
            if (item.item.name == "Regador")
            {
                invManager.DeactivateRegador();  // Desativa os efeitos
            }

            Debug.Log($"{itemQuantity}x {item.item.name} jogados no lixo! Pontos ganhos: {totalPoints}");
        }
    }
}
