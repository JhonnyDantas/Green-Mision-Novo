using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Instances")]
    public static GameController instance; //instace para associar qualquer coisa desse script em outro script


    [Header("Paineis GameObjects")]
    public GameObject painelMenu;
    public GameObject painelCreditos;
    public GameObject painelConfiguracao;
    public GameObject painelInventory;
    public GameObject painelInventoryLixo;

    [Header("Botões")]
    public GameObject button;
    public GameObject buttonImage;
    public GameObject buttonArmaInseticida;


    [Header("Objetos")]   
    public GameObject kit;
    public GameObject armaInseticida;
    public GameObject imageLixo; 
    public GameObject joyStick;


    public LevelLoader levelLoader;
    public string sceneName;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Ações para os Painéis (Menu e Créditos)
    public void AbrirPainelMenu()
    {
        painelMenu.SetActive(true);
        painelCreditos.SetActive(false);

        levelLoader.Transition(sceneName);
    }
    
    public void FecharPainelMenu()
    {
        painelMenu.SetActive(false);
        painelCreditos.SetActive(false);
    }

    public void AbrirPainelCreditos()
    {
        painelCreditos.SetActive(true);
        painelMenu.SetActive(true);     
    }

    public void FecharPainelCreditos()
    {
        painelCreditos.SetActive(false);
        painelMenu.SetActive(true);     
    }

    public void AbrirPainelConfiguracao()
    {
        painelConfiguracao.SetActive(true);
        PlayerMove.instance.ResetSpeed();
    }

    public void FecharPainelConfiguracao()
    {
        painelConfiguracao.SetActive(false);
        PlayerMove.instance.ReturnSeed();
    }

    public void AbrirPainelInventario()
    {
        painelInventory.SetActive(true);
        PlayerMove.instance.ResetSpeed();
        button.SetActive(true);
    }

    public void FecharPainelInventario()
    {
        painelInventory.SetActive(false);
        PlayerMove.instance.ReturnSeed();
    }

    public void AbrirPainelInventarioLixo()
    {
        painelInventory.SetActive(true);
        imageLixo.SetActive(true);
        PlayerMove.instance.ResetSpeed();
        button.SetActive(true);
    }

    public void FecharPainelInventarioLixo()
    {
        painelInventory.SetActive(false);
        imageLixo.SetActive(false);
        PlayerMove.instance.ReturnSeed();
        button.SetActive(false);
    }

    public void ColetarArma()
    {
        kit.SetActive(true);
        buttonImage.SetActive(true);

        // Aqui, fazemos a ativação do joystick diretamente, sem depender da colisão
        joyStick.SetActive(true);

        // Ativa o botão da arma para poder ser ativado imediatamente
        buttonArmaInseticida.SetActive(true); 

        // Não esquecemos de garantir que o botão correto seja ativado logo após a coleta
        if (buttonImage.activeSelf)
        {
            AtivarArma(); // Ativa a arma e o joystick
        }
    }



    public void AtivarArma()
    {
        if (buttonImage.activeSelf) // Só ativa se o botão estiver visível
        {
            armaInseticida.SetActive(true);  // Ativa a arma
            joyStick.SetActive(true);         // Ativa o joystick
            buttonImage.SetActive(false);    // Desativa o botão de coleta
        }
    }



    public void DesativarArma()
    {
        armaInseticida.SetActive(false);
        joyStick.SetActive(false);
        buttonImage.SetActive(true);
    }

}
