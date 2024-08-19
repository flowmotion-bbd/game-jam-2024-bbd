using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private string defaultSceneName = "Main Menu";

    private void Awake()
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
}
