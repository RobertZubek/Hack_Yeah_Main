using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSpawner : MonoBehaviour
{
    public GameObject potionPrefab;     // prefab potionu
    public float spawnZ = 70f;          // odleg³oœæ od gracza
    public float laneDistance = 2f;     // odleg³oœæ miêdzy pasami
    public float spawnInterval = 4f;    // jak czêsto pojawia siê potion

    private float timer = 0f;
    private GameObject currentPotion;   // referencja do aktualnego potionu

    void Update()
    {
        timer += Time.deltaTime;

        // Jeœli nie ma aktywnego potionu, odliczamy czas do spawn
        if (currentPotion == null && timer >= spawnInterval)
        {
            SpawnPotion();
            timer = 0f;
        }
    }

    void SpawnPotion()
    {
        if (potionPrefab == null)
        {
            Debug.LogWarning("Potion prefab nie jest przypisany w Inspectorze!");
            return;
        }

        int lane = Random.value < 0.5f ? -1 : 1;
        Vector3 pos = new Vector3(lane * laneDistance, 1f, spawnZ);

        // Instancjonujemy potion i zapisujemy referencjê
        currentPotion = Instantiate(potionPrefab, pos, Quaternion.identity);
        currentPotion.tag = "Potion";
    }
}
