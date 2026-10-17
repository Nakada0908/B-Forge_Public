using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InfiniteGrassMap : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Tilemap groundTilemap;
    [SerializeField] private Tilemap decorationTilemap;
    [SerializeField] private TileBase grassTile;

    [Header("Decoration Tiles")]
    [SerializeField] private TileBase[] rockTiles;
    [SerializeField] private TileBase[] treeTiles;
    [SerializeField] private TileBase[] graveTiles;

    [Header("Map Settings")]
    [SerializeField] private int chunkSize = 20;

    [Header("Decoration Count Per Chunk")]
    [SerializeField] private int minRockCount = 1;
    [SerializeField] private int maxRockCount = 3;

    [SerializeField] private int minTreeCount = 0;
    [SerializeField] private int maxTreeCount = 2;

    [SerializeField] private int minGraveCount = 3;
    [SerializeField] private int maxGraveCount = 6;

    private Vector2Int currentChunk;

    private void Start()
    {
        currentChunk = GetPlayerChunk();
        GenerateMap();
    }

    private void Update()
    {
        Vector2Int newChunk = GetPlayerChunk();

        if (newChunk != currentChunk)
        {
            currentChunk = newChunk;
            GenerateMap();
        }
    }


    private Vector2Int GetPlayerChunk()
    {
        if(player==null)
        {
            return currentChunk;
        }

        int chunkX = Mathf.FloorToInt(player.position.x / chunkSize);
        int chunkY = Mathf.FloorToInt(player.position.y / chunkSize);

        return new Vector2Int(chunkX, chunkY);
    }

    
    private void GenerateMap()
    {
        groundTilemap.ClearAllTiles();
        decorationTilemap.ClearAllTiles();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector2Int chunk = new Vector2Int(
                    currentChunk.x + x,
                    currentChunk.y + y
                );

                DrawGroundChunk(chunk);
                DrawDecorationChunk(chunk);
            }
        }
    }

    
    private void DrawGroundChunk(Vector2Int chunk)
    {
        int startX = chunk.x * chunkSize;
        int startY = chunk.y * chunkSize;

        for (int x = 0; x < chunkSize; x++)
        {
            for (int y = 0; y < chunkSize; y++)
            {
                Vector3Int tilePos = new Vector3Int(
                    startX + x,
                    startY + y,
                    0
                );

                groundTilemap.SetTile(tilePos, grassTile);
            }
        }
    }

    
    private void DrawDecorationChunk(Vector2Int chunk)
    {
        
        Random.InitState(GetChunkSeed(chunk));

        
        HashSet<Vector3Int> usedPositions = new HashSet<Vector3Int>();

        DrawRandomDecorations(
            chunk,
            graveTiles,
            minGraveCount,
            maxGraveCount,
            usedPositions
        );

        DrawRandomDecorations(
            chunk,
            rockTiles,
            minRockCount,
            maxRockCount,
            usedPositions
        );

        DrawRandomDecorations(
            chunk,
            treeTiles,
            minTreeCount,
            maxTreeCount,
            usedPositions
        );
    }

    
    private void DrawRandomDecorations(
        Vector2Int chunk,
        TileBase[] tiles,
        int minCount,
        int maxCount,
        HashSet<Vector3Int> usedPositions
    )
    {
       
        if (tiles == null || tiles.Length == 0)
        {
            return;
        }

        int count = Random.Range(minCount, maxCount + 1);

        int startX = chunk.x * chunkSize;
        int startY = chunk.y * chunkSize;

        for (int i = 0; i < count; i++)
        {
            Vector3Int tilePos = GetRandomPosition(startX, startY);

            int tryCount = 0;

            // 
            while (usedPositions.Contains(tilePos) && tryCount < 20)
            {
                tilePos = GetRandomPosition(startX, startY);
                tryCount++;
            }

            
            if (usedPositions.Contains(tilePos))
            {
                continue;
            }

            
            TileBase selectedTile =
                tiles[Random.Range(0, tiles.Length)];

            decorationTilemap.SetTile(tilePos, selectedTile);
            usedPositions.Add(tilePos);
        }
    }

    
    private Vector3Int GetRandomPosition(int startX, int startY)
    {
        int x = Random.Range(1, chunkSize - 1);
        int y = Random.Range(1, chunkSize - 1);

        return new Vector3Int(
            startX + x,
            startY + y,
            0
        );
    }

    
    private int GetChunkSeed(Vector2Int chunk)
    {
        return chunk.x * 73856093 ^ chunk.y * 19349663;
    }
}