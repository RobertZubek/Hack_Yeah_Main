using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 200f;
    public Transform playerBody;
    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // ukrywa i blokuje kursor
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // obrót góra/dó³ kamery
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f); // ogranicz zakres
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // obrót gracza w osi Y
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
