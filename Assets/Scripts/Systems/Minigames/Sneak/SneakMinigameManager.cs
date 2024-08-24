using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SneakMinigameManager : MinigameManager
{

    [SerializeField] private TMP_Text minigameStatusText;
    [SerializeField] private Transform spawnPointsParent;
    [SerializeField] private GameObject packetObject;

    [SerializeField] private GameObject globalLight;

    [SerializeField] private float timeLeft = 30f;
    [SerializeField] private int numberOfPackets = 8;

    protected override void StartMinigame()
    {
        scoreAchieved = 4f;
        minigameInProgress = true;
        globalLight.SetActive(false);
    }
    
    private new void Start()
    {
        base.Start();

        minigameStatusText.text = "Score: " + scoreAchieved.ToString("F0") + "\nTime Remaining: " + Mathf.CeilToInt(timeLeft);
        SpawnPackets();
    }

    private new void Update()
    {
        base.Update();

        if (minigameInProgress)
        {
            timeLeft -= Time.deltaTime;
            minigameStatusText.text = "Score: " + scoreAchieved.ToString("F0") +"\nTime Remaining: " + Mathf.CeilToInt(timeLeft);

            if (timeLeft <= 0f)
            {
                GameOver(true);
            }
        }
    }

    void SpawnPackets()
    {
        Transform[] spawnPoints = spawnPointsParent.GetComponentsInChildren<Transform>();
        int spawnCount = Mathf.Min(numberOfPackets, spawnPoints.Length - 1);

        List<int> usedIndices = new List<int>();
        for (int i = 0; i < spawnCount; i++)
        {
            int randomIndex;

            do
            {
                randomIndex = Random.Range(1, spawnPoints.Length);
            }
            while (usedIndices.Contains(randomIndex));

            usedIndices.Add(randomIndex);

            Instantiate(packetObject, spawnPoints[randomIndex]);
        }
    }

    public void AddToScore(float scoreIncrease)
    {
        if (minigameInProgress)
        {
            scoreAchieved += scoreIncrease;
        }
    }
}
