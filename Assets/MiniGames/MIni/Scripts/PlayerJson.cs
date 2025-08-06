using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class PlayerJson
{

    public string Cena;
    public float x, y;

    private string path = "Assets/Mini/Player.txt";

    public void SaveGame()
    {
        var content = JsonUtility.ToJson(this, true);
        File.WriteAllText(path, content);

    }

    public void LoadGame()
    {
        var content = File.ReadAllText(path);
        var data = JsonUtility.FromJson<PlayerJson>(content);

        x = data.x;
        y = data.y;
        Cena = data.Cena;
    }
}