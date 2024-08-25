using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

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

    private GameManager gameManager;

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
    public bool IsTiming
    {
        get { return isTiming; }
    }

    void Start()
    {
        graphController = FindAnyObjectByType<GraphController>();
        graphState = FindAnyObjectByType<GraphState>();
        graphRenderer = FindAnyObjectByType<GraphRenderer>();

        levelUIManager = FindAnyObjectByType<LevelUIManager>();

        dialogueManager = DialogueManager.Instance;
        gameManager = GameManager.Instance;

        elapsedTime = 0f;

        ShowLevelObjects();
        LevelStartDialogue();
    }

    void LevelStartDialogue()
    {
        dialogueManager.StartDialogue(levelStartDialogue, StartCountDown);
    }

    void StartCountDown()
    {
        StartCoroutine(CountDown());
    }

    public void HideLevelObjects()
    {
        foreach (GameObject obj in levelObjects)
        {
            obj.SetActive(false);
        }
    }

    public void ShowLevelObjects()
    {
        Debug.Log("Showing Level Objects");
        foreach (GameObject obj in levelObjects)
        {
            obj.SetActive(true);
        }
    }

    void Update()
    {
        if (!isInMinigame)
        {
            if (isTiming)
            {
                elapsedTime += Time.deltaTime;
                levelUIManager.UpdateTimerDisplay(elapsedTime);
                if (LevelFinished())
                {
                    StopTimer();
                    levelUIManager.ShowEndLevelScreen(elapsedTime);
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
            }

            graphRenderer.UpdateGraph(currentDataPathIndex);
        }
    }

    IEnumerator CountDown()
    {
        countDownTime = 3f;
        isCountingDown = true;
        levelUIManager.ShowCountdown();

        while (countDownTime > 0f)
        {
            countDownTime -= 2*Time.deltaTime;

            if (countDownTime < 0)
            {
                countDownTime = 0;
            }

            levelUIManager.UpdateCountdownDisplay(countDownTime);

            yield return null;
        }

        isCountingDown = false;
        levelUIManager.HideCountdown();
        StartTimer();
        RestartCompromising();
    }

    void RestartCompromising()
    {
        foreach (DataPath dataPath in graphState.DataPaths)
        {
            NodeState nodeState = dataPath.Path.Last();
            if (!nodeState.Node.Compromised)
            {
                nodeState.GetComponent<NodeController>().CompromiseNode(dataPath);
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
        Debug.Log("RestartLevel");
    }

    public void LoadMinigame(string minigameSceneName, NodeState nodeState)
    {
        if (!isTiming || !graphState.DataPaths[currentDataPathIndex].Enabled)
        {
            return;
        }

        if (graphState.RetrieveEdge(graphState.DataPaths[currentDataPathIndex].Path.Last(), nodeState) != null)
        {
            this.minigameSceneName = minigameSceneName;
            this.isInMinigame = true;
            this.isTiming = false;
            minigameNodeState = nodeState;
            gameManager.LoadMinigame(minigameSceneName, true);
        }
    }

    public void MinigameCallback(bool won, float timeChange, Dialogue dialogue)
    {
        elapsedTime += timeChange;
        ShowLevelObjects();
        isInMinigame = false;

        if (won)
        {
            graphController.AddNodeToDataPath(currentDataPathIndex, minigameNodeState);
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

    public void ReturnToMainMenu()
    {
        gameManager.LoadLevel("Main Menu");
    }
}
