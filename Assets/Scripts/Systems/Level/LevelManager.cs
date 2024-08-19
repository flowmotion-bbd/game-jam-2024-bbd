using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private GraphController graphController;
    private GraphState graphState;
    private GraphRenderer graphRenderer;

    private int currentDataPathIndex = 0;

    private LevelUIManager levelUIManager;

    private float elapsedTime = 0f;
    private bool isTiming = false;

    private float countDownTime = 3f;
    private bool isCountingDown = false;

    private bool isInMinigame = false;
    private string minigameSceneName = string.Empty;
    private NodeState minigameNodeState; 

    [SerializeField] private List<GameObject> levelObjects = new List<GameObject>();

    private DialogueManager dialogueManager;
    [SerializeField] private Dialogue levelStartDialogue;

    public int CurrentDataPathIndex
    {
        get { return currentDataPathIndex; }
        set
        {
            if (currentDataPathIndex >= 0 && currentDataPathIndex < graphState.DataPaths.Count)
            {
                currentDataPathIndex = value;
            }
        }
    }
    void Start()
    {
        graphController = FindAnyObjectByType<GraphController>();
        graphState = FindAnyObjectByType<GraphState>();
        graphRenderer = FindAnyObjectByType<GraphRenderer>();

        levelUIManager = FindAnyObjectByType<LevelUIManager>();

        dialogueManager = DialogueManager.Instance;

        elapsedTime = 0f;

        ShowLevelObjects();
        LevelStartDialogue();
    }

    void LevelStartDialogue()
    {
        dialogueManager.StartDialogue(levelStartDialogue, StartCountDown);
    }

    void HideLevelObjects()
    {
        foreach (GameObject obj in levelObjects)
        {
            obj.SetActive(false);
        }
    }
    void ShowLevelObjects()
    {
        foreach (GameObject obj in levelObjects)
        {
            obj.SetActive(true);
        }
    }

    void Update()
    {
        if (!isInMinigame)
        {
            if (LevelFinished())
            {
                StopTimer();
                FindFirstObjectByType<LevelUIManager>().ShowEndLevelScreen(new List<float>() { 20, 20, 20 }, elapsedTime);
            }

            for (int i = 0; i <= 9; i++)
            {
                KeyCode mainKey = KeyCode.Alpha0 + i;   // Main number keys (0-9)
                KeyCode numpadKey = KeyCode.Keypad0 + i; // Numeric keypad keys (0-9)

                if (Input.GetKeyDown(mainKey) || Input.GetKeyDown(numpadKey))
                {
                    currentDataPathIndex = i - 1;
                }
            }

            if (isTiming)
            {
                elapsedTime += Time.deltaTime;
                levelUIManager.UpdateTimerDisplay(elapsedTime);
            }
            else if (countDownTime < 0f)
            {
                countDownTime = 3f;
                isCountingDown = false;
                levelUIManager.HideCountdown();
                StartTimer();
            }
            else if (isCountingDown)
            {
                countDownTime -= Time.deltaTime;
                levelUIManager.UpdateCountdownDisplay(countDownTime);
            }

            graphRenderer.UpdateGraph(currentDataPathIndex);
        } else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Escaped");
                MinigameCallback(false, 5f, new Dialogue());
            }
        }
    }

    bool LevelFinished()
    {
        foreach (DataPath dataPath in graphState.DataPaths)
        {
            if (!graphState.Graph.EndNodes.Contains(dataPath.Path.Last()))
            {
                return false;
            } else if (!dataPath.IsValid())
            {
                return false;
            }
        }

        return true;
    }

    void StartCountDown()
    {
        countDownTime = 3f;
        isCountingDown = true;
        levelUIManager.ShowCountdown();
    }

    void StartTimer()
    {
        isTiming = true;
    }

    void StopTimer()
    {
        isTiming = false;
    }

    public void AddNodeToDataPath(NodeState nodeState)
    {
        if (!isTiming)
        {
            return;
        }

        if (!nodeState.Node.Compromised)
        {
            elapsedTime += nodeState.Node.CompromisationTime;
        }

        graphController.AddNodeToDataPath(currentDataPathIndex, nodeState);
    }

    public void RemoveEdgeFromDataPath(EdgeState edgeState)
    {
        if (!isTiming)
        {
            return;
        }

        graphController.RemoveEdgeFromDataPath(currentDataPathIndex, edgeState);
    }

    public void RestartLevel()
    {
        currentDataPathIndex = 0;
        graphController.ResetState();
        levelUIManager.HideEndLevelScreen();
        StartCountDown();
    }

    public void LoadMinigame(string minigameSceneName, NodeState nodeState)
    {
        if (!isTiming)
        {
            return;
        }

        if (graphState.RetrieveEdge(graphState.DataPaths[currentDataPathIndex].Path.Last(), nodeState) != null)
        {
            this.minigameSceneName = minigameSceneName;
            this.isInMinigame = true;
            this.isTiming = false;
            minigameNodeState = nodeState;
            HideLevelObjects();
            SceneManager.LoadScene(minigameSceneName, LoadSceneMode.Additive);
            SceneManager.sceneLoaded += MinigameLoaded;
        }
    }

    void UnloadMinigame()
    {
        SceneManager.UnloadSceneAsync(minigameSceneName);
        SceneManager.sceneUnloaded += MinigameUnloaded;
    }

    void MinigameLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        if (scene.name == minigameSceneName)
        {
            // Perform the call here
            Debug.Log("Scene has been fully loaded!");

            // Unsubscribe from the event to avoid multiple calls
            SceneManager.sceneLoaded -= MinigameLoaded;
        }
    }

    void MinigameUnloaded(UnityEngine.SceneManagement.Scene scene)
    {
        if (scene.name == minigameSceneName)
        {
            ShowLevelObjects();
            // Perform the call here
            Debug.Log("Scene has been fully unloaded!");

            // Unsubscribe from the event to avoid multiple calls
            SceneManager.sceneUnloaded -= MinigameUnloaded;
        }
    }

    public void MinigameCallback(bool won, float timeChange, Dialogue dialogue)
    {
        elapsedTime += timeChange;
        UnloadMinigame();
        ShowLevelObjects();
        isInMinigame = false;

        if (won)
        {
            AddNodeToDataPath(minigameNodeState);
        } else
        {
            minigameNodeState.GetComponent<MinigameNodeController>().MinigameLost();
        }

        dialogueManager.StartDialogue(dialogue, StartCountDown);
    }

    public void RemoveEdgeFromGraph(NodeState nodeState)
    {
        graphController.RemoveEdge(graphState.DataPaths[currentDataPathIndex].Path.Last(), nodeState);
    }
}
