using UnityEngine;

public class BasicNodeController : NodeController
{
    public override void OnMouseDown()
    {
        levelManager.AddNodeToDataPath(nodeState);
    }

}
