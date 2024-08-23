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
        minigameInProgress = false;
        minigameOver = true;
        minigameWon = hasWon;

        if (hasWon)
        {
            MinigameUIManager.Instance.ShowWinScreen("You scored " + scoreAchieved + " seconds");
        }
        else
        {
            MinigameUIManager.Instance.ShowLoseScreen("You scored " + scoreAchieved + " seconds");
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
