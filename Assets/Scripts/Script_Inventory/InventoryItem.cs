using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Item item;

    [Header("UI")]
    public Image image;
    public Text quantityText; // Texto que mostra a quantidade

    [HideInInspector] public Transform parentAfterDrag;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;

    private bool isDragging;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }  

    public void InitialiseItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.image;
    }

    public void UpdateQuantity(int quantity)
    {
        if (quantity > 1)
        {
            quantityText.text = $"{quantity}x"; // Exibe a quantidade no formato "Nx"
        }
        else
        {
            quantityText.text = ""; // Limpa o texto se houver apenas 1 item
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        parentAfterDrag = transform.parent; // Salva o slot original
        transform.SetParent(canvas.transform); // Move para o topo da hierarquia
        canvasGroup.blocksRaycasts = false; // Desativa os raycasts enquanto arrasta
        canvasGroup.alpha = 0.6f; // Reduz a opacidade para indicar o arraste
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, eventData.position, canvas.worldCamera, out Vector2 localPoint))
        {
            rectTransform.localPosition = localPoint; // Atualiza a posição do item enquanto arrasta
        }

        // Feedback visual ao passar sobre "jogar lixo"
        GameObject pointerObject = eventData.pointerEnter;
        if (pointerObject != null && pointerObject.CompareTag("JogarLixo"))
        {
            // Aqui você pode adicionar um efeito visual, como mudar a cor do "jogar lixo"
            Debug.Log("Item sobre a área de jogar lixo.");
        }         
    }

   public void OnEndDrag(PointerEventData eventData)
{
    isDragging = false;
    canvasGroup.blocksRaycasts = true; // Reativa os raycasts
    canvasGroup.alpha = 1.0f; // Restaura a opacidade

    // Verifica se foi solto em um GameObject com a tag "JogarLixo"
    if (eventData.pointerEnter != null && eventData.pointerEnter.CompareTag("JogarLixo"))
    {
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
        if (inventoryManager != null)
        {
            inventoryManager.RemoveItem(this); // Remove o item do inventário
        }

        Destroy(gameObject); // Destroi o objeto do item na cena
        Debug.Log($"Item {item.name} foi jogado no lixo e destruído!");
        return;
    }

    // Verifica se foi solto em um slot válido
    InventorySlot slot = eventData.pointerEnter?.GetComponent<InventorySlot>();
    if (slot != null && !slot.HasItem()) // Se for um slot e estiver vazio
    {
        transform.SetParent(slot.transform); // Move o item para o novo slot
        rectTransform.localPosition = Vector3.zero; // Centraliza o item no novo slot
        return;
    }

    // Caso contrário, volta para o slot original
    transform.SetParent(parentAfterDrag);
    rectTransform.localPosition = Vector3.zero;
}

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isDragging) // Só executa se não estiver arrastando
        {
            InventoryDetailPanel detailPanel = FindObjectOfType<InventoryDetailPanel>();
            if (detailPanel != null)
            {
                detailPanel.UpdateDetails(item); // Atualiza os detalhes do item
            }

            // Seleciona o item
            InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
            if (inventoryManager != null)
            {
                inventoryManager.SelectItem(this);
                Debug.Log($"Item {item.name} selecionado.");
            }
        }
    }
}