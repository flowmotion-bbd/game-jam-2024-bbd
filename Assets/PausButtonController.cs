using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausButtonController : MonoBehaviour
{
    public void OnPauseClick()
    {
        PauseManager.Instance.Pause();
    }
}
