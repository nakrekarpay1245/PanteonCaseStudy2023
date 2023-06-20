using UnityEngine;

[System.Serializable]
public class EntityPrefab 
{
    [Header("Entity Prefab Parameters")]
    [Tooltip("The type of the held entity")]
    public EntityType entityType;
    [Tooltip("The prefab of the held entity")]
    public Entity entityPrefab;
}
