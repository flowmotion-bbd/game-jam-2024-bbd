using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

public class LeaderboardResult
{
    public string playerId { get; set; }
    public string playerName { get; set; }
    public int rank { get; set; }
    public double score { get; set; }
    public string timeScore { get; set; }
    public bool isTopPlayer => rank == 0;
}

public class LeaderboardResponse
{
    public int limit { get; set; }
    public int total { get; set; }
    public List<LeaderboardResult> results { get; set; }
}

public class PlayerScore
{
    public string playerId { get; set; }
    public string playerName { get; set; }
    public int rank { get; set; }
    public double score { get; set; }
    public string updatedTime { get; set; }
    public string timeScore { get; set; }
    public bool isTopPlayer => rank == 0;
}


public class LeaderBoardManager : MonoBehaviour
{
    const string LEADERBOARD_ID_PREFIX = "dfl";
    const int NAME_SUFFIX_LENGTH = 5;

    public static LeaderBoardManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private string formatLeaderBoardId(int level)
    {
        var leaderboardId = LEADERBOARD_ID_PREFIX + level.ToString();
        return leaderboardId;
    }
    private string ConvertMillisecondsToTimeFormat(int milliseconds)
    {
        TimeSpan time = TimeSpan.FromMilliseconds(milliseconds);

        string formattedTime = string.Format("{0:D2}:{1:D2}:{2:D3}",
            time.Minutes,
            time.Seconds,
            time.Milliseconds);

        return formattedTime;
    }

    public async Task<LeaderboardResponse> GetScoresForLevelAsync(int level, int limit=10, int offset=0)
    {
        try
        {
            var jsonResponse = await LeaderboardsService.Instance.GetScoresAsync(formatLeaderBoardId(level), new GetScoresOptions { Limit=limit, Offset=offset});

            var leaderboardData = JsonConvert.DeserializeObject<LeaderboardResponse>(JsonConvert.SerializeObject(jsonResponse));

            foreach (var result in leaderboardData.results)
            {
                var nameLen = result.playerName.Length;
                if (nameLen > NAME_SUFFIX_LENGTH)
                {
                    result.playerName = result.playerName[..(nameLen - NAME_SUFFIX_LENGTH)];
                }
                result.timeScore = ConvertMillisecondsToTimeFormat((int)result.score);
            }

            return leaderboardData;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to retrieve level {level} scores: {ex.Message}");
            return null;
        }
    }

    public async Task<PlayerScore> GetPlayerScoreForLevelAsync(int level)
    {
        try
        {
            var jsonResponse = await LeaderboardsService.Instance.GetPlayerScoreAsync(formatLeaderBoardId(level));

        var playerData = JsonConvert.DeserializeObject<PlayerScore>(JsonConvert.SerializeObject(jsonResponse));

        var nameLen = playerData.playerName.Length;
        if (nameLen > NAME_SUFFIX_LENGTH)
        {
            playerData.playerName = playerData.playerName[..(nameLen - NAME_SUFFIX_LENGTH)];
        }
        playerData.timeScore = ConvertMillisecondsToTimeFormat((int)playerData.score);


        return playerData;
    }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to retrieve level {level} scores: {ex.Message}");
            return null;
        }
    }

    public async Task<PlayerScore> AddPlayerScoreForLevelAsync(int level, int levelTimeMs)
    {
        try
        {
            var response = await LeaderboardsService.Instance.AddPlayerScoreAsync(formatLeaderBoardId(level), levelTimeMs);

            var playerData = JsonConvert.DeserializeObject<PlayerScore>(JsonConvert.SerializeObject(response));

            var nameLen = playerData.playerName.Length;
            if (nameLen > NAME_SUFFIX_LENGTH)
            {
                playerData.playerName = playerData.playerName[..(nameLen - NAME_SUFFIX_LENGTH)];
            }
            playerData.timeScore = ConvertMillisecondsToTimeFormat((int)playerData.score);


            return playerData;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to retrieve level {level} scores: {ex.Message}");
            return null;
        }
    }
}

