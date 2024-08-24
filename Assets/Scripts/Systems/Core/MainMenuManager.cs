using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Pannels")]
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject levelSelectPanel;
    [SerializeField] GameObject leaderboardsPanel;
    [SerializeField] GameObject minigameSelectPanel;
    [SerializeField] GameObject howToPlayPanel;

    [Header("Username")]
    [SerializeField] TMP_InputField usernameInputField;

    [Header("Leaderboards")]
    [SerializeField] Transform leaderboardLevelButtonContainer;
    [SerializeField] GameObject leaderboardLevelButton;
    [SerializeField] Transform leaderboardEntryParent;
    [SerializeField] GameObject leaderboardEntry;
    [SerializeField] GameObject leaderboardEmpty;

    [Header("Levels")]
    [SerializeField] Transform levelButtonContainer;
    [SerializeField] GameObject levelButton;
    [SerializeField] string levelNamePrefix = "Level ";

    const string START_LEVEL_NAME = "Level 1";

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
        GenerateLevelButtons();
    }

    void GenerateLevelButtons()
    {
        List<string> levelSceneNames = GetLevelSceneNames();

        foreach (string sceneName in levelSceneNames)
        {
            CreateLeaderboardLevelButtonForScene(sceneName);
            CreateLevelButtonForScene(sceneName);
        }
    }

    List<string> GetLevelSceneNames()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        List<string> levelSceneNames = new List<string>();

        for (int i = 0; i < sceneCount; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

            if (sceneName.StartsWith(levelNamePrefix))
            {
                levelSceneNames.Add(sceneName);
            }
        }

        return levelSceneNames;
    }

    void CreateLevelButtonForScene(string sceneName)
    {
        GameObject buttonInstance = Instantiate(levelButton, levelButtonContainer);
        TMP_Text buttonText = buttonInstance.GetComponentInChildren<TMP_Text>();

        if (buttonText != null)
        {
            buttonText.text = sceneName.Replace(levelNamePrefix, "");
        }

        Button buttonComponent = buttonInstance.GetComponent<Button>();

        if (buttonComponent != null)
        {
            buttonComponent.onClick.AddListener(() => LoadLevel(sceneName));
        }
    }

    void CreateLeaderboardLevelButtonForScene(string sceneName)
    {
        GameObject buttonInstance = Instantiate(leaderboardLevelButton, leaderboardLevelButtonContainer);
        TMP_Text buttonText = buttonInstance.GetComponentInChildren<TMP_Text>();

        if (buttonText != null)
        {
            buttonText.text = sceneName;
        }

        Button buttonComponent = buttonInstance.GetComponent<Button>();

        if (buttonComponent != null)
        {
            buttonComponent.onClick.AddListener(() => PopulateLeaderboard(int.Parse(sceneName.Replace(levelNamePrefix, ""))));
        }
    }

    async void PopulateLeaderboard(int levelNumber)
    {
        foreach (Transform child in leaderboardEntryParent)
        {
            Destroy(child.gameObject);
        }

        int offset = 0;
        int total;
        int current = 0;
        do
        {
            LeaderboardResponse leaderboardResponse = await LeaderBoardManager.Instance.GetScoresForLevelAsync(levelNumber, 20, offset);

            total = leaderboardResponse.total;

            foreach (LeaderboardResult result in leaderboardResponse.results)
            {
                current++;
                var entryInstance = Instantiate(leaderboardEntry, leaderboardEntryParent);
                entryInstance.GetComponent<LevelLeaderBoardEntry>().SetEntryInformation(result.rank + 1, result.playerName, result.timeScore);
            }

            offset += current;
        } while (total < current);

        if (current <= 0)
        {
            Instantiate(leaderboardEmpty, leaderboardEntryParent);
        }
    }


    public void StartGame()
    {
        gameManager.LoadLevel(START_LEVEL_NAME);
    }

    void LoadLevel(string levelSceneName)
    {
        gameManager.LoadLevel(levelSceneName);
    }

    public void LoadMinigame(string minigameSceneName)
    {
        gameManager.LoadMinigame(minigameSceneName, false, null);
    }

    public void LevelSelectButtonHandler()
    {
        mainMenuPanel.SetActive(false);
        levelSelectPanel.SetActive(true);
    }

    public void MiniGameSelectButtonHandler()
    {
        mainMenuPanel.SetActive(false);
        minigameSelectPanel.SetActive(true);
    }

    public void HowToPlayButtonHandler()
    {
        mainMenuPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        mainMenuPanel.SetActive(true);
        minigameSelectPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
        leaderboardsPanel.SetActive(false);
    }

    public void LeaderboardsButtonHandler()
    {
        mainMenuPanel.SetActive(false);
        leaderboardsPanel.SetActive(true);
    }

    public async void UpdateUsername()
    {
        Debug.Log("New Username: " + usernameInputField.text);
        await AuthManager.Instance.UpdatePlayerNameAsync(usernameInputField.text);
    }
}
