using UnityEngine;

public class InventoryDrop : MonoBehaviour
{
    public InventoryManager inventoryManager; // Referência ao InventoryManager
    public Transform playerTransform;         // Referência à posição do jogador
    public float distancePlayer = 2f;           // Distância para dropar o item

    public void DropSelectedItem()
    {
        InventoryItem selectedItem = inventoryManager.GetSelectedItem();
        if (selectedItem != null)
        {
            Item item = selectedItem.item;

            // Verifica se o item tem um prefab configurado para ser instanciado na cena
            if (item.prefab != null)
            {
                // Se o item for o regador, a remoção já vai cuidar dos efeitos no InventoryManager
                if (inventoryManager.itemCounts[item] > 1)
                {
                    inventoryManager.itemCounts[item]--;
                    selectedItem.UpdateQuantity(inventoryManager.itemCounts[item]);
                }
                else
                {
                    inventoryManager.RemoveItem(selectedItem);
                }

                // Define a posição para dropar o item perto do jogador
                Vector2 randomOffset = Random.insideUnitCircle.normalized * distancePlayer;
                Vector3 dropPosition = playerTransform.position + new Vector3(randomOffset.x, randomOffset.y, 0);

                Debug.Log($"Dropando {item.name} na posição {dropPosition}");

                // Instancia o item na cena e ativa-o
                GameObject droppedItem = Instantiate(item.prefab, dropPosition, Quaternion.identity);
                droppedItem.name = item.name;
                droppedItem.SetActive(true);

                Debug.Log($"Item {item.name} foi instanciado e ativado na cena.");
            }
            else
            {
                Debug.LogWarning($"O item {item.name} não tem um prefab configurado!");
            }
        }
        else
        {
            Debug.Log("Nenhum item selecionado para dropar.");
        }
    }
}