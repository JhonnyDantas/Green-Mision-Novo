using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Cria um asset de diálogo no menu do Unity
[CreateAssetMenu(fileName = "New Dialogue", menuName = "New Dialogue/Dialogue")]
public class Dialog : ScriptableObject
{
    [Header("Settings")]
    public GameObject actor; // Referência ao personagem que fala

    [Header("Dialogue")]
    public Sprite spearkSprite; // Imagem do personagem durante o diálogo
    public string sentence; // Frase temporária (usada para adicionar à lista)

    public List<Sentences> dialogues = new List<Sentences>(); // Lista de falas
}

[System.Serializable]
public class Sentences
{
    public string actorName; // Nome do personagem que fala
    public Sprite profile;   // Imagem usada no diálogo
    public Languages sentence; // Frase em diferentes idiomas
}

[System.Serializable]
public class Languages
{
    public string portugues;
    public string ingles;
    public string espanhol;
}

#if UNITY_EDITOR
// Editor personalizado para o ScriptableObject de diálogo
[CustomEditor(typeof(Dialog))]
public class BuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Desenha o inspector padrão

        Dialog ds = (Dialog)target;

        // Cria a estrutura de idiomas com a frase em português (vinda do campo "sentence")
        Languages l = new Languages();
        l.portugues = ds.sentence;

        // Cria a estrutura da fala usando a sprite e a frase
        Sentences s = new Sentences();
        s.profile = ds.spearkSprite;
        s.sentence = l;

        // Botão que adiciona uma nova fala à lista
        if (GUILayout.Button("Create Dialogue"))
        {
            if (ds.sentence != "")
            {
                ds.dialogues.Add(s);

                // Limpa os campos temporários
                ds.spearkSprite = null;
                ds.sentence = "";
            }
        }
    }
}
#endif
