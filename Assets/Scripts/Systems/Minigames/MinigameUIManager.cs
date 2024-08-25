using TMPro;
using UnityEngine;

public class MinigameUIManager : MonoBehaviour
{
    [SerializeField] GameObject background;

    [Header("Win Elements")]
    [SerializeField] GameObject winPanel;
    [SerializeField] TMP_Text winMessageText;

    [Header("Lose Elements")]
    [SerializeField] GameObject losePanel;
    [SerializeField] TMP_Text lostMessageText;

    public static MinigameUIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void SetWinMessageText(string winMessage)
    {
        winMessageText.text = winMessage;
    }

    public void SetLoseMessageText(string loseMessage)
    {
        lostMessageText.text = loseMessage;
    }

    public void SetWinPanelState(bool isActive)
    {
        background.SetActive(isActive);
        winPanel.SetActive(isActive);
    }

    public void SetLosePanelState(bool isActive)
    {
        background.SetActive(isActive);
        losePanel.SetActive(isActive);
    }

    public void ShowWinScreen(string winMessage)
    {
        winMessageText.text = winMessage;
        background.SetActive(true);
        winPanel.SetActive(true);
    }

    public void ShowLoseScreen(string loseMessage)
    {
        lostMessageText.text = loseMessage;
        background.SetActive(true);
        losePanel.SetActive(true);
    }

    public void HideAllPannels()
    {
        background.SetActive(false);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
    }
}
