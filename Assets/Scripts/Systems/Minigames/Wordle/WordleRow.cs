using UnityEngine;

public class WordleRow : MonoBehaviour
{
    private WordleTile[] tiles;

    public WordleTile[] Tiles { get => tiles; }

    private void Awake()
    {
        tiles = GetComponentsInChildren<WordleTile>();
    }

    public string GetWord()
    {
        string word = "";
        foreach(WordleTile tile in tiles)
        {
            word += tile.Letter;
        }
        return word;
    }
}