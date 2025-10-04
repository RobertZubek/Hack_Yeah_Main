using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platformPrefab;
    public float spawnZ = 30f;
    public float platformLength = 20f;
    public int maxPlatforms = 6;

    void Start()
    {
        for (int i = 0; i < maxPlatforms; i++)
        {
            SpawnPlatform(i * platformLength);
        }
    }

    void SpawnPlatform(float zOffset)
    {
        Vector3 pos = new Vector3(0f, 0f, spawnZ + zOffset);
        Instantiate(platformPrefab, pos, Quaternion.identity);
    }

    void Update()
    {
        // mo¿esz tu póŸniej dodaæ spawnowanie w czasie
    }
}
