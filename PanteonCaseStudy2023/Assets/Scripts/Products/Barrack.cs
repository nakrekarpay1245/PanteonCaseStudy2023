using System;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : Building
{
    [Header("Barrack Parameters")]
    [Tooltip("soldier prefab to be formed in the barrack")]
    [SerializeField]
    private Soldier soldierPrefab;

    public void ProduceSoldier()
    {
        //Debug.Log(name + " Produce soldier!");

        Tile nearestTile =
            TileManager.singleton.GetNearestUnOccupiedTile(tilesInEntity[0].transform.localPosition);

        Soldier generatedSoldier = Factory.singleton.CreateEntity(EntityType.Soldier1,
            nearestTile.transform.position, Quaternion.identity,
                nearestTile.transform).GetComponent<Soldier>();

        generatedSoldier.SetTilesInEntity(new List<Tile> { nearestTile });

        nearestTile.SetEntity(generatedSoldier);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    public override void Select()
    {
        base.Select();
        UIManager.singleton.AddBarrackToButton(this);
        UIManager.singleton.DisplaySoldierButtons();
        UIManager.singleton.HideBuildingButtons();
    }

    public override void DeSelect()
    {
        base.DeSelect();
    }

    public override void DisplayInformation()
    {
        base.DisplayInformation();
    }
}
