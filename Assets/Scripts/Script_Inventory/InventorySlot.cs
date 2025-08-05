using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public bool HasItem() // Verifica se o slot já possui um item
    {
        return GetComponentInChildren<InventoryItem>() != null;
    }
}