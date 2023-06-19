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
    private Entity entityOnTile;

    public int gCost;
    public int hCost;
    public int fCost;

    public Tile cameFromTile;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

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

    public void CalculateFCost()
    {
        fCost = gCost + fCost;
    }

    private void Occupy()
    {
        //Debug.Log(name + " " + x + "," + y + " is Occupied!");
        isOccupied = true;
    }

    public void UnOccupy()
    {
        //Debug.Log(name + " " + x + "," + y + " is UnOccupied!");
        isOccupied = false;
        SetEntity(null);
    }

    public bool IsOccupied()
    {
        return isOccupied;
    }

    public Entity GetEntity()
    {
        return entityOnTile;
    }

    public void SetEntity(Entity entity)
    {
        entityOnTile = entity;
        Occupy();
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }
}
