using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{    public static PauseManager Instance { get; private set; }

    public static bool isPaused = false;
    public GameObject pauseMenu;

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

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!isPaused)
            {
                Pause();
            } else
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }

    void Pause()
    {
        if (GameManager.Instance.IsScenePausible())
        {
            isPaused = true;
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
    }

    public void BackToMainMenu()
    {
        GameManager.Instance.LoadLevel(GameManager.Instance.DefaultSceneName);
        Resume();
    }

    public void RestartCurrentScene()
    {
        GameManager.Instance.LoadLevel(SceneManager.GetActiveScene().name);
        Resume();
    }
}
