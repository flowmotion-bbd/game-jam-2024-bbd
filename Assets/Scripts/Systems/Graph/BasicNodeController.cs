using UnityEngine;

public class BasicNodeController : NodeController
{
    public override void OnMouseDown()
    {
        Debug.Log("Clicked");
        levelManager.AddNodeToDataPath(nodeState);
    }

}
