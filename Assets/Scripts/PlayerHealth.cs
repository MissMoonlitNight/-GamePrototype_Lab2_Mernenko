using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // <-- Важно!

public class PlayerHealth : MonoBehaviour
{
    [Header("Настройки здоровья")]
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    [Header("UI")]
    [SerializeField] private Text healthText;

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
        Debug.Log("ИГРОК ПОГИБ! Перезагрузка сцены...)"); // <-- Одна строка в консоль
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text = $"Health: {currentHealth}/{maxHealth}";
        }
    }
}


