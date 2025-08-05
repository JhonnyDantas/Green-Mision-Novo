using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDetailPanel : MonoBehaviour
{
    public Image itemImage; // Imagem do item selecionado
    public Text itemNameText; // Nome do item
    public Text itemDescriptionText; // Descrição do item

    // Atualiza os detalhes no painel com as informações do item
    public void UpdateDetails(Item item)
    {
        if (item != null)
        {
            itemImage.sprite = item.image; // Define a imagem do item
            itemNameText.text = item.name; // Define o nome do item
            itemDescriptionText.text = item.description; // Define a descrição do item
        }
        else
        {
            ClearDetails(); // Limpa os detalhes se nenhum item for selecionado
        }
    }

    // Limpa as informações do painel
    public void ClearDetails()
    {
        itemImage.sprite = null;
        itemNameText.text = "";
        itemDescriptionText.text = "";
    }
}