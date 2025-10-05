using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;

    private int health = 100;
    private int score = 0;

    [Header("Enemy Speed Settings")]
    public float baseEnemySpeed = 20f;
    public float speedIncreasePer10Points = 2f;

    [Header("Game Over Settings")]
    public GameObject gameOverPanel;     
    public Animator playerAnimator;


    public bool isGameOver = false;


    void Start()
    {
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        if (isGameOver) return;

        score += amount;
        UpdateUI();
        UpdateEnemySpeeds();
    }

    public void TakeDamage(int damage)
    {
        if (isGameOver) return;

        health -= damage;
        if (health < 0) health = 0;
        UpdateUI();
        if (health <= 0)
        {
            GameOver();
        }

    }

    public void Heal(int heal)
    {
        if (isGameOver) return;

        health += heal;
        if (heal >=100) health = 100;
        UpdateUI();


    }

    private void UpdateUI()
    {
        healthText.text = "Health: " + health;
        scoreText.text = "Score: " + score;
        
        
    }
    private void UpdateEnemySpeeds()
    {
        float newSpeed = GetCurrentEnemySpeed();

        // znajdŸ wszystkich aktywnych przeciwników w scenie i ustaw im now¹ prêdkoœæ
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();
        foreach (var e in enemies)
        {
            e.SetSpeed(newSpeed);
        }
    }

    public void GameOver()
    {
        isGameOver = true;

        Debug.Log("GAME OVER!");

        // wywo³aj animacjê œmierci gracza
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("Death");
        }
    }
    public void UpdateHealthUI(int newHealth)
    {
        health = newHealth;
        UpdateUI();
    }


    public float GetCurrentEnemySpeed()
    {
        // przyk³adowa skala: +1 prêdkoœci za ka¿de 100 punktów
        float bonus = (score / 10f) * speedIncreasePer10Points;
        return baseEnemySpeed + bonus;
    }
}


