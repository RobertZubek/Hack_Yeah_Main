using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstacles")]
    public GameObject obstaclePrefab;   // prefab przeciwnika
    public float spawnZ = 70f;          // odleg³oœæ od gracza
    public float laneDistance = 2f;     // odleg³oœæ miêdzy pasami
    public float spawnInterval = 1f;    // co ile sekund spawnowaæ przeszkody

    [Header("Potions")]
    public GameObject potionPrefab;     // prefab potionu
    [Range(0f, 1f)]
    public float potionSpawnChance = 0.4f; // 10% szans na spawn potionu zamiast wroga

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObject();
            timer = 0f;
        }
    }

    void SpawnObject()
    {
        // losowy pas (-1 = lewy, 1 = prawy)
        int lane = Random.value < 0.5f ? -1 : 1;
        Vector3 pos = new Vector3(lane * laneDistance, 1f, spawnZ);

        // losuj: przeciwnik lub potion
        if (Random.value < potionSpawnChance)
        {
            // spawn potionu
            Instantiate(potionPrefab, pos, Quaternion.identity);
        }
        else
        {
            // spawn przeciwnika (obróconego w stronê gracza)
            Instantiate(obstaclePrefab, pos, Quaternion.Euler(0, 180, 0));
        }
    }
}
