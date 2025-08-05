using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Dialogue : MonoBehaviour
{
    public float dialogueRange;
    public LayerMask playerLayer;

    public Dialog dialogue;

    private bool playerHit;
    private List<string> sentences = new List<string>();

    void Start()
    {
        GetTexts();
    }

    void FixedUpdate()
    {
        ShowDialogue();
    }

    void GetTexts()
    {
        for (int i = 0; i < dialogue.dialogues.Count; i++)
        {
            switch (DialogueController.instance.language)
            {
                case DialogueController.idiom.pt:
                    sentences.Add(dialogue.dialogues[i].sentence.portugues);
                    break;

                case DialogueController.idiom.eng:
                    sentences.Add(dialogue.dialogues[i].sentence.ingles);
                    break;

                case DialogueController.idiom.spa:
                    sentences.Add(dialogue.dialogues[i].sentence.espanhol);
                    break;
            }
        }
    }

    public void ShowDialogue()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, dialogueRange, playerLayer);
        playerHit = hit != null; // Define true se o player estiver na área
    }

    public void AbrirPainelDialogo()
    {
        if (playerHit)
        {
            // Obtendo o primeiro diálogo para pegar nome e imagem do NPC
            if (dialogue.dialogues.Count > 0)
            {
                string npcName = dialogue.dialogues[0].actorName;
                Sprite npcProfile = dialogue.dialogues[0].profile;

                DialogueController.instance.Speech(sentences.ToArray(), npcProfile, npcName);
            }
        }
        else
        {
            Debug.Log("O jogador não está na área do NPC!");
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }
}