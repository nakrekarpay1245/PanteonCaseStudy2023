using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building
{
    /// <summary>
    /// Produces a soldier of the specified entity prefab type.
    /// The soldier is created on the nearest unoccupied tile to the current entity.
    /// </summary>
    /// <param name="entityPrefab">The entity prefab type for the soldier.</param>
    public void ProduceSoldier(EntityPrefab entityPrefab)
    {
        Tile nearestTile =
            TileManager.singleton.GetNearestUnOccupiedTile(tilesInEntity[0].transform.localPosition);

        if (!nearestTile)
        {
            return;

        }
        Soldier generatedSoldier = Factory.singleton.CreateEntity(entityPrefab.entityType,
            transform.position, Quaternion.identity, nearestTile.transform).GetComponent<Soldier>();

        generatedSoldier.SetTilesInEntity(new List<Tile> { nearestTile });

        generatedSoldier.MoveToDefensiveTile(nearestTile);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    public override void Select()
    {
        base.Select();
        UIManager.singleton.AddBarrackToButtons(this);
        UIManager.singleton.DisplaySoldierButtons();
        UIManager.singleton.HideBuildingButtons();
    }

    public override void DisplayInformation()
    {
        base.DisplayInformation();
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
