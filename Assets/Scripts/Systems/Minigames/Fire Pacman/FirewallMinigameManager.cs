using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FirewallMinigameManager : MonoBehaviour
{
    public GameObject[] flames;

    public GameObject hackerman;

    public Canvas won;

    public Canvas lost;

    public Canvas start;

    public TMP_Text time;

    public TMP_Text winScore;

    public TMP_Text lossScore;

    public float timeTaken;

    public bool gameOver;

    public void GameOver(bool hasWon)
    {
        Time.timeScale = 0;
        gameOver = true;
        if (hasWon)
        {
            won.enabled = true;
            time.enabled = false;
            winScore.text = "Your time added is " + timeTaken.ToString("F2") + " seconds";
        } else
        {
            lost.enabled = true;
            time.enabled = false;
            lossScore.text = "Your time added is " + timeTaken.ToString("F2") + " seconds";
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        won.enabled = false;
        lost.enabled = false;
        time.enabled = false;
        start.enabled = true;
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && start.enabled == true)
        {
            start.enabled = false;
            time.enabled = true;
            timeTaken = 0f;
            Time.timeScale = 1;
        }
        if (Input.anyKeyDown && gameOver == true)
        {
            
        }
        timeTaken += Time.deltaTime;
        time.text = "Time Taken: " + timeTaken.ToString("F2");
    }
}
