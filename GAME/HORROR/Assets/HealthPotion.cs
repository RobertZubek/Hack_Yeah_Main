using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
   
    public float rotationSpeed = 50f; // efekt obracania

    void Update()
    {
        // obracanie, ¿eby wygl¹da³o ³adnie
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }

}
