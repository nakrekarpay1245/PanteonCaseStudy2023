using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoSingleton<Pathfinding>
{
    private const int straightMoveCost = 10;
    private const int diagonalMoveCost = 14;

    //public List<Vector2> GetPathVectorList(Tile startTile, Tile endTile)
    //{
    //    List<Tile> path = FindPath(startTile, endTile);

    //    List<Vector2> pathVectors = new List<Vector2>();

    //    if (path != null)
    //    {
    //        for (int i = 0; i < path.Count; i++)
    //        {
    //            Tile tile = path[i];

    //            pathVectors.Add(tile.transform.position);
    //        }

    //        return pathVectors;
    //    }

    //    return null;
    //}

    public List<Tile> FindPath(Tile startTile, Tile endTile)
    {
        List<Tile> openList = new List<Tile>() { startTile };
        List<Tile> closeList = new List<Tile>();

        int gridWidth = TileManager.singleton.GetTileGrid().GetLength(0);
        int gridHeight = TileManager.singleton.GetTileGrid().GetLength(1);

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                Tile tile = TileManager.singleton.GetTileGrid()[x, y];

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
            closeList.Add(currentTile);

            foreach (Tile neighbourTile in GetNeighbourTileList(currentTile))
            {
                if (closeList.Contains(neighbourTile))
                {
                    continue;
                }

                int tentativeGCost = currentTile.gCost + CalculateDistanceCost(currentTile, neighbourTile);
                if (tentativeGCost < neighbourTile.gCost)
                {
                    neighbourTile.cameFromTile = currentTile;
                    neighbourTile.gCost = tentativeGCost;
                    neighbourTile.hCost = CalculateDistanceCost(neighbourTile, endTile);
                    neighbourTile.CalculateFCost();

                    if (!openList.Contains(neighbourTile))
                    {
                        openList.Add(neighbourTile);
                    }
                }
            }
        }

        return null;
    }

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

    public int CalculateDistanceCost(Tile tileA, Tile tileB)
    {
        int aX = tileA.GetTileGridPosition().x;
        int aY = tileA.GetTileGridPosition().y;

        int bX = tileB.GetTileGridPosition().x;
        int bY = tileB.GetTileGridPosition().y;

        int distanceX = Mathf.Abs(aX - bX);
        int distanceY = Mathf.Abs(aY - bY);

        int remaining = Mathf.Abs(distanceX - distanceY);

        return (Mathf.Min(distanceX, distanceY) * diagonalMoveCost) + (remaining * straightMoveCost);
    }
}
