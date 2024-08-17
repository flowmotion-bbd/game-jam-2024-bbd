using UnityEngine;

public class Row : MonoBehaviour
{
    private Tile[] tiles;

    public Tile[] Tiles { get => tiles; }

    private void Awake()
    {
        tiles = GetComponentsInChildren<Tile>();
    }

    public string GetWord()
    {
        string word = "";
        foreach(Tile tile in tiles)
        {
            word += tile.Letter;
        }
        return word;
    }
}