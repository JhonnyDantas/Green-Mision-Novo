using UnityEngine;

public class CollectedItem : MonoBehaviour
{
    public Item item; // Referência ao item (deve ser configurado no Inspector)

    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player")) 
    {           
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();

        if (inventoryManager != null && item != null)
        {
            if (inventoryManager.IsInventoryFull()) // Impede a coleta se o inventário estiver cheio
            {
                Debug.Log("Inventário cheio! Não pode coletar mais itens.");
                return;
            }

            inventoryManager.AddItem(item);
            Debug.Log($"Item {item.name} adicionado ao inventário!");
            Destroy(gameObject); 
        }
    }
}

}
