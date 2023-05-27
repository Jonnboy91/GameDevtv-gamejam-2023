using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using NavMeshPlus.Components;

public class ProceduralTilemapGenerator : MonoBehaviour
{
    public Tilemap groundTilemap;
    public Tilemap rockTilemap;

    public TileBase[] groundTileVariations;
    public TileBase[] rockTileVariations;

    public int tileSize = 1;
    public int loadDistance = 20;
    public int generationLoadDistance = 5;
    public int unloadDistance = 20;

    public float rockSpawnChance = 0.2f;

    private Transform player;
    private Vector3Int lastTilePosition;

    public NavMeshSurface Surface2D;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastTilePosition = Vector3Int.zero;
    }

    private void Start()
    {
        Surface2D.BuildNavMeshAsync();
        GenerateInitialTilemap();
    }

    private void LateUpdate()
    {
        Vector3Int currentPlayerTile = WorldToTilemapPosition(player.position);

        if (Mathf.Abs(currentPlayerTile.x - lastTilePosition.x) >= loadDistance - generationLoadDistance ||
            Mathf.Abs(currentPlayerTile.y - lastTilePosition.y) >= loadDistance - generationLoadDistance)
        {
            GenerateNewTiles(currentPlayerTile);
            DeleteOldTiles(currentPlayerTile);
            Surface2D.UpdateNavMesh(Surface2D.navMeshData);
        }
    }

    private void GenerateInitialTilemap()
    {
        Vector3Int currentPlayerTile = WorldToTilemapPosition(player.position);
        GenerateNewTiles(currentPlayerTile);
        Surface2D.UpdateNavMesh(Surface2D.navMeshData);
    }

    private void GenerateNewTiles(Vector3Int currentPlayerTile)
    {

        BoundsInt visibleAreaBounds = new BoundsInt(
        currentPlayerTile.x - loadDistance,
        currentPlayerTile.y - loadDistance,
        0,
        loadDistance * 2,
        loadDistance * 2,
        1
        );

    // Iterate over the visible area bounds to generate new tiles
    foreach (var position in visibleAreaBounds.allPositionsWithin)
    {
        // Check if the position is outside the current tilemap bounds
        if (!groundTilemap.HasTile(position))
        {
            TileBase randomGroundTile = groundTileVariations[Random.Range(0, groundTileVariations.Length)];
            groundTilemap.SetTile(position, randomGroundTile);

            // Check if the position is within the rock spawn chance and no ground tile exists
            if (Random.value < rockSpawnChance && !rockTilemap.HasTile(position))
            {
                TileBase randomRockTile = rockTileVariations[Random.Range(0, rockTileVariations.Length)];
                rockTilemap.SetTile(position, randomRockTile);
            }
        }
    }

        lastTilePosition = currentPlayerTile;
    }

    private void DeleteOldTiles(Vector3Int currentPlayerTile)
    {
        BoundsInt bounds = groundTilemap.cellBounds;
        TileBase[] allTiles = groundTilemap.GetTilesBlock(bounds);

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);

                if (Vector3Int.Distance(tilePosition, currentPlayerTile) > unloadDistance)
                {
                    groundTilemap.SetTile(tilePosition, null);
                    rockTilemap.SetTile(tilePosition, null);
                }
            }
        }
    }

    private Vector3Int WorldToTilemapPosition(Vector3 worldPosition)
    {
        return groundTilemap.WorldToCell(worldPosition);
    }
}
