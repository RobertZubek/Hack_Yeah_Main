using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;   // prefab przeciwnika
    public float spawnZ = 70f;          // odleg³oœæ od gracza
    public float laneDistance = 2f;     // odleg³oœæ miêdzy pasami
    public float spawnInterval = 2f;    // co ile sekund spawnowaæ przeszkody

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void SpawnObstacle()
    {
        // Losowo wybiera pas (-1 = lewy, 1 = prawy)
        int lane = Random.value < 0.5f ? -1 : 1;

        // Pozycja spawnu wzglêdem œrodka
        Vector3 pos = new Vector3(lane * laneDistance, 1f, spawnZ);

        // Tworzenie przeszkody
        Instantiate(obstaclePrefab, pos, Quaternion.Euler(0, 180, 0)); // 180°, ¿eby patrzy³ w stronê gracza
    }
}
