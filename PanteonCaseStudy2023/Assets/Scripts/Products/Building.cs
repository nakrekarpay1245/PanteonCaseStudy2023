using System;
using System.Collections.Generic;
using UnityEngine;

public class Building : Entity
{
    [Header("Building Scale")]
    [Tooltip("Building horizontal scale")]
    [SerializeField]
    protected int buildingWidth;
    [Tooltip("Building vertical scale")]
    [SerializeField]
    protected int buildingHeight;

    public Vector2Int GetBuildingScale()
    {
        Vector2Int buildingScale = new Vector2Int(buildingWidth, buildingHeight);
        return buildingScale;
    }

    public Sprite GetBuildingIcon()
    {
        return entityIcon;
    }

    public override void DisplayInformation()
    {
        base.DisplayInformation();
    }

    public override void Select()
    {
        base.Select();
    }

    public override void DeSelect()
    {
        base.DeSelect();
    }

    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);
    }

    public override void SetTilesInEntity(List<Tile> tileList)
    {
        base.SetTilesInEntity(tileList);
    }

    public override List<Tile> GetTilesInEntity()
    {
        return base.GetTilesInEntity();
    }

    public override EntityType GetEntityType()
    {
        return base.GetEntityType();
    }
}
