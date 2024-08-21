using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AsyncLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject gameScreen;
    public Slider slider;
    
    public void LoadLevel(int level)
    {
        gameScreen.SetActive(false);
        gameScreen.SetActive(true);
        StartCoroutine(LoadLevelAsync(level));
    }

    IEnumerator LoadLevelAsync(int level)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(level);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progressValue;
            yield return null;
        }
    }
}
