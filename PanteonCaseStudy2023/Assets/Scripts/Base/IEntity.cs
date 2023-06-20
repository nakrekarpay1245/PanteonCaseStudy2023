using System.Collections.Generic;
using UnityEngine;

public interface IEntity
{
    public void TakeDamage(float damageAmount);
    public void Select();
    public void Dead();
    public void DisplayInformation();
    public void SetTilesInEntity(List<Tile> tileList);
    public List<Tile> GetTilesInEntity();
    public EntityType GetEntityType();
}
