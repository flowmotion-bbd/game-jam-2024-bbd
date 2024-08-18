using TMPro;
using UnityEngine;

public class Board : MinigameManager
{
    private static readonly KeyCode[] KEYS = new KeyCode[] {
        KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F,
        KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L,
        KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R,
        KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X,
        KeyCode.Y, KeyCode.Z,
    };

    private int score = 0;
    private static readonly int MAX_SCORE = 20;
    private static readonly int PER_ROW_SCORE = 5;

    private Row[] rows;
    private int rowIndex;
    private int columnIndex;

    private string[] validWords;
    private string solution;
    private string hint;

    private static readonly Color GREEN = new Color32(83, 141, 78, 255);
    private static readonly Color YELLOW = new Color32(181, 159, 59, 255);
    private static readonly Color DARK_GREY = new Color32(58, 58, 60, 255);
    private static readonly Color LIGHT_GREY = new Color32(86, 87, 88, 255);
    private static readonly Color BACKGROUND = new Color32(18, 18, 19, 255);

    [Header("Styles")]
    public TileStyle EmptyStyle = new(BACKGROUND, DARK_GREY);
    public TileStyle OccupiedStyle = new(BACKGROUND, LIGHT_GREY);
    public TileStyle CorrectStyle = new(GREEN, GREEN);
    public TileStyle WrongSpotStyle = new(YELLOW, YELLOW);
    public TileStyle IncorrectStyle = new(DARK_GREY, DARK_GREY);

    [Header("UI")]
    public GameObject ReturnButton;
    public GameObject FeedbackBox;
    public TextMeshProUGUI HintText;

    private TextMeshProUGUI feedbackBoxText;

    private void Awake()
    {
        rows = GetComponentsInChildren<Row>();
        feedbackBoxText = FeedbackBox.GetComponentInChildren<TextMeshProUGUI>();
    }

    private new void Start()
    {
        base.Start();

        TextAsset textFile = Resources.Load("valid_words") as TextAsset;
        validWords = textFile.text.Split("\n", System.StringSplitOptions.None);

        // Get a random word and hint
        textFile = Resources.Load("solution_hints") as TextAsset;
        string[] solutionHints = textFile.text.Split("\n", System.StringSplitOptions.None);
        solutionHints = solutionHints[Random.Range(0, solutionHints.Length)].Split(",");
        solution = solutionHints[0].ToLower().Trim();
        hint = solutionHints[1];

        HintText.text = "HINT: " + hint;
        HintText.gameObject.SetActive(true);

        // Start game
        ClearState();
        enabled = true;
    }

    private void Update()
    {
        Row currentRow = rows[rowIndex];

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            columnIndex = Mathf.Max(columnIndex - 1, 0);
            currentRow.Tiles[columnIndex].ClearLetter();
            currentRow.Tiles[columnIndex].Style = EmptyStyle;
            ToggleFeedback();
        }
        else if (columnIndex >= currentRow.Tiles.Length)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SubmitRow(currentRow);
            }
        }
        else
        {
            for (int i = 0; i < KEYS.Length; i++)
            {
                if (Input.GetKeyDown(KEYS[i]))
                {
                    currentRow.Tiles[columnIndex].Letter = (char)KEYS[i];
                    currentRow.Tiles[columnIndex].Style = OccupiedStyle;
                    columnIndex++;
                    break;
                }
            }
        }
    }

    public void ReturnButtonMethod()
    {
        ClearState();
        gameManager.MinigameEnded(minigameWon, score);
    }

    private void SubmitRow(Row row)
    {
        if (!IsValidWord(row.GetWord()))
        {
            ToggleFeedback("INVALID WORD!");
            return;
        }

        string remaining = solution;

        // Check correct/incorrect letters first
        for (int i = 0; i < row.Tiles.Length; i++)
        {
            Tile tile = row.Tiles[i];

            if (tile.Letter == solution[i])
            {
                tile.Style = CorrectStyle;

                remaining = remaining.Remove(i, 1);
                remaining = remaining.Insert(i, " ");
            }
            else if (!solution.Contains(tile.Letter))
            {
                tile.Style = IncorrectStyle;
            }
        }

        // Check wrong spots after
        for (int i = 0; i < row.Tiles.Length; i++)
        {
            Tile tile = row.Tiles[i];

            if (tile.Style != CorrectStyle && tile.Style != IncorrectStyle)
            {
                if (remaining.Contains(tile.Letter))
                {
                    tile.Style = WrongSpotStyle;

                    int index = remaining.IndexOf(tile.Letter);
                    remaining = remaining.Remove(index, 1);
                    remaining = remaining.Insert(index, " ");
                }
                else
                {
                    tile.Style = IncorrectStyle;
                }
            }
        }

        if (HasWon(row))
        {
            ToggleFeedback("YOU WON!");
            minigameWon = true;
            score = MAX_SCORE - (rows.Length - rowIndex) * PER_ROW_SCORE;
            enabled = false;
            return;
        }

        rowIndex++;
        columnIndex = 0;

        if (rowIndex >= rows.Length)
        {
            ToggleFeedback("YOU LOST!");
            score = MAX_SCORE;
            minigameWon = false;
            enabled = false;
        }
    }

    private void ToggleFeedback(string feedback = "")
    {
        if (feedback == "")
        {
            HintText.gameObject.SetActive(true);
            FeedbackBox.SetActive(false);
        }
        else
        {
            feedbackBoxText.text = feedback;
            HintText.gameObject.SetActive(false);
            FeedbackBox.SetActive(true);
        }
    }

    private bool IsValidWord(string word)
    {
        for (int i = 0; i < validWords.Length; i++)
            if (string.Equals(word, validWords[i], System.StringComparison.OrdinalIgnoreCase))
                return true;

        return false;
    }

    private bool HasWon(Row row)
    {
        for (int tileIndex = 0; tileIndex < row.Tiles.Length; tileIndex++)
            if (row.Tiles[tileIndex].Style != CorrectStyle)
                return false;

        return true;
    }

    private void ClearState()
    {
        for (int row = 0; row < rows.Length; row++)
        {
            for (int col = 0; col < rows[row].Tiles.Length; col++)
            {
                rows[row].Tiles[col].ClearLetter();
                rows[row].Tiles[col].Style = EmptyStyle;
            }
        }

        rowIndex = 0;
        columnIndex = 0;

        ReturnButton.SetActive(false);
        ToggleFeedback();
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