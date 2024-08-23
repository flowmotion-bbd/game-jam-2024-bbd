using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PatternMatchBoard : MinigameManager
{
    private static readonly int NUM_ROWS = 4;

    private static readonly Color DARK_GREY = new Color32(58, 58, 60, 255);
    private static readonly Color BACKGROUND = new Color32(18, 18, 19, 255);
    private static readonly Color PAIR_ONE = new Color32(220, 190, 255, 255);
    private static readonly Color PAIR_TWO = new Color32(60, 180, 75, 255);
    private static readonly Color PAIR_THREE = new Color32(0, 0, 117, 255);
    private static readonly Color PAIR_FOUR = new Color32(255, 225, 25, 255);
    private static readonly Color PAIR_FIVE = new Color32(128, 0, 0, 255);
    private static readonly Color PAIR_SIX = new Color32(66, 212, 244, 255);
    private static readonly Color PAIR_SEVEN = new Color32(245, 130, 49, 255);
    private static readonly Color PAIR_EIGHT = new Color32(255, 255, 255, 255);

    [Header("Styles")]
    public PatternMatchTileStyle InactiveStyle = new(BACKGROUND, DARK_GREY);
    public PatternMatchTileStyle[] PairStyles = new PatternMatchTileStyle[]
    {
        new PatternMatchTileStyle(PAIR_ONE, PAIR_ONE),
        new PatternMatchTileStyle(PAIR_TWO, PAIR_TWO),
        new PatternMatchTileStyle(PAIR_THREE, PAIR_THREE),
        new PatternMatchTileStyle(PAIR_FOUR, PAIR_FOUR),
        new PatternMatchTileStyle(PAIR_FIVE, PAIR_FIVE),
        new PatternMatchTileStyle(PAIR_SIX, PAIR_SIX),
        new PatternMatchTileStyle(PAIR_SEVEN, PAIR_SEVEN),
        new PatternMatchTileStyle(PAIR_EIGHT, PAIR_EIGHT),
    };

    [Header("Config")]
    public int scoreBonus = -2;
    public int scorePenalty = 4;
    public int maxScore = 30;
    public int numCols = 4;
    public float initialLookingTime = 3.0f;
    public GameObject ReferenceTile;
    public GameObject FeedbackBox;
    public TimerSlider timerSlider;
    
    private TextMeshProUGUI feedbackBoxText;
    private GridLayoutGroup gridLayoutGroup;

    private PatternMatchTile prevClickedTile;
    private PatternMatchTile currClickedTile;
    private bool allowClicks = false;
    private int correctPairs = 0;
    
    private void Awake()
    {
        feedbackBoxText = FeedbackBox.GetComponentInChildren<TextMeshProUGUI>();
        gridLayoutGroup = gameObject.GetComponent<GridLayoutGroup>();
    }

    private new void Start()
    {
        base.Start();

        gridLayoutGroup.constraintCount = NUM_ROWS;
        GenerateGrid();
        HideAllTiles();
        toggleFeedback("SCORE: " + scoreAchieved);
    }

    private new void Update()
    {
        base.Update();
    }

    private bool HasWon()
    {
        int targetCorrectPairs = (numCols * NUM_ROWS)/2;
        if (correctPairs == targetCorrectPairs)
            return true;
        return false;
    }

    private void Won()
    {
        StopAllCoroutines();

        correctPairs = 0;
        allowClicks = false;

        ShowAllTiles();

        GameOver(true);
    }

    private void toggleFeedback(string feedback = "")
    {
        if (feedback == "")
        {
            FeedbackBox.SetActive(false);
        }
        else
        {
            feedbackBoxText.text = feedback;
            FeedbackBox.SetActive(true);
        }
    }

    public void TileClicked(PatternMatchTile tile)
    {
        if (!allowClicks) return;

        tile.Show();
        prevClickedTile = currClickedTile;
        currClickedTile = tile;

        if (prevClickedTile != null)
        {
            if (prevClickedTile.Style == currClickedTile.Style && prevClickedTile != currClickedTile)
            {
                scoreAchieved += scoreBonus;

                correctPairs++;
                prevClickedTile.enabled = false;
                currClickedTile.enabled = false;
            }
            else
            {
                scoreAchieved += scorePenalty;
            }

            prevClickedTile = null;
            currClickedTile = null;
            StartCoroutine(HideTilesDelayed(0.2f));

            scoreAchieved = Mathf.Clamp(scoreAchieved, -maxScore, maxScore);
            toggleFeedback("SCORE: " + scoreAchieved);
        }

        if (HasWon())
        {
            Won();
        }
    }

    private void GenerateGrid()
    {
        //Get right num styles for grid size considering num imput styles
        List<PatternMatchTileStyle> allStyles = new List<PatternMatchTileStyle>(PairStyles);
        int neededStyles = (numCols * NUM_ROWS) / 2;

        while (allStyles.Count > neededStyles) //Too many styles
        {
            int randomStyleIndex = Random.Range(0, allStyles.Count);
            allStyles.RemoveAt(randomStyleIndex);
        }

        List<PatternMatchTileStyle> addStyles = new List<PatternMatchTileStyle>(PairStyles);
        while (allStyles.Count < neededStyles) //Too few styles
        {
            if (addStyles.Count == 0)
                addStyles = new List<PatternMatchTileStyle>(PairStyles);

            int randomStyleIndex = Random.Range(0, addStyles.Count);
            allStyles.Add(addStyles[randomStyleIndex]);
            addStyles.RemoveAt(randomStyleIndex);
        }

        //Double the styles up because pairs
        allStyles.AddRange(allStyles);

        //Generate the grid
        for (int row = 0; row < NUM_ROWS; row++)
        {
            for (int col = 0; col < numCols; col++)
            {
                GameObject tileObj = Instantiate(ReferenceTile, transform);                    
                PatternMatchTile tile = tileObj.GetComponent<PatternMatchTile>();

                int randomStyleIndex = Random.Range(0, allStyles.Count);
                tile.Style = allStyles[randomStyleIndex];
                allStyles.RemoveAt(randomStyleIndex);
                tile.InactiveStyle = InactiveStyle;
                tile.OnTileClicked += TileClicked;
            }
        }
    }

    IEnumerator HideTilesDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        HideAllTiles();
        allowClicks = true;
    }

    public void HideAllTiles()
    {
        PatternMatchTile[] tiles = FindObjectsByType<PatternMatchTile>(FindObjectsSortMode.None);
        foreach (PatternMatchTile tile in tiles)
        {
            tile.Hide();
        }
    }

    public void ShowAllTiles()
    {
        PatternMatchTile[] tiles = FindObjectsByType<PatternMatchTile>(FindObjectsSortMode.None);
        foreach (PatternMatchTile tile in tiles)
        {
            tile.enabled = true;
            tile.Show();
        }
    }

    public void ReturnButtonMethod()
    {
        Debug.Log("Go back to game screen");
        Debug.Log("Score is " +scoreAchieved);
    }

    protected override void StartMinigame()
    {
        ShowAllTiles();
        StartCoroutine(HideTilesDelayed(initialLookingTime));
        timerSlider.StartTimerSliderTransition(initialLookingTime);
    }
}