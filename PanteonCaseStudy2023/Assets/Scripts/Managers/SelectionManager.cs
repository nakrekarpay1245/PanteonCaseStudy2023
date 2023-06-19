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

    private void Update()
    {
        SelectEntity();
    }

    public void SelectEntity()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (mousePosition.x < maximumHorizontalMouseSelectionPosition &&
                mousePosition.x > -maximumHorizontalMouseSelectionPosition &&
                mousePosition.y < maximumVerticalMouseSelectionPosition &&
                mousePosition.y > -maximumVerticalMouseSelectionPosition)
            {
                Tile nearestTile = TileManager.singleton.GetNearestTile(mousePosition);

                if (!nearestTile.IsOccupied() || !nearestTile)
                {
                    //Debug.Log("Nearest Tile is not occupied!");
                    UIManager.singleton.HideSoldierButtons();
                    UIManager.singleton.DisplayBuildingButtons();
                    UIManager.singleton.HideInformationMenu();
                    selectedEntity = null;
                    return;
                }

                selectedEntity = nearestTile.GetEntity();

                if (selectedEntity != null)
                {
                    selectedEntity.Select();
                }
                else
                {
                    //Debug.Log("There is no selected Entity!");
                    UIManager.singleton.HideSoldierButtons();
                    UIManager.singleton.DisplayBuildingButtons();
                    UIManager.singleton.HideInformationMenu();
                    selectedEntity = null;
                    return;
                }
            }
        }
    }
}
