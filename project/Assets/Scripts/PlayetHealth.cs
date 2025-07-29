using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 10;
    private int currentHealth;
    [SerializeField] Slider healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.value = currentHealth;  // تحديث الـ health bar

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        SoundManager soundManager = FindAnyObjectByType<SoundManager>();
        if (soundManager != null)
        {
            soundManager.PlaySound("Player died");
        }
        FindAnyObjectByType<MainMenu>().ShowGameOver();



    }

}

