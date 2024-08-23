using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private string defaultSceneName = "Main Menu";

    private TransitionManager transitionManager;

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
            levelManager.MinigameCallback(won, timeChange, dialogue);
        }
        else
        {
            SceneManager.LoadScene(defaultSceneName);
        }
    }

    public void LoadLevel(string levelSceneName)
    {
        SceneManager.LoadScene(levelSceneName);
    }

    public void LoadMinigame(string minigameSceneName, bool additive, Action transitionCallback)
    {
        transitionManager.TransitionBetweenMinigames(minigameSceneName, additive, transitionCallback);
    }

    public void UnloadMinigame(string minigameSceneName)
    {
        SceneManager.UnloadSceneAsync(minigameSceneName);
    }

    public bool IsScenePausible()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        return currentScene.name != defaultSceneName;
    }
}
