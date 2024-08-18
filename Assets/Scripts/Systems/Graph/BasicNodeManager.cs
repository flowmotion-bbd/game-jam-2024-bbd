using UnityEngine.EventSystems;

public class BasicNodeController : NodeController
{
    public override void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        levelManager.AddNodeToDataPath(nodeState);
    }

}
