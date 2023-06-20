using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class Pathfinding : MonoSingleton<Pathfinding>
{
    private const int StraightMoveCost = 10;
    private const int DiagonalMoveCost = 14;

    /// <summary>
    /// This function implements the A* algorithm to find a path between two tiles and
    /// returns the path as a list of tiles.
    /// </summary>
    /// <param name="startTile"></param>
    /// <param name="endTile"></param>
    /// <returns></returns>
    public List<Tile> FindPath(Tile startTile, Tile endTile)
    {
        List<Tile> openList = new List<Tile>() { startTile };
        List<Tile> closedList = new List<Tile>();

        int gridWidth = TileManager.singleton.GetTileGrid().GetLength(0);
        int gridHeight = TileManager.singleton.GetTileGrid().GetLength(1);

        Tile[,] tileGrid = TileManager.singleton.GetTileGrid();

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Tile tile = tileGrid[x, y];

                tile.gCost = int.MaxValue;

                tile.CalculateFCost();

                tile.cameFromTile = null;
            }
        }

        startTile.gCost = 0;

        startTile.hCost = CalculateDistanceCost(startTile, endTile);

        startTile.CalculateFCost();

        while (openList.Count > 0)
        {
            Tile currentTile = GetLowestFCostTile(openList);

            if (currentTile == endTile)
            {
                return CalculatePath(endTile);
            }

            openList.Remove(currentTile);
            closedList.Add(currentTile);

            List<Tile> neighborTileList = GetNeighbourTileList(currentTile);
            int neighborCount = neighborTileList.Count;

            for (int i = 0; i < neighborCount; i++)
            {
                Tile neighborTile = neighborTileList[i];

                if (closedList.Contains(neighborTile))
                {
                    continue;
                }

                int tentativeGCost = currentTile.gCost + CalculateDistanceCost(currentTile, neighborTile);
                if (tentativeGCost < neighborTile.gCost)
                {
                    neighborTile.cameFromTile = currentTile;
                    neighborTile.gCost = tentativeGCost;
                    neighborTile.hCost = CalculateDistanceCost(neighborTile, endTile);
                    neighborTile.CalculateFCost();

                    if (!openList.Contains(neighborTile))
                    {
                        openList.Add(neighborTile);
                    }
                }
            }
        }

        return null;
    }

    /// <summary>
    /// This function calculates and returns the path by traversing the cameFromTile references
    /// from the end tile to the start tile in reverse order.
    /// </summary>
    /// <param name="endTile"></param>
    /// <returns></returns>
    private List<Tile> CalculatePath(Tile endTile)
    {
        List<Tile> path = new List<Tile>();
        path.Add(endTile);
        Tile currentTile = endTile;

        while (currentTile.cameFromTile != null)
        {
            path.Add(currentTile.cameFromTile);
            currentTile = currentTile.cameFromTile;
        }

        path.Reverse();
        return path;
    }

    /// <summary>
    /// This function returns a list of neighboring tiles around a given tile that are within 
    /// the valid grid bounds, exist in the tile grid, and are unoccupied.
    /// </summary>
    /// <param name="tile"></param>
    /// <returns></returns>
    public List<Tile> GetNeighbourTileList(Tile tile)
    {
        int x = tile.GetTileGridPosition().x;
        int y = tile.GetTileGridPosition().y;

        List<Tile> neighborTileList = new List<Tile>();

        for (int xOffset = -1; xOffset <= 1; xOffset++)
        {
            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                if (xOffset == 0 && yOffset == 0)
                {
                    continue;
                }
                int neighborX = x + xOffset;
                int neighborY = y + yOffset;

                if (neighborX >= 0 && neighborX < TileManager.singleton.GetTileGrid().GetLength(0) &&
                    neighborY >= 0 && neighborY < TileManager.singleton.GetTileGrid().GetLength(1) &&
                    TileManager.singleton.GetTileGrid()[neighborX, neighborY] != null &&
                    !TileManager.singleton.GetTileGrid()[neighborX, neighborY].IsOccupied())
                {
                    neighborTileList.Add(TileManager.singleton.GetTileGrid()[neighborX, neighborY]);
                }
            }
        }

        return neighborTileList;
    }

    /// <summary>
    /// This function returns the tile with the lowest F-cost from a given list of tiles.
    /// </summary>
    /// <param name="tileList"></param>
    /// <returns></returns>
    private Tile GetLowestFCostTile(List<Tile> tileList)
    {
        Tile lowestFCostTile = tileList[0];

        for (int i = 1; i < tileList.Count; i++)
        {
            if (tileList[i].fCost <= lowestFCostTile.fCost)
            {
                lowestFCostTile = tileList[i];
            }
        }

        return lowestFCostTile;
    }

    /// <summary>
    ///  This function calculates the distance cost between two tiles based on their grid positions,
    ///  taking into account diagonal and straight movements.
    /// </summary>
    /// <param name="tileA"></param>
    /// <param name="tileB"></param>
    /// <returns></returns>
    public int CalculateDistanceCost(Tile tileA, Tile tileB)
    {
        int aX = tileA.GetTileGridPosition().x;
        int aY = tileA.GetTileGridPosition().y;

        int bX = tileB.GetTileGridPosition().x;
        int bY = tileB.GetTileGridPosition().y;

        int distanceX = Mathf.Abs(aX - bX);
        int distanceY = Mathf.Abs(aY - bY);

        int remaining = Mathf.Abs(distanceX - distanceY);

        return (Mathf.Min(distanceX, distanceY) * DiagonalMoveCost) + (remaining * StraightMoveCost);
    }
}
