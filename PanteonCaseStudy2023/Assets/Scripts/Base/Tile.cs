using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private int x;
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
    /// Gets the grid position of the tile.
    /// </summary>
    /// <returns>The grid position of the tile.</returns>
    public Vector2Int GetTileGridPosition()
    {
        return new Vector2Int(x, y);
    }

    /// <summary>
    /// Sets the grid position of the tile.
    /// </summary>
    /// <param name="x">The x-coordinate of the grid position.</param>
    /// <param name="y">The y-coordinate of the grid position.</param>
    public void SetTileGridPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    /// <summary>
    /// Calculates the F cost of the tile.
    /// </summary>
    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }

    /// <summary>
    /// Occupies the tile.
    /// </summary>
    public void Occupy()
    {
        isOccupied = true;
        SetColor(Color.red);
    }

    /// <summary>
    /// Unoccupies the tile.
    /// </summary>
    public void UnOccupy()
    {
        isOccupied = false;
        SetEntity(null);
        SetColor(Color.white);
    }

    /// <summary>
    /// Checks if the tile is occupied.
    /// </summary>
    /// <returns>True if the tile is occupied, false otherwise.</returns>
    public bool IsOccupied()
    {
        return isOccupied;
    }

    /// <summary>
    /// Gets the entity on the tile.
    /// </summary>
    /// <returns>The entity on the tile.</returns>
    public Entity GetEntity()
    {
        return entityOnTile;
    }

    /// <summary>
    /// Sets the entity on the tile.
    /// </summary>
    /// <param name="entity">The entity to set on the tile.</param>
    public void SetEntity(Entity entity)
    {
        entityOnTile = entity;
    }

    /// <summary>
    /// Sets the color of the tile.
    /// </summary>
    /// <param name="color">The color to set.</param>
    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }
}
