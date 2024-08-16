using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EdgeController : MonoBehaviour
{

    private EdgeState edgeState;
    private LevelManager levelManager;

    void Awake()
    {
        levelManager = FindAnyObjectByType<LevelManager>();
        edgeState = GetComponent<EdgeState>();
    }

    void OnMouseDown()
    {
        levelManager.RemoveEdgeFromDataPath(edgeState);
    }
}
