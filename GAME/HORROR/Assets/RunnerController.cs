using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RunnerController : MonoBehaviour
{
    public float sideSpeed = 5f;      // prêdkoœæ przesuwania siê w lewo/prawo
    public float laneDistance = 2f;   // odleg³oœæ miêdzy lewym a prawym pasem
    public float jumpForce = 5f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private int targetLane = -1; // startujemy na lewym pasie (-1 = lewy, 1 = prawy)

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Sterowanie klawiatur¹
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            targetLane = -1; // zawsze lewy pas
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            targetLane = 1; // zawsze prawy pas
        }

        // Oblicz ruch w lewo/prawo
        float targetX = targetLane * laneDistance;
        float diffX = targetX - transform.position.x;
        Vector3 move = Vector3.right * diffX * sideSpeed;

        // Skok
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpForce;
        }

        // Grawitacja
        velocity.y += gravity * Time.deltaTime;

        // Wykonaj ruch
        controller.Move((move + velocity) * Time.deltaTime);
    }
}

