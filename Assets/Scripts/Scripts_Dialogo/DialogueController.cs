using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [System.Serializable]
    public enum idiom
    {
        pt,
        eng,
        spa
    }

    public idiom language;

    [Header("Components")]
    public GameObject painelDialogue;
    public Image profileImage;
    public Text speechText;
    public Text speechNameText;

    [Header("Settings")]
    public float typingSpeech;

    [HideInInspector] public bool isShowing;
    private int index;
    private string[] sentences;

    public static DialogueController instance;

    void Awake()
    {
        instance = this;
    }

    IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeech);
        }
    }

    public void NextSentences()
    {
        if (speechText.text == sentences[index])
        {
            if (index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else
            {
                EndDialogue(); // Finaliza o diálogo aqui
            }
        }
    }

    public void Speech(string[] txt, Sprite profile, string npcName)
    {
        if (!isShowing)
        {
            painelDialogue.SetActive(true);
            sentences = txt;
            speechNameText.text = npcName; // Define o nome do NPC
            profileImage.sprite = profile; // Define a imagem do NPC
            profileImage.gameObject.SetActive(true); // Garante que a imagem esteja visível
            StartCoroutine(TypeSentence());
            isShowing = true;
        }
    }


    private void EndDialogue()
    {
        speechText.text = "";
        index = 0;
        painelDialogue.SetActive(false);
        sentences = null;
        isShowing = false;

    }
}