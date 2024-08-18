using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirewallNodeController : NodeController
{
    [SerializeField] private string minigameSceneName;

    public override void OnMouseDown()
    {
        levelManager.LoadMinigame(minigameSceneName, GetComponent<NodeState>());
    }
}
