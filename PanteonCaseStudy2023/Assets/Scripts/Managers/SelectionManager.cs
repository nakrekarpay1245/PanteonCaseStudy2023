using UnityEngine;

public class SelectionManager : MonoSingleton<SelectionManager>
{
    [Header("Selection Manager Parameters")]
    [Tooltip("Entity that the mouse has clicked")]
    public IEntity selectedEntity;

    [Header("Mouse Position Clamps")]
    [Tooltip("The maximum horizontal point where the mouse should be present for selection")]
    [SerializeField]
    private int maximumHorizontalMouseSelectionPosition = 5;
    [Tooltip("The maximum vertical point where the mouse should be present for selection")]
    [SerializeField]
    private int maximumVerticalMouseSelectionPosition = 5;

    protected override void OnEnable()
    {
        base.OnEnable();
        InputManager.singleton.OnLeftMouseClick += SelectEntity;
    }

    /// <summary>
    /// Selects an entity based on the current mouse position within the selection bounds.
    /// </summary>
    private void SelectEntity()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (IsWithinSelectionBounds(mousePosition))
        {
            Tile nearestTile = TileManager.singleton.GetNearestTile(mousePosition);

            if (!IsTileOccupied(nearestTile))
            {
                ResetSelection();
                return;
            }

            selectedEntity = nearestTile.GetEntity();

            if (selectedEntity != null)
            {
                selectedEntity.Select();
            }
            else
            {
                ResetSelection();
                return;
            }
        }
    }


    /// <summary>
    /// Checks if the given position is within the selection bounds.
    /// </summary>
    /// <param name="position">The position to check.</param>
    /// <returns>True if the position is within the selection bounds, false otherwise.</returns>
    private bool IsWithinSelectionBounds(Vector2 position)
    {
        return position.x < maximumHorizontalMouseSelectionPosition &&
               position.x > -maximumHorizontalMouseSelectionPosition &&
               position.y < maximumVerticalMouseSelectionPosition &&
               position.y > -maximumVerticalMouseSelectionPosition;
    }

    /// <summary>
    /// Checks if the given tile is occupied.
    /// </summary>
    /// <param name="tile">The tile to check.</param>
    /// <returns>True if the tile is occupied, false otherwise.</returns>
    private bool IsTileOccupied(Tile tile)
    {
        return tile != null && tile.IsOccupied();
    }

    /// <summary>
    /// Resets the current selection by hiding soldier buttons, displaying building buttons,
    /// hiding the information menu, and setting the selected entity to null.
    /// </summary>
    private void ResetSelection()
    {
        UIManager.singleton.HideSoldierButtons();
        UIManager.singleton.DisplayBuildingButtons();
        UIManager.singleton.HideInformationMenu();
        selectedEntity = null;
    }


    private void OnDisable()
    {
        InputManager.singleton.OnLeftMouseClick -= SelectEntity;
    }
}
