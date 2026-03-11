using UnityEngine;

///<summary>
/// Реализует логику подбираемого объекта: добавление очков, визуальный эффект, уничтожение.
///</summary>
public class Collectible : MonoBehaviour
{
    [Header("Score Settings")]
    [SerializeField] private int scoreValue = 1; // Количество очков за подбор

    [Header("Visual Effect")]
    [SerializeField] private GameObject pickupEffect; // Префаб системы частиц 

    ///<summary>
    /// Вызывается при касании триггера другим коллайдером
    ///</summary>
    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что коснулся именно игрок (по тегу)
        if (other.CompareTag("Player"))
        {
            // Поиск менеджера интерфейса на сцене
            UIManager ui = FindObjectOfType<UIManager>();
            if (ui != null)
            {
                ui.AddScore(scoreValue);
            }

            // Уведомляем менеджер монет о сборе
            if (CoinManager.Instance != null)
            {
                CoinManager.Instance.OnCoinCollected();
            }

            // Создание эффекта подбора, если префаб задан
            if (pickupEffect != null)
            {
                Instantiate(pickupEffect, transform.position, Quaternion.identity);
            }

            // Удаление объекта-монетки
            Destroy(gameObject);
        }
    }
}