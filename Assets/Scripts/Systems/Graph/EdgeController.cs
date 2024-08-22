using UnityEngine;
using UnityEngine.EventSystems;

public class EdgeController : MonoBehaviour
{

    private EdgeState edgeState;
    private LevelManager levelManager;

    void Awake()
    {
        levelManager = FindAnyObjectByType<LevelManager>();
        edgeState = GetComponent<EdgeState>();
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        levelManager.RemoveEdgeFromDataPath(edgeState);
    }
}
