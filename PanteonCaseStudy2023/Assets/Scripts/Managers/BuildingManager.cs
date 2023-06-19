using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [Header("Building Manager Parameters")]
    [Header("Building References")]
    [Tooltip("The barrack object that will create")]
    [SerializeField]
    private Building barrackPrefab;

    [Tooltip("The power plant object that will create")]
    [SerializeField]
    private Building powerPlantPrefab;

    private Building selectedBuilding;

    private Action buildAction;

    private void Update()
    {
        buildAction?.Invoke();
    }

    public void BuildProces()
    {
        DynamicCursor.singleton.Display(selectedBuilding);

        Tile nearestTile =
            TileManager.singleton.GetNearestTile(DynamicCursor.singleton.transform.position);

        if (IsBuildingCanBePlaced(nearestTile, selectedBuilding))
        {
            DynamicCursor.singleton.CanPlaceable();

            if (Input.GetMouseButtonDown(0))
            {
                Building currentBuilding = Factory.singleton.CreateEntity(selectedBuilding.GetEntityType(),
                    nearestTile.transform.position, Quaternion.identity,
                        nearestTile.transform).GetComponent<Building>();

                currentBuilding.SetTilesInEntity(GetTilesInBuilding(nearestTile, currentBuilding));

                foreach (var tile in GetTilesInBuilding(nearestTile, currentBuilding))
                {
                    tile.SetEntity(currentBuilding);
                }

                selectedBuilding = null;
                currentBuilding = null;

                DynamicCursor.singleton.Hide();

                buildAction -= BuildProces;
            }
        }
        else
        {
            DynamicCursor.singleton.CanNotPlaceable();

            if (Input.GetMouseButtonDown(0))
            {
                selectedBuilding = null;

                DynamicCursor.singleton.Hide();

                buildAction -= BuildProces;
            }
        }
    }

    public void SelectBarrack()
    {
        selectedBuilding = barrackPrefab;
        buildAction += BuildProces;
    }

    public void SelectPowerPlant()
    {
        selectedBuilding = powerPlantPrefab;
        buildAction += BuildProces;
    }

    private bool IsBuildingCanBePlaced(Tile tile, Building building)
    {
        List<Tile> tilesInBuilding = GetTilesInBuilding(tile, building);

        if (tilesInBuilding == null)
        {
            return false;
        }

        for (int i = 0; i < tilesInBuilding.Count; i++)
        {
            Tile t = tilesInBuilding[i];
            if (t.IsOccupied())
            {
                return false;
            }
        }
        return true;
    }

    private List<Tile> GetTilesInBuilding(Tile tile, Building building)
    {
        int buildingWidth = building.GetBuildingScale().x;
        int buildingHeight = building.GetBuildingScale().y;

        int x = tile.GetTileGridPosition().x;
        int y = tile.GetTileGridPosition().y;

        int tileGridWidth = TileManager.singleton.GetTileGrid().GetLength(0);
        int tileGridHeight = TileManager.singleton.GetTileGrid().GetLength(1);

        List<Tile> tilesInBuilding = new List<Tile>();

        if (x + buildingWidth <= tileGridWidth && y + buildingHeight <= tileGridHeight)
        {
            for (int i = x; i < x + buildingWidth; i++)
            {
                for (int j = y; j < y + buildingHeight; j++)
                {
                    tilesInBuilding.Add(TileManager.singleton.GetTileGrid()[i, j]);
                }
            }

            return tilesInBuilding;
        }
        return null;
    }
}
