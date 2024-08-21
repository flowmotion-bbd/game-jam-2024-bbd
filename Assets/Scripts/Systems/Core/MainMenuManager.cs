using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void LoadLevel(string levelSceneName)
    {
        gameManager.LoadLevel(levelSceneName);
    }

    public void LoadMinigame(string minigameSceneName)
    {
        gameManager.LoadMinigame(minigameSceneName, false, null);
    }
}
