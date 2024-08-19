using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences = new Queue<string>();

    [SerializeField] private TextMeshProUGUI dialogueText;

    Action dialogueCallback;

    public void StartDialogue(Dialogue dialogue, Action dialogueCallback)
    {
        sentences.Clear();
        this.dialogueCallback = dialogueCallback;

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count > 0)
        {
            EndDialouge();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char character in sentence.ToCharArray())
        {
            dialogueText.text += character;
            yield return null;
        }
    }

    void EndDialouge()
    {
        dialogueCallback();
    }
}
