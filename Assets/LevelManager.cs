using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class LevelManager : MonoBehaviour
{
    private GraphState graphState;

    void Start()
    {
        graphState = FindFirstObjectByType<GraphState>();
    }

    void Update()
    {
        if (LevelFinished())
        {
            FindFirstObjectByType<LevelUIManager>().ToggleEndLevelScreen(true);
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
}
