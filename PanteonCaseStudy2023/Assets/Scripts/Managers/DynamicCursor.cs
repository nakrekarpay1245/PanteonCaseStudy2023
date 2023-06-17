using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCursor : MonoSingleton<DynamicCursor>
{
    [Header("Dynamic Cursor Parameters")]
    [Tooltip("Dynmic cursor sprite renderer component")]
    [SerializeField]
    private SpriteRenderer spriteRendererComponent;

    private Action workingAction;

    private Vector2 mouseOffset;
    private void Awake()
    {
        spriteRendererComponent = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        workingAction?.Invoke();
    }

    public void Display(Building building)
    {
        SetSprite(building.GetBuildingIcon());

        spriteRendererComponent.enabled = true;

        mouseOffset = building.GetBuildingScale();

        workingAction += FollowMousePosition;
    }

    public void Hide()
    {
        spriteRendererComponent.enabled = false;
        workingAction -= FollowMousePosition;
    }

    private void SetSprite(Sprite sprite)
    {
        spriteRendererComponent.sprite = sprite;
    }

    private void FollowMousePosition()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = mousePosition;

        spriteRendererComponent.transform.localPosition = new Vector3(mouseOffset.x / 10, mouseOffset.y / 10, 0);
    }

    public void CanNotPlaceable()
    {
        SetColor(Color.red);
    }

    public void CanPlaceable()
    {
        SetColor(Color.green);
    }

    private void SetColor(Color color)
    {
        spriteRendererComponent.color = color;
    }
}
