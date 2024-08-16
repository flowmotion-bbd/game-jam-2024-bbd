using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class BasicNodeController : NodeController
{
    public override void OnMouseDown()
    {
        levelManager.AddNodeToDataPath(nodeState);
    }

}
