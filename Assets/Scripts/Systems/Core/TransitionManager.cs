using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    private bool transitioning = false;

    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI tranitionText;
    [SerializeField] private GameObject tranitionPanel;

    private string ipAddress = string.Empty;

    public static TransitionManager Instance { get; private set; }

    public bool Transitioning
    {
        get { return transitioning; }
        set { transitioning = value; }
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TransitionBetweenMinigames(string sceneName, bool additive, Action transitionCallback)
    {
        tranitionPanel.SetActive(true);
        transitioning = true;
        animator.SetBool("IsTransitioning", true);
        StartCoroutine(LoadMinigameScene(sceneName, additive, transitionCallback));
    }

    public void TransitionOut()
    {
        animator.SetBool("IsTransitioning", false);
        tranitionPanel.SetActive(false);
        transitioning = false;

        MinigameManager minigameManager = FindAnyObjectByType<MinigameManager>();
        if (minigameManager != null)
        {
            minigameManager.StartMinigameDialogue();
        }
    }

    IEnumerator TransitionText()
    {
        ipAddress = RandomIPAddress();
        tranitionText.text = "Connecting to " + ipAddress + " ";
        while (transitioning)
        {
            yield return new WaitForSeconds(0.5f);

            if (tranitionText.text.Contains("..."))
            {
                tranitionText.text = "Connecting to " + ipAddress + " ";
            } else
            {
                tranitionText.text += ".";
            }
        }
    }

    IEnumerator LoadMinigameScene(string sceneName, bool additive, Action transitionCallback)
    {
        StartCoroutine("TransitionText");

        float startTime = Time.time;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, additive ? LoadSceneMode.Additive : LoadSceneMode.Single);
        asyncLoad.allowSceneActivation = false;

        // Wait until the minimum time has passed and the scene is ready to activate
        while (!asyncLoad.isDone)
        {
            // If the scene is fully loaded, but the minimum time hasn't passed, wait
            if (asyncLoad.progress >= 0.9f && Time.time - startTime >= 3f)
            {
                asyncLoad.allowSceneActivation = true;
                transitioning = false;
                StopCoroutine("TransitionText");
            }

            yield return null;
        }

        if (transitionCallback != null)
        {
            transitionCallback();
        }

        tranitionText.text = "Successfully Connected to " + ipAddress;

        yield return new WaitForSeconds(1f);

        TransitionOut();
    }

    string RandomIPAddress()
    {
        System.Random random = new System.Random();
        int rangeSelector = random.Next(1, 4);

        string ipAddress = string.Empty;

        switch (rangeSelector)
        {
            case 1:
                // Generate an IP in the range 10.0.0.0 - 10.255.255.255
                ipAddress = $"10.{random.Next(0, 256)}.{random.Next(0, 256)}.{random.Next(1, 255)}";
                break;

            case 2:
                // Generate an IP in the range 172.16.0.0 - 172.31.255.255
                ipAddress = $"172.{random.Next(16, 32)}.{random.Next(0, 256)}.{random.Next(1, 255)}";
                break;

            case 3:
                // Generate an IP in the range 192.168.0.0 - 192.168.255.255
                ipAddress = $"192.168.{random.Next(0, 256)}.{random.Next(1, 255)}";
                break;
        }

        return ipAddress;
    }
}
