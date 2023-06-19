using System;
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

    [Tooltip("The waypoints that the soldier will advance through in sequence")]
    public List<Tile> walkTileList;

    //The index of the waypoint to advance to
    private int currentPositionIndex;

    //Controls the movement of the soldier
    private bool isMoving;

    private Action selectTargetAction;

    private Tile endTile = null;
    private Tile startTile = null;

    private void Update()
    {
        selectTargetAction?.Invoke();

        if (isMoving)
        {
            Move();
        }
    }

    public void SelectTarget()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (startTile)
            {
                endTile = TileManager.singleton.GetNearestTile(mousePosition);

                if ((!endTile || endTile.IsOccupied()))
                {
                    selectTargetAction -= SelectTarget;

                    startTile = null;
                    endTile = null;

                    return;
                }

                List<Tile> walkTileList = Pathfinding.singleton.FindPath(startTile, endTile);

                if (walkTileList == null)
                {
                    selectTargetAction -= SelectTarget;

                    startTile = null;
                    endTile = null;
                    return;
                }

                MoveToPositionsSmoothly(walkTileList);

                selectTargetAction -= SelectTarget;

                startTile = null;
                endTile = null;
            }
            else
            {
                startTile = GetTilesInEntity()[0];
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Tile tile = TileManager.singleton.GetNearestTile(mousePosition);

            if (!tile || !tile.IsOccupied())
            {
                selectTargetAction -= SelectTarget;
                return;
            }

            Entity entity = tile.GetEntity();

            entity.TakeDamage(damage);
        }
    }

    public void MoveToPositionsSmoothly(List<Tile> targetTiles)
    {
        if (isMoving)
        {
            StopMoving();
        }

        walkTileList = targetTiles;
        currentPositionIndex = 0;
        isMoving = true;
    }

    private void Move()
    {
        Vector3 targetPosition = walkTileList[currentPositionIndex].transform.position;

        Vector3 currentPosition = transform.position;

        Vector3 newPosition = Vector3.MoveTowards(currentPosition,
            targetPosition, walkSpeedBetweenTiles * Time.deltaTime);

        transform.position = newPosition;

        if (newPosition == targetPosition)
        {
            tilesInEntity[0].UnOccupy();
            tilesInEntity.Clear();

            currentPositionIndex++;

            if (currentPositionIndex >= walkTileList.Count)
            {
                StopMoving();
                return;
            }

            tilesInEntity.Add(walkTileList[currentPositionIndex]);
            tilesInEntity[0].SetEntity(this);
        }
    }

    private void StopMoving()
    {
        isMoving = false;
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
        selectTargetAction += SelectTarget;
    }

    public override void DeSelect()
    {
        base.DeSelect();
    }

    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);
    }
}
