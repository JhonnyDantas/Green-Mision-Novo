using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Header("Botões")]
    public GameObject buttonImage; // Botão padrão
    public GameObject buttonDialogo;
    public GameObject buttonInventoryLixo;
    public GameObject buttonArmaInseticida;
    public GameObject buttonFlor;
    public GameObject buttonCaderno;
    public GameObject buttonColetaAgua;
    public GameObject buttonConsertar;
    private GameObject activeButton; // Referência ao botão atualmente ativo


    [Header("Controlers")]
    public static PlayerCollision instance; // Criamos uma instância para acessar no GameController


    [Header("Dicionários")]
    private Dictionary<string, GameObject> buttonMapping; // Dicionário para associar tags aos botões

    
    [Header("Boleanos")]
    bool estaNaAreaDeObjeto = false; // Variável para armazenar se o player está na área de um objeto

    private void Awake()
    {
        instance = this; // Garante que podemos acessar PlayerCollision.instance
    }

    // Função que verifica se o player está dentro da área de um objeto
    public bool PlayerDentroDaArea()
    {
        return estaNaAreaDeObjeto;
    }

    private void Start()
    {
        // Inicializa o dicionário de mapeamento de botões
        buttonMapping = new Dictionary<string, GameObject>
        {
            { "Lixo", buttonInventoryLixo },
            { "NPC", buttonDialogo },
            { "Flor", buttonFlor },
            { "Agua", buttonColetaAgua },
            { "Arma", buttonArmaInseticida },
            { "Objeto", buttonConsertar },
            { "Caderninho", buttonCaderno }
        };

        // Certifique-se de que todos os botões começam desativados
        foreach (var button in buttonMapping.Values)
        {
            if (button != null)
            {
                button.SetActive(false);
            }
        }
        buttonImage?.SetActive(true); // Garante que o botão padrão esteja ativo
        activeButton = buttonImage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (buttonImage != null && !buttonImage.activeSelf) return; // Evita colisões se o botão de coleta estiver desativado

        string tag = other.tag;

        if (tag == "Arma" && other.gameObject != null)
        {
            // Coleta a arma e ativa os botões necessários
            GameController.instance.ColetarArma(); // Chama a função de coleta diretamente
            Destroy(other.gameObject); // Destroi a arma
        }

        // Ativa o botão correspondente à tag, se existir no mapeamento
        if (buttonMapping.ContainsKey(tag))
        {
            ActivateButton(buttonMapping[tag]);
        }

        if (buttonMapping.ContainsKey(other.gameObject.tag))
        {
            estaNaAreaDeObjeto = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        string tag = other.tag;

        if (buttonMapping.ContainsKey(tag))
        {
            DeactivateButton();
        
        }
        
        if (buttonMapping.ContainsKey(other.gameObject.tag))
        {
            estaNaAreaDeObjeto = false;
            PlayerMove.instance.StopBatendo();
        }

    }
    
    // Método para ativar um botão e desativar outros
    private void ActivateButton(GameObject buttonToActivate)
    {
        foreach (var button in buttonMapping.Values)
        {
            if (GameController.instance != null && GameController.instance.kit != null)
            {
                if (button != GameController.instance.kit) // Garante que o kit não seja desativado
                {
                button?.SetActive(false);
                }
            }
            else
            {
            button?.SetActive(false);
            }
        }

        buttonToActivate?.SetActive(true);
        activeButton = buttonToActivate;

        buttonImage?.SetActive(false);
    }


    // Método para desativar o botão ativo e voltar ao padrão
    private void DeactivateButton()
    {
        // Se o botão ativo for o kit, não o desativa
        if (activeButton == GameController.instance.kit)
        {
            return;
        }

        // Desativa o botão ativo, se houver
        activeButton?.SetActive(false);

        // Ativa o botão padrão
        buttonImage?.SetActive(true);
        activeButton = buttonImage;
    }

}
