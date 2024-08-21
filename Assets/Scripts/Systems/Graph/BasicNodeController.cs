using UnityEngine.EventSystems;

public class BasicNodeController : NodeController
{
    public override void OnMouseDown()
    {
        levelManager.AddNodeToDataPath(nodeState);
    }

}
