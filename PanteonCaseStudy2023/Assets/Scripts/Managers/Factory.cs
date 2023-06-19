using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoSingleton<Factory>
{
    [Header("Factory Parameters")]
    [Tooltip("All entity prefabs in game")]
    [SerializeField]
    private List<EntityPrefab> entityPrefabList;

    public Entity CreateEntity(EntityType entityType)
    {
        Entity generatedEntityPrefab = null;
        Entity generatedEntity = null;

        for (int i = 0; i < entityPrefabList.Count; i++)
        {
            if (entityType == entityPrefabList[i].entityType)
            {
                generatedEntityPrefab = entityPrefabList[i].entityPrefab;
                generatedEntity = Instantiate(generatedEntityPrefab, Vector3.zero, Quaternion.identity);
                return generatedEntity;
            }
        }

        Debug.LogError("Undefined entity type");
        return null;
    }
    
    public Entity CreateEntity(EntityType entityType, Vector3 generatePosition,
        Quaternion generateRotation, Transform parentTransform)
    {
        Entity generatedEntityPrefab = null;
        Entity generatedEntity = null;

        for (int i = 0; i < entityPrefabList.Count; i++)
        {
            if (entityType == entityPrefabList[i].entityType)
            {
                generatedEntityPrefab = entityPrefabList[i].entityPrefab;

                generatedEntity = Instantiate(generatedEntityPrefab, generatePosition, 
                    generateRotation, parentTransform);

                return generatedEntity;
            }
        }

        Debug.LogError("Undefined entity type");
        return null;
    }
}

public enum EntityType
{
    Soldier1,
    Soldier2,
    Soldier3,
    Barracks,
    PowerPlant
}
