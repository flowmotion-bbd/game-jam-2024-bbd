using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelLeaderBoardEntry : MonoBehaviour
{
    [SerializeField] TMP_Text rankText = null;
    [SerializeField] TMP_Text usernameText = null;
    [SerializeField] TMP_Text timeText = null;

    public void SetEntryInformation(int rank, string username, string time)
    {
        rankText.text = rank.ToString();
        usernameText.text = username;
        timeText.text = time;
    }
}
