using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class Board : MonoBehaviour
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
    public TileStyle InactiveStyle = new(BACKGROUND, DARK_GREY);
    public TileStyle[] PairStyles = new TileStyle[]
    {
        new TileStyle(PAIR_ONE, PAIR_ONE),
        new TileStyle(PAIR_TWO, PAIR_TWO),
        new TileStyle(PAIR_THREE, PAIR_THREE),
        new TileStyle(PAIR_FOUR, PAIR_FOUR),
        new TileStyle(PAIR_FIVE, PAIR_FIVE),
        new TileStyle(PAIR_SIX, PAIR_SIX),
        new TileStyle(PAIR_SEVEN, PAIR_SEVEN),
        new TileStyle(PAIR_EIGHT, PAIR_EIGHT),
    };

    [Header("Config")]
    public int scoreBonus = -2;
    public int scorePenalty = 4;
    public int maxScore = 30;
    public int numCols = 4;
    public float initialLookingTime = 3.0f;
    public GameObject ReferenceTile;
    public GameObject ReturnButton;
    public GameObject FeedbackBox;
    
    private TextMeshProUGUI feedbackBoxText;
    private GridLayoutGroup gridLayoutGroup;

    private Tile prevClickedTile;
    private Tile currClickedTile;
    private bool allowClicks = false;
    private int correctPairs = 0;
    private int score = 0;
    
    private void Awake()
    {
        feedbackBoxText = FeedbackBox.GetComponentInChildren<TextMeshProUGUI>();
        gridLayoutGroup = gameObject.GetComponent<GridLayoutGroup>();
    }

    private void Start()
    {
        gridLayoutGroup.constraintCount = NUM_ROWS;
        GenerateGrid();
        StartCoroutine(HideTilesDelayed(initialLookingTime));
        toggleFeedback("SCORE: " + score);

        enabled = true;
    }

    private void Update()
    {

    }

    private bool hasWon()
    {
        int targetCorrectPairs = (numCols * NUM_ROWS)/2;
        if (correctPairs == targetCorrectPairs)
            return true;
        return false;
    }

    private void won()
    {
        StopAllCoroutines();

        toggleFeedback("YOU WON!");
        correctPairs = 0;
        allowClicks = false;

        Tile[] tiles = FindObjectsByType<Tile>(FindObjectsSortMode.None);
        foreach (Tile tile in tiles)
        {
            tile.enabled = true;
            tile.Show();
        }

        enabled = false;
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

    public void TileClicked(Tile tile)
    {
        if (!allowClicks) return;

        tile.Show();
        prevClickedTile = currClickedTile;
        currClickedTile = tile;

        if (prevClickedTile != null)
        {
            if (prevClickedTile.Style == currClickedTile.Style && prevClickedTile != currClickedTile)
            {
                score += scoreBonus;

                correctPairs++;
                prevClickedTile.enabled = false;
                currClickedTile.enabled = false;
            }
            else
            {
                score += scorePenalty;
            }

            prevClickedTile = null;
            currClickedTile = null;
            StartCoroutine(HideTilesDelayed(0.2f));

            score = Mathf.Clamp(score, -maxScore, maxScore);
            toggleFeedback("SCORE: " + score);
        }

        if (hasWon())
            won();
    }

    private void GenerateGrid()
    {
        //Get right num styles for grid size considering num imput styles
        List<TileStyle> allStyles = new List<TileStyle>(PairStyles);
        int neededStyles = (numCols * NUM_ROWS) / 2;

        while (allStyles.Count > neededStyles) //Too many styles
        {
            int randomStyleIndex = Random.Range(0, allStyles.Count);
            allStyles.RemoveAt(randomStyleIndex);
        }

        List<TileStyle> addStyles = new List<TileStyle>(PairStyles);
        while (allStyles.Count < neededStyles) //Too few styles
        {
            if (addStyles.Count == 0)
                addStyles = new List<TileStyle>(PairStyles);

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
                Tile tile = tileObj.GetComponent<Tile>();

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
        Tile[] tiles = FindObjectsByType<Tile>(FindObjectsSortMode.None);
        foreach (Tile tile in tiles)
        {
            tile.Hide();
        }
    }

    public void ReturnButtonMethod()
    {
        Debug.Log("Go back to game screen");
        Debug.Log("Score is " +score);
    }

    private void OnEnable()
    {
        ReturnButton.SetActive(false);
    }

    private void OnDisable()
    {
        ReturnButton.SetActive(true);
    }
}