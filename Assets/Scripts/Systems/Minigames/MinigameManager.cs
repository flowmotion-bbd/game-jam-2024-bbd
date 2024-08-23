using UnityEngine;
using System.Collections;

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

    public bool MinigameInProgess
    {
        get { return minigameInProgress; }
        set { minigameInProgress = value; }
    }

    protected void Start()
    {
        gameManager = GameManager.Instance;

        dialogueManager = DialogueManager.Instance;

        MinigameUIManager.Instance.HideAllPannels();

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
        if (minigameInProgress == false || minigameOver == true)
        {
            return;
        }

        minigameInProgress = false;
        minigameWon = hasWon;

        if (hasWon)
        {
            MinigameUIManager.Instance.ShowWinScreen("You scored " + scoreAchieved + " seconds");
        }
        else
        {
            MinigameUIManager.Instance.ShowLoseScreen("You scored " + scoreAchieved + " seconds");
        }

        StartCoroutine(SetMinigameOver());
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

    IEnumerator SetMinigameOver()
    {
        yield return new WaitForSeconds(3f);
        minigameOver = true;
    }

    protected abstract void StartMinigame();
}
