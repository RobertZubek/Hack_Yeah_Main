using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject slashPrefab;
    public Transform slashSpawnPoint;
    public float slashLifetime = 0.5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Attack();
        }
    }

    void Attack()
    {
        // Tworzymy efekt slash
        GameObject slash = Instantiate(slashPrefab, slashSpawnPoint.position, slashSpawnPoint.rotation);

        // Jeœli ma Rigidbody lub efekt siê porusza, mo¿na dodaæ np.:
        // slash.GetComponent<Rigidbody>().velocity = transform.forward * 5f;

        Destroy(slash, slashLifetime);
    }
}
