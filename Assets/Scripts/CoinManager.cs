using UnityEngine;
using System.Collections.Generic;

///<summary>
/// Менеджер для отслеживания состояния монет на уровне.
/// Выводит сообщение о победе при сборе всех монет.
///</summary>
public class CoinManager : MonoBehaviour
{
    private List<Collectible> coins = new List<Collectible>();
    private int collectedCoins = 0;
    private int totalCoins = 0;

    // Синглтон для доступа из любого скрипта
    public static CoinManager Instance { get; private set; }

    private void Awake()
    {
        // экземпляр существует только один
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Сохраняем при смене сцен
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Находим все монеты на сцене
        Collectible[] allCoins = FindObjectsOfType<Collectible>();
        coins.AddRange(allCoins);
        totalCoins = coins.Count;

        Debug.Log($"[CoinManager] Всего монет на уровне: {totalCoins}");

        // Проверка: если монет нет, выводим предупреждение
        if (totalCoins == 0)
        {
            Debug.LogWarning("[CoinManager] На сцене не найдено ни одной монеты!");
        }
    }

    ///<summary>
    /// Вызывается при сборе монеты.
    ///</summary>
    public void OnCoinCollected()
    {
        collectedCoins++;
        Debug.Log($"[CoinManager] Собрана монета! Счёт: {collectedCoins}/{totalCoins}");

        // Проверка победы
        if (collectedCoins >= totalCoins)
        {
            Victory();
        }
    }

    ///<summary>
    /// Вызывается при победе (все монеты собраны).
    ///</summary>
    private void Victory()
    {
        Debug.Log("========================================");
        Debug.Log("ПОБЕДА! Все монеты собраны!");
        Debug.Log("========================================");


        UIManager ui = FindObjectOfType<UIManager>();
        if (ui != null)
        {
            ui.ShowVictoryMessage();
        }
    }

    ///<summary>
    /// Возвращает текущий прогресс сбора монет.
    ///</summary>
    public string GetProgress()
    {
        return $"{collectedCoins}/{totalCoins}";
    }
}
