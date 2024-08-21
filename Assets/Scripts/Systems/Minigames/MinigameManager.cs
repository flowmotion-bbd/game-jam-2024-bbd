using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MinigameManager : MonoBehaviour
{
    protected GameManager gameManager;
    protected bool minigameWon = false;
    protected DialogueManager dialogueManager;

    [SerializeField] private Dialogue startMinigameDialogue;
    [SerializeField] private Dialogue minigameWonDialogue;
    [SerializeField] private Dialogue minigameLostDialogue;

    protected void Start()
    {
        gameManager = GameManager.Instance;

        dialogueManager = DialogueManager.Instance;

        dialogueManager.StartDialogue(startMinigameDialogue, StartMinigame);
    }

    protected void EndMinigame(float timeChange)
    {
        gameManager.MinigameEnded(minigameWon, timeChange, minigameWon ? minigameWonDialogue : minigameLostDialogue);
    }

    protected abstract void StartMinigame();
}
