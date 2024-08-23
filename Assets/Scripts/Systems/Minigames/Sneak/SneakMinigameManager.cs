using TMPro;
using UnityEngine;

public class SneakMinigameManager : MinigameManager
{

    [SerializeField] private TMP_Text time;

    private float timeTaken = 0f;

    protected override void StartMinigame()
    {
        minigameInProgress = true;
    }

    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    private new void Update()
    {
        base.Update();

        if (minigameInProgress)
        {
            timeTaken += Time.deltaTime;
            time.text = "Time Taken: " + timeTaken.ToString("F2");
        }
    }
}
