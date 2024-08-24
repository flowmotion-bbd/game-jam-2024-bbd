using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndLeveUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] GameObject leaderboardEntryParent = null;
    [SerializeField] GameObject entryPrefab = null;
    [SerializeField] TMP_Text userTimeText = null;
    [SerializeField] TMP_Text userRankText = null;

    [Header("Level Info")]
    [SerializeField] int levelNumber = 0;

    const int NUM_ENTRIES_TO_DISPLAY = 5;

    public static EndLeveUIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public async void PopulateLevelLeaderBoard(float elapsedTime)
    {
        var addScoreResponse = await LeaderBoardManager.Instance.AddPlayerScoreForLevelAsync(levelNumber, (int)elapsedTime*1000);
        userTimeText.text = addScoreResponse.timeScore;
        userRankText.text = (addScoreResponse.rank+1).ToString();

        var leaderboardResponse = await LeaderBoardManager.Instance.GetScoresForLevelAsync(levelNumber, limit:5);

        int numIterations = Mathf.Min(leaderboardResponse.results.Count, NUM_ENTRIES_TO_DISPLAY);

        for (int i = 0; i < numIterations; i++)
        {
            var result = leaderboardResponse.results[i];
            var entryInstance = Instantiate(entryPrefab, leaderboardEntryParent.transform);
            entryInstance.GetComponent<LevelLeaderBoardEntry>().SetEntryInformation(result.rank + 1, result.playerName, result.timeScore);
        }
    }
}
