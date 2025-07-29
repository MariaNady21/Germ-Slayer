using UnityEngine;

using UnityEngine.UI;

public class EnemyDeath : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] int maxHealth = 5;
    private int currentHealth;
    private bool isDead = false;
    [Header("UI")]
    [SerializeField] Slider healthSlider;

    [Header("Animation")]
    [SerializeField] Animator animator;  // لازم تربطي Animator من Inspector


    [Header("VFX")]
    [SerializeField] GameObject deathEffectPrefab;

    void Start()
    {
        currentHealth = maxHealth;
        
        UpdateHealthUI();


    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = (float)currentHealth / maxHealth;
        }
    }


    void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        SoundManager soundManager = FindAnyObjectByType<SoundManager>();
        soundManager.PlaySound("deathSound");
        //VFX للموت
        GetComponentInChildren<EnemyHealthBar>()?.Hide();
        ScoreManager.instance.AddScore(5);

        if (GameObject.FindGameObjectsWithTag("Target").Length <= 1) 
        {
            Object.FindAnyObjectByType<MainMenu>().ShowYouWin();
        }


    }
       
    public void TriggerDeathVFX()
    {
        if (deathEffectPrefab != null)
        {
            //VFX للموت
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject, 0.5f); // نديه وقت صغير بعد الـ VFX
    }

}


