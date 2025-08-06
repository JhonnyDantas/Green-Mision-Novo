using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MudarDeFases : MonoBehaviour
{
    [SerializeField] private string Facil;
    [SerializeField] private string Medio;
    [SerializeField] private string Dificil;
    [SerializeField] private string MenuInicial;
    [SerializeField] private GameObject painelMenuInicial;
    [SerializeField] private GameObject painelOpcoes;
    [SerializeField] private GameObject painelDificuldades;
    [SerializeField] private GameObject pingPong;
    [SerializeField] GameObject btnMedio, btnDificil;
    [SerializeField] private GameObject PainelMenuJogo;
    [SerializeField] private string Jogo;

    void Start()
    {

    }

    public void AbrirPainelDificuldade()
    {
        Debug.Log("teste");
        pingPong.SetActive(false);
        Debug.Log(PlayerPrefs.GetString("fase").ToString());

        painelDificuldades.SetActive(true);

        switch (PlayerPrefs.GetString("fase"))
        {
            case "PongFacil": btnMedio.SetActive(true); break;
            case "PongMedio": btnDificil.SetActive(true); btnMedio.SetActive(true); break;
            default: btnMedio.SetActive(false); btnDificil.SetActive(false); break;
        }

    }

    public void PongFacil()
    {
        SceneManager.LoadScene(Facil);
    }

    public void PongMedio()
    {
        SceneManager.LoadScene(Medio);
    }

    public void PongDificil()
    {
        SceneManager.LoadScene(Dificil);
    }

    public void FecharPainelDificuldades()
    {
        pingPong.SetActive(true);
        painelDificuldades.SetActive(false);
        painelMenuInicial.SetActive(true);
    }

    public void AbrirMenuJogo()
    {
        PainelMenuJogo.SetActive(true);
        Time.timeScale = 0f;
    }

    public void FecharMenuJogo()
    {
        Time.timeScale = 1f;
        PainelMenuJogo.SetActive(false);
    }

    public void Abriropcoes()
    {
        pingPong.SetActive(false);
        PlayerPrefs.SetString("fase", "inicio"); //deve estar no jogo principal

        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(true);
    }

    public void FecharOpcoes()
    {
        pingPong.SetActive(true);
        painelOpcoes.SetActive(false);
        painelMenuInicial.SetActive(true);
    }

    public void Recomecar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PainelMenuJogo.SetActive(false);
    }

    public void SairJogo()
    {
        SceneManager.LoadScene(Jogo);
    }
    public void SairMenu()
    {
        SceneManager.LoadScene(MenuInicial);
    }

    //     Debug.Log("Sair do Jogo");
    //         Application.Quit();
}
