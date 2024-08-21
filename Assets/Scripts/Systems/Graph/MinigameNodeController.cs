using UnityEngine;

public abstract class MinigameNodeController : NodeController
{
    [SerializeField] protected string minigameSceneName;

    public abstract void MinigameLost();

    public override void OnMouseDown()
    {
        if (nodeState.Node.Compromised)
        {
            levelManager.AddNodeToDataPath(nodeState);
        } else
        {
            levelManager.LoadMinigame(minigameSceneName, nodeState);
        }
    }
}
