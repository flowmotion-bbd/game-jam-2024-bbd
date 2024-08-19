using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUIManager : MonoBehaviour
{
    [SerializeField] private GameObject endLevelScreen;
    [SerializeField] private GameObject countDownScreen;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI endScreenTimeText;
    [SerializeField] private TextMeshProUGUI countDownText;
    private LevelManager levelManager;

    void Start()
    {
        levelManager = FindAnyObjectByType<LevelManager>();

        timerText.text = FormatTime(0f);

        HideEndLevelScreen();    
    }
    public void ShowEndLevelScreen(List<float> topTimes, float usersTime)
    {
        UpdateTopTimes(topTimes, usersTime);
        endLevelScreen.SetActive(true);
    }

    void UpdateTopTimes(List<float> topTimes, float usersTime)
    {
        endScreenTimeText.text = "";
        for (int i = 0; i < topTimes.Count; i++) 
        {
            endScreenTimeText.text += i + ". " + FormatTime(topTimes[i]) + "\n";
        }

        endScreenTimeText.text += "4. " + FormatTime(usersTime);
    }

    public void HideEndLevelScreen()
    {
        endLevelScreen.SetActive(false);
    }

    public void RetryLevel()
    {
        timerText.text = FormatTime(0f);
        levelManager.RestartLevel();
    }

    List<float> SplitTime(float time)
    {
        time = Math.Abs(time);

        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time - minutes * 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);

        return new List<float>() { minutes, seconds, milliseconds };
    }

    public void UpdateTimerDisplay(float elapsedTime)
    {
        timerText.text = FormatTime(elapsedTime);
    }

    string FormatTime(float time)
    {
        List<float> splitTime = SplitTime(time);

        string signAppend = time < 0f ? "-" : "";
        return signAppend + string.Format("{0:00}:{1:00}:{2:000}", splitTime[0], splitTime[1], splitTime[2]);
    }

    public void UpdateCountdownDisplay(float countDownTime)
    {
        if (countDownTime > 3)
        {
            return;
        }

       int countDown = Mathf.CeilToInt(countDownTime);
        countDownText.text = countDown.ToString();
    }

    public void ShowCountdown()
    {
        countDownScreen.SetActive(true);
        UpdateCountdownDisplay(3f);
    }

    public void HideCountdown()
    {
        countDownScreen.SetActive(false);
    }
}
