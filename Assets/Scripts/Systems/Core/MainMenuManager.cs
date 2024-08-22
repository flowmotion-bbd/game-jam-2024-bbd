using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Pannels")]
    [SerializeField] GameObject MainMenuPanel = null;
    [SerializeField] GameObject LevelSelectPanel = null;
    [SerializeField] GameObject MinigameSelectPanel = null;
    [SerializeField] GameObject HowToPlayPanel = null;

    const string START_LEVEL_NAME = "Level 1";

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
    }
    public void StartGame()
    {
        gameManager.LoadLevel(START_LEVEL_NAME);
    }

    public void LoadLevel(string levelSceneName)
    {
        gameManager.LoadLevel(levelSceneName);
    }

    public void LoadMinigame(string minigameSceneName)
    {
        gameManager.LoadMinigame(minigameSceneName, false, null);
    }

    public void LevelSelectButtonHandler()
    {
        MainMenuPanel.SetActive(false);
        LevelSelectPanel.SetActive(true);
    }

    public void MiniGameSelectButtonHandler()
    {
        MainMenuPanel.SetActive(false);
        MinigameSelectPanel.SetActive(true);
    }

    public void HowToPlayButtonHandler()
    {
        MainMenuPanel.SetActive(false);
        HowToPlayPanel.SetActive(true);
    }

    public void BackToMainMenu()
    {
        MainMenuPanel.SetActive(true);
        MinigameSelectPanel.SetActive(false);
        HowToPlayPanel.SetActive(false);
        LevelSelectPanel.SetActive(false);
    }
}
