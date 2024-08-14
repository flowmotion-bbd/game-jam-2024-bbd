using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUIManager : MonoBehaviour
{
    [SerializeField] private GameObject endLevelScreen;

    public void ToggleEndLevelScreen(bool toggle)
    {
        endLevelScreen.SetActive(toggle);
    }
}
