using System;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IEntity
{
    [Header("Building Parameters")]
    [Header("Type")]
    [Tooltip("It holds the type of this entity")]
    [SerializeField]
    protected EntityType entityType;

    [Header("Informations")]
    [Tooltip("It holds the name of this entity")]
    [SerializeField]
    protected string entityName;

    [Tooltip("It holds the description of this entity")]
    [TextArea(5, 10)]
    [SerializeField]
    protected string entityDescription;

    [Tooltip("It holds the icon of this entity")]
    [SerializeField]
    protected Sprite entityIcon;

    [Header("Health")]
    [Tooltip("It holds the health data of this entity")]
    [SerializeField]
    protected float entityHealth;

    public List<Tile> tilesInEntity;

    public virtual void DisplayInformation()
    {
        //Debug.Log(entityName + "'s information displayed!");

        UIManager.singleton.DisplayInformationMenu(entityName, entityDescription, entityIcon);
    }

    public virtual void Dead()
    {
        for (int i = 0; i < tilesInEntity.Count; i++)
        {
            tilesInEntity[i].SetEntity(null);
            tilesInEntity[i].UnOccupy();
        }
        Destroy(gameObject);
    }

    public virtual void Select()
    {
        //Debug.Log(entityName + "'s selected!");

        DisplayInformation();
    }

    public virtual void TakeDamage(float damageAmount)
    {
        entityHealth -= damageAmount;

        if (entityHealth <= 0)
        {
            entityHealth = 0;
            Dead();
            return;
        }
    }

    public virtual void SetTilesInEntity(List<Tile> tileList)
    {
        tilesInEntity = tileList;
    }

    public virtual List<Tile> GetTilesInEntity()
    {
        return tilesInEntity;
    }

    public virtual EntityType GetEntityType()
    {
        return entityType;
    }
}
