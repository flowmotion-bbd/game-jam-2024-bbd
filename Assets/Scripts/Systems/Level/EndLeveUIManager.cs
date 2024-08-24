using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLeveUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] GameObject leaderboardEntryParent = null;
    [SerializeField] GameObject entryPrefab = null;
    [SerializeField] TMP_Text userCurrentTimeText = null;
    [SerializeField] TMP_Text userBestTimeText = null;
    [SerializeField] TMP_Text userRankText = null;

    [Header("Level Info")]
    [SerializeField] string levelNamePrefix = "Level ";

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

    private int GetLevelNumberFromSceneName()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        string levelNumberStr = currentSceneName[levelNamePrefix.Length..];
        if (int.TryParse(levelNumberStr, out int levelNumber))
        {
            return levelNumber;
        }
        else
        {
            Debug.LogError($"Failed to parse level number from scene name '{currentSceneName}'.");
        }
        return -1;
    }

    public async void PopulateLevelLeaderBoard(float elapsedTime)
    {
        int timeMillis = (int)(elapsedTime * 1000);
        userCurrentTimeText.text = LeaderBoardManager.Instance.ConvertMillisecondsToTimeFormat(timeMillis);

        int levelNumber = GetLevelNumberFromSceneName();
        var addScoreResponse = await LeaderBoardManager.Instance.AddPlayerScoreForLevelAsync(levelNumber, timeMillis);
        userBestTimeText.text = addScoreResponse.timeScore;
        userRankText.text = "#" + (addScoreResponse.rank + 1).ToString();

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
