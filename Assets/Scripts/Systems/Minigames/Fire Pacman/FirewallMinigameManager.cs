using TMPro;
using UnityEngine;

public class FirewallMinigameManager : MinigameManager
{
    public GameObject[] flames;

    public GameObject hackerman;

    public TMP_Text time;

    public float timeTaken;

    protected override void StartMinigame()
    {
        timeTaken = 0f;
        minigameInProgress = true;
    }

    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();

        time.text = "";
    }

    private new void Update()
    {
        base.Update();

        if (minigameInProgress)
        {
            timeTaken += Time.deltaTime;
            time.text = "Time Taken: " + timeTaken.ToString("F2");
            scoreAchieved = timeTaken;
        }
    }
}
