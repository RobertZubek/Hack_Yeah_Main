using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformManager : MonoBehaviour
{
    public GameObject[] platforms; // przypisz 3 platformy w inspektorze
    public float speed = 10f;
    public float platformLength = 30f; // d³ugoœæ jednej platformy (dopasuj do swojej sceny)

    void Update()
    {
        foreach (GameObject platform in platforms)
        {
            // przesuwaj platformy w stronê gracza (czyli w dó³ osi Z)
            platform.transform.Translate(Vector3.back * speed * Time.deltaTime);

            // jeœli platforma minie kamerê (np. Z < -platformLength), przesuñ j¹ na koniec
            if (platform.transform.position.z < -platformLength)
            {
                float maxZ = GetFurthestPlatformZ();
                platform.transform.position = new Vector3(
                    platform.transform.position.x,
                    platform.transform.position.y,
                    maxZ + platformLength
                );
            }
        }
    }

    float GetFurthestPlatformZ()
    {
        float maxZ = float.MinValue;
        foreach (GameObject platform in platforms)
        {
            if (platform.transform.position.z > maxZ)
                maxZ = platform.transform.position.z;
        }
        return maxZ;
    }
}

