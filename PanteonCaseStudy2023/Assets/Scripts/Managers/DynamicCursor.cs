using System;
using UnityEngine;

public class DynamicCursor : MonoSingleton<DynamicCursor>
{
    [Header("Dynamic Cursor Parameters")]
    [Tooltip("The \"Sprite Renderer\" component of the Cursor")]
    [SerializeField]
    private SpriteRenderer spriteRendererComponent;

    /// <summary>
    /// The action that holds all the functions to be executed for the Cursor to display 
    /// the selected building
    /// </summary>
    private Action workingAction;

    /// <summary>
    /// The offset given to the cursor based on the size of the building for more accurate
    /// placement of buildings.
    /// </summary>
    private Vector2 mouseOffset;

    private void Awake()
    {
        spriteRendererComponent = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        Hide();
    }

    private void Update()
    {
        workingAction?.Invoke();
    }

    /// <summary>
    /// This function displays the specified building on the cursor. It sets the sprite of the
    /// cursor to the building's icon, enables the sprite renderer component, sets the mouse 
    /// offset based on the building's scale, and adds the FollowMousePosition function to the 
    /// working action.
    /// </summary>
    /// <param name="building"></param>
    public void Display(Building building)
    {
        SetSprite(building.GetBuildingIcon());
        spriteRendererComponent.enabled = true;
        mouseOffset = building.GetBuildingScale();
        workingAction += FollowMousePosition;
    }

    /// <summary>
    /// This function hides the cursor by disabling the sprite renderer component and removing the
    /// FollowMousePosition function from the working action.
    /// </summary>
    public void Hide()
    {
        spriteRendererComponent.enabled = false;
        workingAction -= FollowMousePosition;
    }

    /// <summary>
    /// This function sets the sprite of the sprite renderer component to the provided sprite.
    /// </summary>
    /// <param name="sprite"></param>
    private void SetSprite(Sprite sprite)
    {
        spriteRendererComponent.sprite = sprite;
    }

    /// <summary>
    /// This function continuously updates the position of the cursor to follow the mouse position
    /// on the screen. It also adjusts the local position of the sprite renderer component based
    /// on the provided mouse offset.
    /// </summary>
    private void FollowMousePosition()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;
        spriteRendererComponent.transform.localPosition = new Vector3(mouseOffset.x / 10, mouseOffset.y / 10, 0);
    }

    /// <summary>
    /// This function sets the color of the cursor based on whether the selected building 
    /// can be placed at the current position. If the building can be placed, the cursor
    /// color is set to green; otherwise, it is set to red.
    /// </summary>
    /// <param name="canPlace"></param>
    public void SetPlaceable(bool canPlace)
    {
        SetColor(canPlace ? Color.green : Color.red);
    }

    /// <summary>
    /// This function sets the color of the sprite renderer component attached to the cursor to the
    /// specified color.
    /// </summary>
    /// <param name="color"></param>
    private void SetColor(Color color)
    {
        spriteRendererComponent.color = color;
    }
}
