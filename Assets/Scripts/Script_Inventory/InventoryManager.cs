using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    [Header("Referências da Interface e Efeitos")]
    public GameObject ImageRegador;  // Imagem do regador no HUD
    public GameObject agua;          // Objeto da água que deve ser ativada/desativada

    [Header("Inventário")]
    public InventorySlot[] inventorySlots; // Slots do inventário
    public GameObject inventoryItemPrefab;   // Prefab para instanciar itens no inventário

    [HideInInspector] 
    public Dictionary<Item, int> itemCounts = new Dictionary<Item, int>(); // Dicionário para armazenar quantidades

    private InventoryItem selectedItem; // Referência ao item selecionado

    public GameObject inventarioCheio;

    void Start()
    {
        inventarioCheio.SetActive(false);
    }

    // Método para exibir o aviso "Inventário Cheio" por 3 segundos
    void ShowInventarioCheio()
    {
        inventarioCheio.SetActive(true);
        Invoke(nameof(HideInventarioCheio), 3f); // Esconde após 3 segundos
    }

    // Método para selecionar um item no inventário
    public void SelectItem(InventoryItem item)
    {
        selectedItem = item;

        // Se o item for o regador, ativa a interface do regador
        if (item.item.name == "Regador")
        {
            ActivateRegador();
        }
    }

    // Retorna o item atualmente selecionado
    public InventoryItem GetSelectedItem()
    {
        return selectedItem;
    }

    void HideInventarioCheio()
    {
        inventarioCheio.SetActive(false);
    }

    // Adiciona um item ao inventário
    public void AddItem(Item item)
    {
        if (IsInventoryFull()) // Verifica se está cheio ANTES de tentar adicionar
        {
            Debug.Log("Inventário cheio!");
            ShowInventarioCheio(); // Exibe o aviso de inventário cheio
            return;
        }

        foreach (InventorySlot slot in inventorySlots)
        {
            InventoryItem existingItem = slot.GetComponentInChildren<InventoryItem>();
            if (existingItem != null && existingItem.item == item)
            {
                itemCounts[item]++;
                existingItem.UpdateQuantity(itemCounts[item]);
                return;
            }
        }

        foreach (InventorySlot slot in inventorySlots)
        {
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);
                itemCounts[item] = 1;

                // Após adicionar um item, verificar novamente se o inventário ficou cheio
                if (IsInventoryFull())
                {
                    ShowInventarioCheio();
                }
                return;
            }
        }
    }

    // Remove um item do inventário (por exemplo, ao dropar ou jogar no lixo)
    public void RemoveItem(InventoryItem item)
{
    foreach (InventorySlot slot in inventorySlots)
    {
        InventoryItem slotItem = slot.GetComponentInChildren<InventoryItem>();
        if (slotItem == item)
        {
            Destroy(slotItem.gameObject); 
            itemCounts[item.item]--;

            if (itemCounts[item.item] <= 0)
            {
                itemCounts.Remove(item.item);
            }

            HideInventarioCheio();

            if (item.item.name == "Regador")
            {
                DeactivateRegador(); // Desativa a água e o painel
            }

            break;
        }
    }
}


    // Verifica se o inventário está cheio
    public bool IsInventoryFull()
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.GetComponentInChildren<InventoryItem>() == null)
            {
                return false;
            }
        }
        return true;
    }

    // Instancia um novo item em um slot do inventário
    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
        inventoryItem.UpdateQuantity(1);

        if (!itemCounts.ContainsKey(item))
        {
            itemCounts[item] = 1;
        }
    }

    // Método para ativar os efeitos do regador (água e painel)
    public void ActivateRegador()
{
    if (agua != null)
    {
        agua.SetActive(true); // Ativa o efeito de água
    }
    if (ImageRegador != null)
    {
        ImageRegador.SetActive(true); // Ativa o painel do regador
    }
    Debug.Log("Regador coletado: água ativada e painel exibido.");
}


    // Método para desativar os efeitos do regador (água e painel)
    public void DeactivateRegador()
{
    if (agua != null)
    {
        agua.SetActive(false); // Desativa o efeito de água
    }
    if (ImageRegador != null)
    {
        ImageRegador.SetActive(false); // Desativa o painel do regador
    }
    Debug.Log("Regador removido: água e painel desativados.");
}

}
