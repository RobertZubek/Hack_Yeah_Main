using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth = 100;

    private UIManager uiManager;

    void Start()
    {
        currentHealth = maxHealth;

        uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.UpdateHealthUI(currentHealth); // poka¿ pocz¹tkowe HP
        }
    }

    public void Heal(int amount)
    {
        if(!(currentHealth +amount >= maxHealth))
            currentHealth += amount;
        if (uiManager != null)
        {
            uiManager.UpdateHealthUI(currentHealth);
        }
        Debug.Log("Zebrano potion! HP: " + currentHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        if (uiManager != null)
        {
            uiManager.UpdateHealthUI(currentHealth);
        }
        if (currentHealth <= 0)
        {
            Die();
        }
        

    }
    private void Die()
    {
        Debug.Log("Player died!");
        if (uiManager != null)
        {
            uiManager.GameOver();
        }
    }
}

