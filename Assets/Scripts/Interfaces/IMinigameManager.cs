using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class MinigameManager : MonoBehaviour
{
    LevelManager levelManager;
    private string defaultScene = "Main Menu";

    void Start()
    {
        levelManager = FindAnyObjectByType<LevelManager>();    
    }

    void MinigameEnded(float timeChange)
    {
        if (levelManager != null)
        {
            levelManager.MinigameEndedCallback(timeChange);
        } else
        {
            SceneManager.LoadScene(defaultScene);
        }
    }
}
