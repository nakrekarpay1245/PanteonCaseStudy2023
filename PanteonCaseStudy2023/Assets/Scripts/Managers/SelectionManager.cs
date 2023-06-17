using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoSingleton<SelectionManager>
{
    private Vector2 searchingPosition;
    private Tile nearestTile;

    public Tile GetNearestTile(Vector2 position)
    {
        searchingPosition = position;

        float nearestDistance = float.MaxValue;

        for (int i = 0; i < TileManager.singleton.GetActiveTileList().Count; i++)
        {
            Tile tile = TileManager.singleton.GetActiveTileList()[i];

            float distance = Vector2.Distance(tile.transform.position, searchingPosition);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestTile = tile;
            }
        }

        return nearestTile;
    }

    public Tile GetNearestUnOccupiedTile(Vector2 position)
    {
        searchingPosition = position;

        float nearestDistance = float.MaxValue;

        for (int i = 0; i < TileManager.singleton.GetActiveTileList().Count; i++)
        {
            Tile tile = TileManager.singleton.GetActiveTileList()[i];

            if (tile.IsOccupied())
            {
                continue;
            }

            float distance = Vector2.Distance(tile.transform.position, searchingPosition);

            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                nearestTile = tile;
            }
        }

        return nearestTile;
    }
}
