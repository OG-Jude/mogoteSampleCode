 using UnityEngine;
using System.Collections.Generic;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefab; // Array of tile prefabs
    public float tileLength = 5; // Length of a tile
    public float moveSpeed = 0.533f; // Speed at which tiles move
    public float deletionZ = -10f; // Z position at which tiles are deleted
    public int initialTileCount = 6; // Number of tiles to maintain

    private Queue<GameObject> activeTiles = new Queue<GameObject>(); // Queue for active tiles

    void Start()
    {
        // Spawn initial tiles
        for (int i = 0; i < initialTileCount; i++)
        {
            SpawnTile(Random.Range(0, tilePrefab.Length));
        }
    }

    void Update()
    {
        MoveTiles();
        ManageTiles();
    }

    private void SpawnTile(int tileIndex)
    {
        Vector3 spawnPosition = activeTiles.Count > 0
            ? activeTiles.ToArray()[activeTiles.Count - 1].transform.position + new Vector3(0, 0, tileLength)
            : Vector3.zero;

        GameObject tile = Instantiate(tilePrefab[tileIndex], spawnPosition, Quaternion.identity);
        activeTiles.Enqueue(tile);
    }

    private void MoveTiles()
    {
        foreach (GameObject tile in activeTiles)
        {
            tile.transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
        }
    }

    private void ManageTiles()
    {
        while (activeTiles.Count > 0 && activeTiles.Peek().transform.position.z < deletionZ)
        {
            Destroy(activeTiles.Dequeue());
        }

        while (activeTiles.Count < initialTileCount)
        {
            SpawnTile(Random.Range(0, tilePrefab.Length));
        }
    }
}
