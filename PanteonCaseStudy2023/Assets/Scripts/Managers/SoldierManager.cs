using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierManager : MonoBehaviour
{
    private void Update()
    {
        SelectBuildings();
    }

    public void SelectBuildings()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Soldier selectedSoldier = SelectionManager.singleton.GetNearestTile(mousePosition).GetSoldier();

            selectedSoldier?.Select();
        }
    }
}
