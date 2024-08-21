using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class EdgeRendererEditor
{
    [System.Obsolete]
    static EdgeRendererEditor()
    {
        EditorApplication.update += UpdateEdge;
    }

    [System.Obsolete]
    private static void UpdateEdge()
    {

        foreach (var edgeRenderer in Object.FindObjectsOfType<EdgeRenderer>())
        {
            edgeRenderer.UpdateLine();
        }
    }
}