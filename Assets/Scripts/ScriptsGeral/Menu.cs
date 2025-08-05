using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [Header("Strings")]
    public string sceneName;
    public string sceneMenu;

    public LevelLoader levelLoader;

    public void StartGame()
    {
        levelLoader.Transition(sceneName);
    }

    public void StartMenu()
    {
        levelLoader.Transition(sceneMenu);
    }

    public void GuitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;

        //Application.Quit();
    }
}
