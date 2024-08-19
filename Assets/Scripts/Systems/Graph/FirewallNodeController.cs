using UnityEngine;

public class FirewallNodeController : MinigameNodeController
{
    [SerializeField] private string minigameSceneName;

    public override void MinigameLost()
    {
        levelManager.RemoveEdgeFromGraph(nodeState);
    }

    public override void OnMouseDown()
    {
        levelManager.LoadMinigame(minigameSceneName, nodeState);
    }
}
