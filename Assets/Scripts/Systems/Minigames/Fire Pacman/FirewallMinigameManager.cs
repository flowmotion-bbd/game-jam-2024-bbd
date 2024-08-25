using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FirewallMinigameManager : MinigameManager
{
    public GameObject[] flames;

    public GameObject hackerman;

    public TMP_Text timeText;

    public int numberOfFlameAgents = 4;

    [SerializeField] private Transform spawnPointsParent;
    [SerializeField] private GameObject fireAgentObject;

    protected override void StartMinigame()
    {
        scoreAchieved = 0f;
        minigameInProgress = true;
    }

    private new void Start()
    {
        base.Start();

        SpawnFireAgents();
    }

    private new void Update()
    {
        base.Update();

        if (minigameInProgress)
        {
            scoreAchieved += Time.deltaTime;
            timeText.text = FormatTime(scoreAchieved);
        }
    }

    void SpawnFireAgents()
    {
        Transform[] spawnPoints = spawnPointsParent.GetComponentsInChildren<Transform>();
        int spawnCount = Mathf.Min(numberOfFlameAgents, spawnPoints.Length - 1);

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

            Instantiate(fireAgentObject, spawnPoints[randomIndex]);
        }
    }
}
