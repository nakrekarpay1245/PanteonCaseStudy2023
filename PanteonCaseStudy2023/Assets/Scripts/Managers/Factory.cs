using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoSingleton<Factory>
{
    [Header("Factory Parameters")]
    [Tooltip("All entity prefabs in game")]
    [SerializeField]
    private List<EntityPrefab> entityPrefabList;

    /// <summary>
    /// Creates an entity of the specified entity type at the default position and rotation.
    /// </summary>
    /// <param name="entityType">The type of the entity to create.</param>
    /// <returns>The created entity.</returns>
    public Entity CreateEntity(EntityType entityType)
    {
        Entity generatedEntityPrefab = GetEntityPrefab(entityType);

        if (generatedEntityPrefab != null)
        {
            Entity generatedEntity = Instantiate(generatedEntityPrefab, Vector3.zero, Quaternion.identity);
            return generatedEntity;
        }

        Debug.LogError("Undefined entity type");
        return null;
    }

    /// <summary>
    /// Creates an entity of the specified entity type at the given position and rotation,
    /// under the specified parent transform.
    /// </summary>
    /// <param name="entityType">The type of the entity to create.</param>
    /// <param name="generatePosition">The position to generate the entity.</param>
    /// <param name="generateRotation">The rotation of the generated entity.</param>
    /// <param name="parentTransform">The parent transform under which the entity will be placed.</param>
    /// <returns>The created entity.</returns>
    public Entity CreateEntity(EntityType entityType, Vector3 generatePosition,
        Quaternion generateRotation, Transform parentTransform)
    {
        Entity generatedEntityPrefab = GetEntityPrefab(entityType);

        if (generatedEntityPrefab != null)
        {
            Entity generatedEntity = Instantiate(generatedEntityPrefab, generatePosition,
                generateRotation, parentTransform);
            return generatedEntity;
        }

        Debug.LogError("Undefined entity type");
        return null;
    }

    /// <summary>
    /// Returns the prefab of the entity based on the specified entity type.
    /// </summary>
    /// <param name="entityType">The type of the entity.</param>
    /// <returns>The prefab of the entity.</returns>
    private Entity GetEntityPrefab(EntityType entityType)
    {
        for (int i = 0; i < entityPrefabList.Count; i++)
        {
            EntityPrefab entityPrefab = entityPrefabList[i];
            if (entityType == entityPrefab.entityType)
            {
                return entityPrefab.entityPrefab;
            }
        }

        return null;
    }
}

public enum EntityType
{
    Soldier1,
    Soldier2,
    Soldier3,
    Barracks,
    PowerPlant,
    House,
    Hospital
}
