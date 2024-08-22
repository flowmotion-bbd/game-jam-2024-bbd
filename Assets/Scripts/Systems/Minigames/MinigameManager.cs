using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class MinigameManager : MonoBehaviour
{
    protected GameManager gameManager;
    protected bool minigameWon = false;
    protected DialogueManager dialogueManager;
    protected bool minigameInProgress = false;
    protected bool minigameOver = false;
    protected float scoreAchieved;

    [SerializeField] private Dialogue startMinigameDialogue;
    [SerializeField] private Dialogue minigameLostDialogue;

    [SerializeField] private GameObject wonPanel;
    [SerializeField] private TMP_Text winScoreText;

    [SerializeField] private GameObject lostPanel;
    [SerializeField] private TMP_Text lostScoreText;

    public bool MinigameInProgess
    {
        get { return minigameInProgress; }
        set { minigameInProgress = value; }
    }

    protected void Start()
    {
        gameManager = GameManager.Instance;

        dialogueManager = DialogueManager.Instance;

        wonPanel.SetActive(false);
        lostPanel.SetActive(false);

        if (TransitionManager.Instance == null || !TransitionManager.Instance.Transitioning)
        {
            StartMinigameDialogue();
        }
    }

    public void StartMinigameDialogue()
    {
        dialogueManager.StartDialogue(startMinigameDialogue, StartMinigame);
    }

    protected void EndMinigame()
    {
        gameManager.MinigameEnded(minigameWon, scoreAchieved, minigameWon ? null : minigameLostDialogue);
    }

    public void GameOver(bool hasWon)
    {
        minigameInProgress = false;
        minigameOver = true;
        minigameWon = hasWon;

        if (hasWon)
        {
            wonPanel.SetActive(true);
            winScoreText.text = "You scored " + scoreAchieved + " seconds";
        }
        else
        {
            lostPanel.SetActive(true);
            lostScoreText.text = "You scored " + scoreAchieved + " seconds";
        }
    }

    protected void Update()
    {
        if (minigameOver)
        {
            if (Input.anyKeyDown)
            {
                EndMinigame();
            }
        }
    }

    protected abstract void StartMinigame();
}
