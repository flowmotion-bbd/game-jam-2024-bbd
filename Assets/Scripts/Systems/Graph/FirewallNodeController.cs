using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirewallNodeController : NodeController
{
    public override void OnMouseDown()
    {
        levelManager.AddNodeToDataPath(nodeState);
    }
}
