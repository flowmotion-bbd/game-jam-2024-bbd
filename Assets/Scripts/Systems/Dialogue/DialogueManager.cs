using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class DialogueManager : MonoBehaviour
{    public static DialogueManager Instance { get; private set; }

    private Queue<string> sentences = new Queue<string>();

    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private Animator animator;

    Action dialogueCallback;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartDialogue(Dialogue dialogue, Action dialogueCallback)
    {
        if (dialogue == null)
        {
            dialogue = new Dialogue();
        }

        animator.SetBool("IsOpen", true);

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
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        } else
        {
            EndDialouge();
        }
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

    public void EndDialouge()
    {
        animator.SetBool("IsOpen", false);
        dialogueCallback();
    }
}
