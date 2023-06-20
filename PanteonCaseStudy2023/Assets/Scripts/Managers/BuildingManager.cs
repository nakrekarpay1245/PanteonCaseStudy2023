using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    /// <summary>
    /// The currently selected building for actions such as producing soldiers
    /// </summary>
    private Building selectedBuilding;

    /// <summary>
    /// The action in which the functions related to the building are executed
    /// </summary>
    private Action buildAction;

    private void Update()
    {
        buildAction?.Invoke();
    }

    /// <summary>
    /// responsible for handling the building process. It first displays the selected building using the 
    /// DynamicCursor singleton. Then, it retrieves the nearest tile to the cursor's position from the 
    /// TileManager. Next, it checks if the building can be placed on the nearest tile by calling the
    /// CanBuildingBePlaced function.If it can be placed, the DynamicCursor is set to be placeable, and
    /// if the left mouse button is pressed, the CreateBuilding function is called to create the building 
    /// on the nearest tile. If the building cannot be placed, the DynamicCursor is set to be 
    /// non-placeable.If the left mouse button is pressed, the selected building is reset, the 
    /// DynamicCursor is hidden, and the BuildProcess method is removed from the buildAction delegate. 
    /// Overall, this function handles the process of displaying the selected building, checking for 
    /// placement feasibility, and performing the necessary actions based on the user's input.
    /// </summary>
    public void BuildProcess()
    {
        DynamicCursor.singleton.Display(selectedBuilding);
        Tile nearestTile = TileManager.singleton.GetNearestTile(DynamicCursor.singleton.transform.position);

        if (CanBuildingBePlaced(nearestTile, selectedBuilding))
        {
            DynamicCursor.singleton.SetPlaceable(true);

            if (Input.GetMouseButtonDown(0))
            {
                CreateBuilding(nearestTile);
            }
        }
        else
        {
            DynamicCursor.singleton.SetPlaceable(false);

            if (Input.GetMouseButtonDown(0))
            {
                ResetSelectedBuilding();

                DynamicCursor.singleton.Hide();

                buildAction -= BuildProcess;
            }
        }
    }

    /// <summary>
    /// responsible for creating a building on the nearest tile. It creates a new building instance 
    /// based on the selected building type using the Factory class. The tiles within the building's 
    /// area are obtained using the GetTilesInBuilding function. Then, the function iterates through 
    /// the tiles and sets the building entity on each tile, marking them as occupied.
    ///After that, the building's tiles are set using the SetTilesInEntity method. The selected 
    ///building is reset, the dynamic cursor is hidden, and the BuildProcess method is removed from 
    ///the buildAction delegate. Overall, this function handles the creation of a building entity on 
    ///the nearest tile and performs the necessary setup and clean-up operations
    /// </summary>
    /// <param name="nearestTile"></param>
    private void CreateBuilding(Tile nearestTile)
    {
        Building currentBuilding = Factory.singleton.CreateEntity(selectedBuilding.GetEntityType(),
            nearestTile.transform.position, Quaternion.identity, nearestTile.transform).GetComponent<Building>();

        List<Tile> tilesInBuilding = GetTilesInBuilding(nearestTile, currentBuilding);

        for (int i = 0; i < tilesInBuilding.Count; i++)
        {
            Tile tile = tilesInBuilding[i];
            tile.SetEntity(currentBuilding);
            tile.Occupy();
        }

        currentBuilding.SetTilesInEntity(tilesInBuilding);

        ResetSelectedBuilding();

        DynamicCursor.singleton.Hide();

        buildAction -= BuildProcess;
    }

    /// <summary>
    /// resets the currently selected building by setting it to null
    /// </summary>
    private void ResetSelectedBuilding()
    {
        selectedBuilding = null;
    }

    /// <summary>
    /// It sets the provided building as the currently selected building by assigning it 
    /// to the selectedBuilding variable. Additionally, it adds the BuildProcess method 
    /// to the buildAction delegate, which will be executed when the build action is triggered
    /// </summary>
    /// <param name="building"></param>
    public void SelectBuilding(Building building)
    {
        selectedBuilding = building;
        buildAction += BuildProcess;
    }

    /// <summary>
    /// This function determines whether a building can be placed on a specific tile. It first
    /// retrieves the tiles within the building using the "GetTilesInBuilding" function. If the 
    /// building does not fit within the tile grid, it immediately returns false. Then, it iterates
    /// through the tiles in the building and checks if any of them are already occupied. If an occupied
    /// tile is found, it returns false. If all tiles are unoccupied, it returns true, indicating that 
    /// the building can be placed on the given tile.
    /// </summary>
    /// <param name="tile"></param>
    /// <param name="building"></param>
    /// <returns></returns>
    private bool CanBuildingBePlaced(Tile tile, Building building)
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

    /// <summary>
    /// This function retrieves the tiles occupied by a building, given a starting tile and the 
    /// dimensions of the building. It checks if the building fits within the bounds of the
    /// tile grid and then iterates through the corresponding tiles, adding them to a list.
    /// The list of tiles within the building is returned if it fits within the grid; otherwise, 
    /// null is returned
    /// </summary>
    /// <param name="tile"></param>
    /// <param name="building"></param>
    /// <returns></returns>
    private List<Tile> GetTilesInBuilding(Tile tile, Building building)
    {
        int buildingWidth = building.GetBuildingScale().x;
        int buildingHeight = building.GetBuildingScale().y;

        int x = tile.GetTileGridPosition().x;
        int y = tile.GetTileGridPosition().y;

        Tile[,] tileGrid = TileManager.singleton.GetTileGrid();
        int tileGridWidth = tileGrid.GetLength(0);
        int tileGridHeight = tileGrid.GetLength(1);

        List<Tile> tilesInBuilding = new List<Tile>();

        if (x + buildingWidth <= tileGridWidth && y + buildingHeight <= tileGridHeight)
        {
            for (int i = x; i < x + buildingWidth; i++)
            {
                for (int j = y; j < y + buildingHeight; j++)
                {
                    tilesInBuilding.Add(tileGrid[i, j]);
                }
            }

            return tilesInBuilding;
        }

        return null;
    }
}
