using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public PlayerJson player;
    public Transform posPlayer;

    void Start()
    {



    }

    void Update()
    {
       
    }

    public void Save()
    {
        
        player.x = posPlayer.position.x;
        player.y = posPlayer.position.y;
        player.Cena = SceneManager.GetActiveScene().name; 
              player.SaveGame();

    }

    public void LoadGame()
    {
        player.LoadGame(); 
        SceneManager.LoadScene(player.Cena);
        
        posPlayer.position = new Vector3(player.x, player.y + 0.5f, 0f);
       
    }



}