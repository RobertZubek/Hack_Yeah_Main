using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingItem : MonoBehaviour
{
    private float speed;
    private UIManager uiManager;

    public float destroyZ = -30f;

    void Start()
    {
        // znajdŸ UIManager w scenie
        uiManager = FindObjectOfType<UIManager>();

        // pobierz aktualn¹ prêdkoœæ z UIManager
        if (uiManager != null)
            speed = uiManager.GetCurrentEnemySpeed();
        else
            speed = 10f; // zapasowa wartoœæ, gdyby nie znalaz³ UIManagera
    }

    void Update()
    {
        // poruszaj potion w stronê gracza
        transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);

        // usuñ po miniêciu gracza
        if (transform.position.z < destroyZ)
            Destroy(gameObject);
    }
}


