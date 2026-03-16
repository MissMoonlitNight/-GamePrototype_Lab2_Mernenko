using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [Header("Настройки здоровья")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI healthText; // Для TextMeshPro

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        UpdateHealthUI();
        Debug.Log($"Игрок получил {damage} урона. Осталось здоровья: {currentHealth}");
    }

    void Die()
    {
        Debug.Log("Игрок погиб!");
        // Можно добавить перезагрузку сцены:
        // UnityEngine.SceneManagement.SceneManager.LoadScene(
        //     UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = $" Здоровье: {currentHealth}/{maxHealth}";
        }
    }
}
