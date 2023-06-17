using UnityEngine;

public class Tile : MonoBehaviour
{
    [Header("Tile Parameters")]
    [Header("Grid Position Parameters")]
    [Tooltip("Tile horizontal position on grid")]
    [SerializeField]
    private int x;
    [Tooltip("Tile vertical position on grid")]
    [SerializeField]
    private int y;

    private bool isOccupied;
    private Building buildingOnTile;
    private Soldier soldierOnTile;
    /// <summary>
    /// Returns the grid position of the tile
    /// </summary>
    /// <returns>The grid position as a Vector2</returns>
    public Vector2Int GetTileGridPosition()
    {
        Vector2Int tileGridPosition = new Vector2Int(x, y);
        return tileGridPosition;
    }

    /// <summary>
    /// Returns the grid position of the tile
    /// </summary>
    /// <returns>The grid position as a Vector2</returns>
    public void SetTileGridPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    private void Occupy()
    {
        //Debug.Log(name + " " + x + "," + y + " is occupied!");
        isOccupied = true;
    }

    public void UnOccupy()
    {
        //Debug.Log(name + " " + x + "," + y + " is UnOccupied!");
        isOccupied = false;
    }

    public bool IsOccupied()
    {
        return isOccupied;
    }

    public Building GetBuilding()
    {
        return buildingOnTile;
    }
    public Soldier GetSoldier()
    {
        return soldierOnTile;
    }

    public void SetBuilding(Building building)
    {
        buildingOnTile = building;
        Occupy();
    }
    public void SetSoldier(Soldier soldier)
    {
        soldierOnTile = soldier;
        Occupy();
    }
}
