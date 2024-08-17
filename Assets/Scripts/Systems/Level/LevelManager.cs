using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private GraphController graphController;
    private GraphState graphState;
    private GraphRenderer graphRenderer;

    private LevelUIManager levelUIManager;

    private float elapsedTime = 0f;
    private bool isTiming = false;

    private float countDownTime = 3f;
    private bool isCountingDown = false;

    void Start()
    {
        graphController = FindAnyObjectByType<GraphController>();
        graphState = FindAnyObjectByType<GraphState>();
        graphRenderer = FindAnyObjectByType<GraphRenderer>();

        levelUIManager = FindAnyObjectByType<LevelUIManager>();

        StartCountDown();
    }

    void Update()
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
                graphState.GetComponent<GraphController>().CurrentDataPathIndex = i - 1;
            }
        }

        if (isTiming)
        {
            elapsedTime += Time.deltaTime;
            levelUIManager.UpdateTimerDisplay(elapsedTime);
        } else if (countDownTime < 0f)
        {
            countDownTime = 3f;
            isCountingDown = false;
            levelUIManager.HideCountdown();
            StartTimer();
        } else if (isCountingDown)
        {
            countDownTime -= Time.deltaTime;
            levelUIManager.UpdateCountdownDisplay(countDownTime);
        }

        graphRenderer.UpdateGraph();
    }

    bool LevelFinished()
    {
        foreach (DataPath dataPath in graphState.DataPaths)
        {
            if (!graphState.Graph.EndNodes.Contains(dataPath.Path.Last()))
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
        elapsedTime = 0f;
        isTiming = true;
    }

    void StopTimer()
    {
        isTiming = false;
    }

    public void AddNodeToDataPath(NodeState nodeState)
    {
        if (!nodeState.Node.Compromised)
        {
            elapsedTime += nodeState.Node.CompromisationTime;
        }

        graphController.AddNodeToDataPath(nodeState);
    }

    public void RemoveEdgeFromDataPath(EdgeState edgeState)
    {
        graphController.RemoveEdgeFromDataPath(edgeState);
    }

    public void RestartLevel()
    {
        graphController.ResetState();
        levelUIManager.HideEndLevelScreen();
        StartCountDown();
    }
}
