using UnityEngine;

public class FirewallNodeController : MinigameNodeController
{
    public override void MinigameLost()
    {
        levelManager.RemoveEdgeFromGraph(nodeState);
    }
}
