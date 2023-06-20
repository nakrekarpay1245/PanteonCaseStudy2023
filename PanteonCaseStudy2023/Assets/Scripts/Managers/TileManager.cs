using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoSingleton<TileManager>
{
    private const float CellEqualizer = 100f;
    private const float CellSize = 32f;

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

    public Tile[,] tileGrid;

    [HideInInspector]
    public List<Tile> activeTileList;

    private void Awake()
    {
        tileGrid = new Tile[gridWidth, gridHeight];
        activeTileList = new List<Tile>();
        GenerateTiles();
    }

    /// <summary>
    /// Generates the tiles in the grid based on the specified grid width and height.
    /// Each tile is instantiated and positioned accordingly.
    /// </summary>
    private void GenerateTiles()
    {
        float cellMultiplier = CellSize / CellEqualizer;
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Vector2 gridOffset = new Vector2(gridWidth / 2f, gridHeight / 2f) * -1f;

                Vector2 tilePosition = (new Vector2(x, y) + gridOffset);
                tilePosition *= cellMultiplier;

                Tile generatedTile = Instantiate(tilePrefab, tilePosition, Quaternion.identity, transform);
                generatedTile.SetTileGridPosition(x, y);

                tileGrid[x, y] = generatedTile;
                activeTileList.Add(generatedTile);
            }
        }
    }

    /// <summary>
    /// Retrieves the nearest tile from the specified position.
    /// </summary>
    /// <param name="position">The position to find the nearest tile from.</param>
    /// <returns>The nearest tile to the specified position.</returns>
    public Tile GetNearestTile(Vector2 position)
    {
        float nearestDistance = float.MaxValue;
        Tile nearestTile = null;

        for (int i = 0; i < activeTileList.Count; i++)
        {
            Tile tile = activeTileList[i];
            float distance = Vector2.Distance(tile.transform.position, position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestTile = tile;
            }
        }

        return nearestTile;
    }

    /// <summary>
    /// Retrieves the nearest unoccupied tile from the specified position.
    /// </summary>
    /// <param name="position">The position to find the nearest unoccupied tile from.</param>
    /// <returns>The nearest unoccupied tile to the specified position.</returns>
    public Tile GetNearestUnOccupiedTile(Vector2 position)
    {
        float nearestDistance = float.MaxValue;
        Tile nearestTile = null;

        for (int i = 0; i < activeTileList.Count; i++)
        {
            Tile tile = activeTileList[i];

            if (tile.IsOccupied())
            {
                continue;
            }

            float distance = Vector2.Distance(tile.transform.localPosition, position);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestTile = tile;
            }
        }

        return nearestTile;
    }

    /// <summary>
    /// Returns the list of currently active tiles.
    /// </summary>
    /// <returns>The list of active tiles.</returns>
    public List<Tile> GetActiveTileList()
    {
        return activeTileList;
    }

    /// <summary>
    /// Returns the two-dimensional array representing the tile grid.
    /// </summary>
    /// <returns>The tile grid.</returns>
    public Tile[,] GetTileGrid()
    {
        return tileGrid;
    }
}
