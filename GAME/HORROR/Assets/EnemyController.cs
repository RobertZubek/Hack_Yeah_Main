using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 8f; // prêdkoœæ bazowa
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // ruch w kierunku gracza (tylko po Z)
        transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);

        // usuwanie po wyjœciu za gracza
        if (transform.position.z < player.position.z - 5f)
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Kolizja z graczem!");
            Destroy(gameObject);
        }
    }
}
