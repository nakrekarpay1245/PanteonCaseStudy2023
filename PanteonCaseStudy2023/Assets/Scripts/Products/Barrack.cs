using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrack : Building
{
    [Header("Barrack Parameters")]
    [Tooltip("soldier prefab to be formed in the barrack")]
    [SerializeField]
    private Soldier soldierPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ProduceSoldier();
        }
    }

    public void ProduceSoldier()
    {
        Tile nearestTile =
            SelectionManager.singleton.GetNearestUnOccupiedTile(tilesInBuilding[0].transform.localPosition);

        Soldier generatedSoldier = Instantiate(soldierPrefab, nearestTile.transform);

        generatedSoldier.transform.localPosition = Vector3.zero;

        //nearestTile.Occupy();
        nearestTile.SetSoldier(generatedSoldier);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    public override void Select()
    {
        base.Select();
        UIManager.singleton.DisplaySoldiers();
    }
}
