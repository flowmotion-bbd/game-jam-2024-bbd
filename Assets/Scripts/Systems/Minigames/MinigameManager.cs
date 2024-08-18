using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MinigameManager : MonoBehaviour
{
    protected GameManager gameManager;
    protected bool minigameWon = false;

    protected void Start()
    {
        gameManager = GetComponent<GameManager>();
    }
}
