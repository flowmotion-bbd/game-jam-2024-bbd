using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class MinigameManager : MonoBehaviour
{
    protected GameManager gameManager;
    protected bool minigameWon = false;
    protected DialogueManager dialogueManager;
    protected bool minigameInProgress = false;
    protected bool minigameOver = false;
    protected float scoreAchieved;

    [SerializeField] private Dialogue startMinigameDialogue;

    [SerializeField] private float lossScore = 5f;

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
        gameManager.MinigameEnded(minigameWon, scoreAchieved, null);
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
            MinigameUIManager.Instance.ShowWinScreen(scoreAchieved.ToString("F3") + " seconds will be added to your current time!");
        }
        else
        {
            scoreAchieved = lossScore;
            MinigameUIManager.Instance.ShowLoseScreen(scoreAchieved.ToString("F3") + " seconds will be added to your current time!");
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
        yield return new WaitForSeconds(1f);
        minigameOver = true;
    }

    List<float> SplitTime(float time)
    {
        time = Math.Abs(time);

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);

        return new List<float>() { minutes, seconds, milliseconds };
    }

    public string FormatTime(float time)
    {
        List<float> splitTime = SplitTime(time);

        string signAppend = time < 0f ? "-" : "";
        return signAppend + string.Format("{0:00}:{1:00}:{2:000}", splitTime[0], splitTime[1], splitTime[2]);
    }

    protected abstract void StartMinigame();
}
