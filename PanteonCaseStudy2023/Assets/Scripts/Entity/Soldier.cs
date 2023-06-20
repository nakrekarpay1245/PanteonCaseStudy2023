using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Entity
{
    [Header("Soldier Attack")]
    [Tooltip("Soldier damage")]
    [SerializeField]
    private float damage;

    [Header("Soldier Movement")]
    [Tooltip("Soldier moveSpeed")]
    [SerializeField]
    public float walkSpeedBetweenTiles;

    [Tooltip("The walkpoints that the soldier will advance through in sequence")]
    public List<Tile> walkTileList;

    /// <summary>
    /// The index of the waypoint to advance to
    /// </summary>
    private int currentPositionIndex;

    /// <summary>
    /// The coroutine responsible for movement.
    /// </summary>
    private Coroutine moveCoroutine;

    /// <summary>
    /// The tile where the movement will end.
    /// </summary>
    private Tile endTile = null;

    /// <summary>
    /// The tile where the movement will start.
    /// </summary>
    private Tile startTile = null;

    private void HandleMouseClick()
    {
        InputManager.singleton.OnLeftMouseClick += SelectLastTile;
        InputManager.singleton.OnRightMouseClick += SelectTarget;
    }

    /// <summary>
    /// Moves the entity to a defensive tile, following a path determined by the pathfinding algorithm
    /// </summary>
    /// <param name="tile"></param>
    public void MoveToDefensiveTile(Tile tile)
    {
        endTile = tile;
        startTile = GetTilesInEntity()[0];

        List<Tile> walkTileList = Pathfinding.singleton.FindPath(startTile, endTile);

        if (walkTileList == null)
        {
            IgnoreMouseClick();

            startTile = null;
            endTile = null;
            return;
        }

        MoveToPositionsSmoothly(walkTileList);

        endTile.SetEntity(this);
        endTile.Occupy();
    }

    /// <summary>
    /// Selects a target based on the mouse position and applies damage to the targeted entity.
    /// </summary>
    public void SelectTarget()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Tile tile = TileManager.singleton.GetNearestTile(mousePosition);

        if (!tile || !tile.IsOccupied())
        {
            IgnoreMouseClick();
            return;
        }

        Entity entity = tile.GetEntity();

        entity.TakeDamage(damage);
    }

    /// <summary>
    /// Selects the last tile based on the mouse position and performs actions such as pathfinding,
    /// movement, and occupancy. Handles cases where the tile is occupied or no valid path is found.
    /// </summary>
    private void SelectLastTile()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (startTile)
        {
            endTile?.UnOccupy();
            endTile = TileManager.singleton.GetNearestTile(mousePosition);

            if ((!endTile || endTile.IsOccupied()))
            {
                IgnoreMouseClick();

                endTile.UnOccupy();

                startTile.SetEntity(this);
                startTile.Occupy();

                startTile = null;
                endTile = null;

                return;
            }

            List<Tile> walkTileList = Pathfinding.singleton.FindPath(startTile, endTile);

            endTile.Occupy();

            if (walkTileList == null)
            {
                IgnoreMouseClick();

                endTile.UnOccupy();

                startTile.SetEntity(this);
                startTile.Occupy();

                startTile = null;
                endTile = null;
                return;
            }

            MoveToPositionsSmoothly(walkTileList);

            IgnoreMouseClick();

            startTile = null;
            endTile = null;
        }
    }

    /// <summary>
    /// Initiates smooth movement to a list of target tiles. Checks if there is an ongoing movement 
    /// coroutine and stops it if necessary. Sets the target tile list, current position index, and
    /// starts the movement coroutine.
    /// </summary>
    /// <param name="targetTiles"></param>
    public void MoveToPositionsSmoothly(List<Tile> targetTiles)
    {
        if (moveCoroutine != null)
        {
            StopMoving();
        }

        walkTileList = targetTiles;
        currentPositionIndex = 0;
        moveCoroutine = StartCoroutine(MoveCoroutine());
    }

    /// <summary>
    /// Performs the movement coroutine, smoothly moving the entity between the target tiles. It
    /// iterates through the target tile list, moving the entity towards each target position.
    /// Once the entity reaches a target position, it updates the occupancy status of the current
    /// and next tiles, sets the entity's parent to the current tile, and updates the list of tiles 
    /// in the entity. The coroutine continues until all target positions have been reached. Finally,
    /// it stops the movement coroutine when the movement is complete.
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveCoroutine()
    {
        int currentPositionIndex = 0;
        int targetTileCount = walkTileList.Count;

        while (currentPositionIndex < targetTileCount)
        {
            Vector3 targetPosition = walkTileList[currentPositionIndex].transform.position;
            Vector3 currentPosition = transform.position;

            Vector3 newPosition = Vector3.MoveTowards(currentPosition, targetPosition, walkSpeedBetweenTiles * Time.deltaTime);
            transform.position = newPosition;

            if (newPosition == targetPosition)
            {
                tilesInEntity[0].UnOccupy();
                tilesInEntity.Clear();

                transform.parent = walkTileList[currentPositionIndex].transform;

                tilesInEntity.Add(walkTileList[currentPositionIndex]);
                tilesInEntity[0].SetEntity(this);
                tilesInEntity[0].Occupy();

                currentPositionIndex++;
            }

            yield return null;
        }

        StopMoving();
    }

    /// <summary>
    /// Stops the movement of the entity by clearing the moveCoroutine, resetting the currentPositionIndex, 
    /// and clearing the walkTileList.
    /// </summary>
    private void StopMoving()
    {
        moveCoroutine = null;
        currentPositionIndex = 0;
        walkTileList.Clear();
    }

    public override void DisplayInformation()
    {
        base.DisplayInformation();
    }

    public override void Select()
    {
        base.Select();
        HandleMouseClick();
        startTile = GetTilesInEntity()[0];
    }

    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);
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
    private void IgnoreMouseClick()
    {
        InputManager.singleton.OnLeftMouseClick -= SelectLastTile;
        InputManager.singleton.OnRightMouseClick -= SelectTarget;
    }
}