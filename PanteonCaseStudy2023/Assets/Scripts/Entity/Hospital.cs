using System.Collections.Generic;

public class Hospital : Building
{
    public override void DisplayInformation()
    {
        base.DisplayInformation();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    public override void Select()
    {
        base.Select();
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
