using UnityEngine;
using UnityEngine.EventSystems;

public class FirewallNodeController : MinigameNodeController
{
    [SerializeField] private string minigameSceneName;

    public override void MinigameLost()
    {
        levelManager.RemoveEdgeFromGraph(nodeState);
    }

    public override void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        levelManager.LoadMinigame(minigameSceneName, nodeState);
    }
}
