using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MiniGameController : MonoBehaviour
{
    [SerializeField] private string MiniGamePong;
    [SerializeField] private string MiniGameEscalada;
    void Start()
    {

    }

    // Update is called once per frame
    public void GamePong()
    {
        SceneManager.LoadScene(MiniGamePong);
    }
    

    public void GameEscalada()
    {
        SceneManager.LoadScene(MiniGameEscalada);
    }
}
