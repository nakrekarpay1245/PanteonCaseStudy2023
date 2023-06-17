using System;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [Header("Building Parameters")]
    [Header("Building Informations")]
    [Tooltip("It holds the name of the building")]
    [SerializeField]
    protected string buildingName;
    [Tooltip("It holds the name of the building")]
    [TextArea(3, 3)]
    [SerializeField]
    protected string buildingDescription;
    [Tooltip("It holds the icon of the building")]
    [SerializeField]
    private Sprite buildingIcon;

    [Header("Building Health")]
    [Tooltip("It holds the health data of the building")]
    [SerializeField]
    protected float buildingHealth;

    [Header("Building Scale")]
    [Tooltip("Building horizontal scale")]
    [SerializeField]
    protected int buildingWidth;
    [Tooltip("Building vertical scale")]
    [SerializeField]
    protected int buildingHeight;

    public List<Tile> tilesInBuilding;

    public virtual void TakeDamage(float damage)
    {
        buildingHealth -= damage;
    }

    public Vector2Int GetBuildingScale()
    {
        Vector2Int buildingScale = new Vector2Int(buildingWidth, buildingHeight);
        return buildingScale;
    }

    public void SetTilesInBuilding(List<Tile> tilesInBuilding)
    {
        this.tilesInBuilding = tilesInBuilding;
    }

    public Sprite GetBuildingIcon()
    {
        return buildingIcon;
    }

    private void DisplayInformation()
    {
        //Debug.Log("Information Displayed!");
        UIManager.singleton.DisplayInformation(buildingName, buildingDescription, buildingIcon);
    }

    public virtual void Select()
    {
        DisplayInformation();
    }
}
