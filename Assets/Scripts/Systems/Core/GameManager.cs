using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private string defaultSceneName = "Main Menu";

    private TransitionManager transitionManager;
    private string currentMinigameSceneName = string.Empty;

    public string DefaultSceneName
    {
        get { return defaultSceneName; }
        set { defaultSceneName = value; }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        transitionManager = TransitionManager.Instance;
    }

    public void MinigameEnded(bool won, float timeChange, Dialogue dialogue)
    {
        LevelManager levelManager = FindAnyObjectByType<LevelManager>();

        if (levelManager != null)
        {
            Debug.Log("LevelManager Found");
            UnloadMinigame(currentMinigameSceneName, true, () => levelManager.MinigameCallback(won, timeChange, dialogue));
        }
        else
        {
            UnloadMinigame("Main Menu", false, null);
        }
    }

    public void LoadLevel(string levelSceneName)
    {
        SceneManager.LoadScene(levelSceneName);
    }

    public void LoadMinigame(string minigameSceneName, bool additive)
    {
        currentMinigameSceneName = minigameSceneName;
        transitionManager.TransitionToMinigame(minigameSceneName, additive);
    }

    public void UnloadMinigame(string minigameSceneName, bool additive, Action unloadCallback)
    {
        transitionManager.TransitionFromMinigame(minigameSceneName, additive, unloadCallback);
    }

    public bool IsScenePausible()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.name != defaultSceneName;
    }
}
