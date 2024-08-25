using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CentralPivot : MinigameManager
{
    private static readonly Color COL_ONE = new Color32(220, 190, 255, 255);
    private static readonly Color COL_TWO = new Color32(60, 180, 75, 255);
    private static readonly Color COL_THREE = new Color32(0, 0, 117, 255);
    private static readonly Color COL_FOUR = new Color32(255, 225, 25, 255);
    private static readonly Color COL_FIVE = new Color32(128, 0, 0, 255);
    private static readonly Color COL_SIX = new Color32(66, 212, 244, 255);
    private static readonly Color COL_SEVEN = new Color32(245, 130, 49, 255);

    private readonly float refPivotAngleDeg = 0f;
    private readonly float refCornerAngle = Mathf.Atan(((float)Screen.height / 2) / ((float)Screen.width / 2));

    [Header("Styles")]
    public TileStyle[] ProjStyles = new TileStyle[]
    {
        new TileStyle(COL_ONE, COL_ONE),
        new TileStyle(COL_TWO, COL_TWO),
        new TileStyle(COL_THREE, COL_THREE),
        new TileStyle(COL_FOUR, COL_FOUR),
        new TileStyle(COL_FIVE, COL_FIVE),
        new TileStyle(COL_SIX, COL_SIX),
        new TileStyle(COL_SEVEN, COL_SEVEN),
    };

    [Header("Config")]
    public GameObject ReferenceProjectile;
    public GameObject SpawnTile;
    public GameObject FeedbackBox;
    public float initialSpawnInterval = 3f;
    public float spawnIntervalMultiplier = 0.9f;
    public float minSpawnInterval = 0.1f;
    public float initialScore = 10f;
    public float initialSpeedMultiplier = 150f;
    public float speedMultiplierStep = 40f;
    public int totalNumLives = 5;
    public float scoreBonus = -1;
    public float maxScore = 30;

    private Transform pivotArrow;
    private GameObject[] edgeTiles;
    private float pivotAngleDeg = 90f;
    private float refCornerAngleDeg;
    private float cornerXTrans;
    private float cornerYTrans;
    private int livesRemaining;
    private float speedMultiplier;
    private float spawnInterval;
    private TextMeshProUGUI feedbackBoxText;

    private string targetPos = "right";
    private float xTrans = 1f;
    private float yTrans = 0f;

    public string TargetPos { get => targetPos; }
    public float XTrans { get => xTrans; }
    public float YTrans { get => yTrans; }

    public void ProjDestroy(bool didHit, bool stylesMatched, bool shouldHaveHit)
    {
        if (minigameInProgress)
        {
            if (didHit && stylesMatched)
            {
                scoreAchieved += scoreBonus;
                scoreAchieved = Mathf.Clamp(scoreAchieved, -maxScore, maxScore);

                speedMultiplier += speedMultiplierStep;
            }
            else if (!didHit && !shouldHaveHit)
            {
                spawnInterval *= spawnIntervalMultiplier;
                spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval);
            }
            else
            {
                livesRemaining -= 1;
            }

            feedbackBoxText.text = "LIVES: " + livesRemaining + "\nSCORE: " + scoreAchieved;

            if (livesRemaining <= 0)
            {
                feedbackBoxText.text = "GAME OVER!\nSCORE: " + scoreAchieved;
                GameOver(true);
            }
        }
    }

    private void Awake()
    {
        pivotArrow = transform.GetChild(0).gameObject.GetComponentInChildren<Transform>();
        edgeTiles = GameObject.FindGameObjectsWithTag("EdgeTile");
    }

    private new void Start()
    {
        base.Start();

        GenerateEdgeTiles();

        scoreAchieved = initialScore;
        spawnInterval = initialSpawnInterval;
        speedMultiplier = initialSpeedMultiplier;
        feedbackBoxText = FeedbackBox.GetComponentInChildren<TextMeshProUGUI>();
        livesRemaining = totalNumLives;
        refCornerAngleDeg = refCornerAngle * Mathf.Rad2Deg;
        cornerXTrans = Mathf.Cos(refCornerAngle);
        cornerYTrans = Mathf.Sin(refCornerAngle);
        feedbackBoxText.text = "LIVES: " + livesRemaining + "\nSCORE: " + scoreAchieved;
    }

    IEnumerator SpawnProj()
    {
        while (minigameInProgress)
        {
            GameObject newProj = Instantiate(ReferenceProjectile, SpawnTile.GetComponentInChildren<Transform>());
            Projectile proj = newProj.GetComponent<Projectile>();
            int randomStyle = UnityEngine.Random.Range(0, ProjStyles.Length);
            proj.Setup(1f, 0f, speedMultiplier, ProjStyles[randomStyle], this);
            proj.enabled = true;

            yield return new WaitForSeconds(spawnInterval);
            Debug.Log(spawnInterval);
        }
    }

    private void GenerateEdgeTiles()
    {
        List<TileStyle> allStyles = new List<TileStyle>(ProjStyles);

        foreach (GameObject edgeTileObj in edgeTiles)
        {
            int randomStyleIndex = UnityEngine.Random.Range(0, allStyles.Count);
            EdgeTile edgeTile = edgeTileObj.GetComponent<EdgeTile>();
            edgeTile.Style = allStyles[randomStyleIndex];
            allStyles.RemoveAt(randomStyleIndex);
        }
    }

    private new void Update()
    {
        base.Update();

        processKeyPresses();

        if (
            Input.GetKey(KeyCode.UpArrow) || 
            Input.GetKey(KeyCode.LeftArrow) || 
            Input.GetKey(KeyCode.DownArrow) || 
            Input.GetKey(KeyCode.RightArrow) ||
            Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D)
        )
        {
            var eulerAngles = pivotArrow.eulerAngles;
            pivotArrow.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, pivotAngleDeg);
        }
    }

    private void processKeyPresses()
    {
        if (
            (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow)) ||
            (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D))
        )
        {
            pivotAngleDeg = refPivotAngleDeg + refCornerAngleDeg;
            targetPos = "top-right";
            xTrans = cornerXTrans;
            yTrans = cornerYTrans;
        }
        else if (
            (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow)) ||
            (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        )
        {
            Debug.Log("HERE");
            pivotAngleDeg = refPivotAngleDeg - refCornerAngleDeg;
            targetPos = "down-right";
            xTrans = cornerXTrans;
            yTrans = -cornerYTrans;
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            pivotAngleDeg = refPivotAngleDeg + 90f;
            targetPos = "up";
            xTrans = 0f;
            yTrans = 1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            pivotAngleDeg = refPivotAngleDeg;
            targetPos = "right";
            xTrans = 1f;
            yTrans = 0f;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            pivotAngleDeg = refPivotAngleDeg - 90f;
            targetPos = "down";
            xTrans = 0f;
            yTrans = -1f;
        }
    }

    protected override void StartMinigame()
    {
        minigameInProgress = true;
        StartCoroutine(SpawnProj());
    }
}