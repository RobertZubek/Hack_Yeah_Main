using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RunnerController : MonoBehaviour
{
    public float sideSpeed = 5f;
    public float laneDistance = 2f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;

    public float attackRange = 2f; // jak blisko przeciwnika musisz byæ, ¿eby go uderzyæ

    private CharacterController controller;
    private Vector3 velocity;
    private int targetLane = -1;

    private Vector3 startPosition;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        startPosition = transform.position;
    }

    void Update()
    {
        
        // Sterowanie pasami
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            targetLane = -1;
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            targetLane = 1;

        float targetX = targetLane * laneDistance;
        float diffX = targetX - transform.position.x;
        Vector3 move = Vector3.right * diffX * sideSpeed;

        // Skok
        if (controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
            velocity.y = jumpForce;

        // Grawitacja
        velocity.y += gravity * Time.deltaTime;

        // Ruch
        controller.Move((move + velocity) * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, startPosition.z);
        // Atak
        if (Input.GetKeyDown(KeyCode.K))
        {
            Attack();
        }
    }

    void Attack()
    {
        // Wykryj przeciwników w zasiêgu
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Destroy(enemy.gameObject);
                Debug.Log("Atak udany!"); // w przysz³oœci daj punkty
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {  // Wykryj przeciwników w zasiêgu
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange);

       
        if (hit.collider.CompareTag("Enemy"))
        {
            foreach (Collider enemy in hitEnemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    Destroy(enemy.gameObject);
                }
            }
            
            Debug.Log("Gracz uderzony! Tracisz ¿ycie");
            // Tutaj mo¿esz w przysz³oœci dodaæ zmniejszenie ¿ycia / jumpscare
        }
    }
}

