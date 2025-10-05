using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class RunnerController : MonoBehaviour
{
    public int damage = 10;
    public int points = 5;

    public AudioClip enemydethsound; // przypisz PotionPickup.wav
    private AudioSource audioSource;

    public AudioClip drinkSound; // przypisz PotionPickup.wav
    private AudioSource audioSourceDrink;

    public float sideSpeed = 5f;
    public float laneDistance = 2f;
    public float jumpForce = 5f;
    public float gravity = -9.81f;

    public float attackRange = 2f; // jak blisko przeciwnika musisz byæ, ¿eby go uderzyæ

    public float destroyRange = 2000f;

    [Header("Game Over Settings")]
    public GameObject gameOverPanel;

    private CharacterController controller;
    private Vector3 velocity;
    private int targetLane = -1;

    private Vector3 startPosition;

    private float timer = 0f;
    private float gameOverDelay = 1.5f;

    public int healAmount = 10; // ile zdrowia dodaje

    void Start()
    {
        controller = GetComponent<CharacterController>();
        startPosition = transform.position;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        audioSourceDrink = gameObject.AddComponent<AudioSource>();
        audioSourceDrink.playOnAwake = false;
    }

    void Update()
    {
        if (FindObjectOfType<UIManager>().isGameOver)
        {
            Collider[] Enemies = Physics.OverlapSphere(transform.position, destroyRange);
            foreach (Collider enemy in Enemies)
            {
                if (enemy.CompareTag("Enemy"))
                {

                    Destroy(enemy.gameObject);
                    Debug.Log("Atak udany!");

                }
            }
            // zatrzymaj grê po chwili (mo¿esz dodaæ animacjê przed zatrzymaniem)
            timer += Time.deltaTime;
            if (timer >= gameOverDelay)
            {
                
                if (gameOverPanel != null)
                {
                    gameOverPanel.SetActive(true);
                }
                Time.timeScale = 0f;

            }
            // zatrzymuje ruch w grze
        }

        // Sterowanie pasami
        if (FindObjectOfType<HandTrackerController>().movement == 2 || Input.GetKeyDown(KeyCode.A))
            targetLane = -1;
        if (FindObjectOfType<HandTrackerController>().movement == 1 || Input.GetKeyDown(KeyCode.D))
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
        if (Input.GetKeyDown(KeyCode.K) || (FindObjectOfType<HandTrackerController>().is_attack == true && FindObjectOfType<HandTrackerController>().movement != 5))
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

                audioSource.PlayOneShot(enemydethsound);
                FindObjectOfType<UIManager>().AddScore(points);
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
            FindObjectOfType<UIManager>().TakeDamage(damage);
            Debug.Log("Gracz uderzony! Tracisz ¿ycie");
            // Tutaj mo¿esz w przysz³oœci dodaæ zmniejszenie ¿ycia / jumpscare
        }


        Collider[] hitPotion = Physics.OverlapSphere(transform.position, attackRange);

        if (hit.collider.CompareTag("Potion"))
        {
            foreach (Collider Potion in hitPotion)
            {
                if (Potion.CompareTag("Potion"))
                {
                    Destroy(Potion.gameObject);
                    audioSource.PlayOneShot(drinkSound);
                }
            }

            //PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
            //if (playerHealth != null)
            //{
                //playerHealth.Heal(healAmount);
                FindObjectOfType<UIManager>().Heal(healAmount);
            //}

            // tutaj mo¿esz dodaæ efekt VFX lub dŸwiêk
            // Tutaj mo¿esz w przysz³oœci dodaæ zmniejszenie ¿ycia / jumpscare
        }

    }
}

