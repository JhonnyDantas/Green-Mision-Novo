using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalEscalada : MonoBehaviour
{
    [SerializeField] private string nomeDoJogo;
    [SerializeField] private string Jogo;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOpcoes;
    
    
    public void Jogar()
    {
        SceneManager.LoadScene(nomeDoJogo);
    }
    
    public void Abriropcoes()
    {
       painelMenuInicial.SetActive(false);
       painelOpcoes.SetActive(true);
    }
    
    public void FecharOpcoes()
    {
        painelOpcoes.SetActive(false);
        painelMenuInicial.SetActive(true);
    }

    public void SairJogo()
    {
        SceneManager.LoadScene(Jogo);
    }

    
}
