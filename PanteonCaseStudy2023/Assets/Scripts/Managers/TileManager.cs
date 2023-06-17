using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoSingleton<TileManager>
{
    private const float cellEqualizer = 100;
    private const float cellSize = 32;

    [Header("Tile Manager Parameters")]
    [Header("References")]
    [Tooltip("The tile object that will create the grid")]
    [SerializeField]
    private Tile tilePrefab;

    [Header("Grid Parameters")]
    [Header("Grid Scale")]
    [Tooltip("Grid horizontal scale")]
    [SerializeField]
    private int gridWidth;
    [Tooltip("Grid vertical scale")]
    [SerializeField]
    private int gridHeight;

    //The grid will hold the tiles, and game systems such as placement and movement
    //will be processed through this grid
    private Tile[,] tileGrid;
    [HideInInspector]
    public List<Tile> activeTileList;

    private void Awake()
    {
        tileGrid = new Tile[gridWidth, gridHeight];
        GenerateTiles();
    }

    private void GenerateTiles()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                float cellMultiplier = cellSize / cellEqualizer;
                float positionX = x - gridWidth / 2;
                float positionY = y - gridHeight / 2;

                Vector2 tilePosition = new Vector2(positionX, positionY) * cellMultiplier;

                Tile generatedTile = Instantiate(tilePrefab, tilePosition, Quaternion.identity, transform);

                generatedTile.SetTileGridPosition(x, y);

                tileGrid[x, y] = generatedTile;
                activeTileList.Add(generatedTile);
            }
        }
    }

    public List<Tile> GetActiveTileList()
    {
        return activeTileList;
    }

    public Tile[,] GetTileGrid()
    {
        return tileGrid;
    }
}
