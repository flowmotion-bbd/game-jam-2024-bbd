using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButtonController : MonoBehaviour
{
    public void OnPauseClick()
    {
        PauseManager.Instance.Pause();
    }
}
