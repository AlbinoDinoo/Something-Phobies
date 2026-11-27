using UnityEngine;
using System.Collections.Generic;

public class PlatformSpawner : MonoBehaviour
{

    //This script was made with a little help from AI. Mostly to find out i needed to use a dictionary instead of an array since it was impossible to destory objects and remove components doing runtime
    public Transform Player;
    public GameObject PlatformPrefab;

    public float TileSize = 2f;
    public int SpawnRange = 2;
    public float ExclusionRadius = 4f;
    public float DespawnDistance = 10f;

    private Dictionary<Vector2Int, GameObject> SpawnedTiles
        = new Dictionary<Vector2Int, GameObject>();

    void Update()
    {
        SpawnPlatforms();
        DespawnPlatforms();
    }

    void SpawnPlatforms()
    {
        int PlayerX = Mathf.RoundToInt(
            (Player.position.x - transform.position.x) / TileSize
        );

        int PlayerZ = Mathf.RoundToInt(
            (Player.position.z - transform.position.z) / TileSize
        );

        for (int X = -SpawnRange; X <= SpawnRange; X++)
        {
            for (int Z = -SpawnRange; Z <= SpawnRange; Z++)
            {
                Vector2Int Tile = new Vector2Int(
                    PlayerX + X, PlayerZ + Z
                );

                Vector3 Position = transform.position + new Vector3(
                    Tile.x * TileSize, -2, Tile.y * TileSize
                );

                Vector2 FlatPosition = new Vector2(
                    Position.x, Position.z
                );

                if (FlatPosition.magnitude < ExclusionRadius)
                    continue;

                if (!SpawnedTiles.ContainsKey(Tile))
                {
                    float RandomRotation = Random.Range(0, 4) * 90f;

                    Quaternion Rotation = Quaternion.Euler(0, RandomRotation, 0);

                    GameObject Platform = Instantiate(PlatformPrefab, Position, Rotation, transform);

                    SpawnedTiles.Add(Tile, Platform);
                }
            }
        }
    }

    void DespawnPlatforms()
    {
        List<Vector2Int> TilesToRemove = new List<Vector2Int>();

        foreach (var Tile in SpawnedTiles)
        {
            Vector3 Difference =
                Player.position - Tile.Value.transform.position;

            Difference.y = 0;

            if (Difference.magnitude > DespawnDistance)
            {
                Destroy(Tile.Value);
                TilesToRemove.Add(Tile.Key);
            }
        }

        foreach (Vector2Int Tile in TilesToRemove)
        {
            SpawnedTiles.Remove(Tile);
        }
    }
}