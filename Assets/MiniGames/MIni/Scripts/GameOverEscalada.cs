using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverEscalada : MonoBehaviour
{
    // Start is called before the first frame update
    public void Jogar()
    {
        SceneManager.LoadScene("MiniGameEscalada");
    }

    public void SairJogo()
    {
        Debug.Log("Sair do Jogo");
        Application.Quit();
    }

}
