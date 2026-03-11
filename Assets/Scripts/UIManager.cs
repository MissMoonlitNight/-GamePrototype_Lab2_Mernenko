using UnityEngine;
using UnityEngine.UI;

///<summary>
/// Управление пользовательским интерфейсом: отображение позиции игрока и счёта.
///</summary>
public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Text scoreText;      // Текст для отображения счёта
    [SerializeField] private Text positionText;   // Текст для отображения координат

    [Header("Player Reference")]
    [SerializeField] private Transform player;    // Ссылка на игрока

    // Цвета для визуальной обратной связи
    private Color defaultColor = Color.white;
    private Color warningColor = Color.yellow;

    private int currentScore = 0; // Текущий счёт

    private void Start()
    {
        // Проверка наличия ссылок
        if (positionText == null)
        {
            Debug.LogWarning("UIManager: positionText не назначен в инспекторе!");
        }
        if (player == null)
        {
            Debug.LogWarning("UIManager: player не назначен в инспекторе!");
        }
        if (scoreText == null)
        {
            Debug.LogWarning("UIManager: scoreText не назначен в инспекторе!");
        }

        // Инициализация счёта
        UpdateScoreUI();
    }

    private void Update()
    {
        // Обновление позиции игрока в реальном времени
        UpdatePlayerPosition();
    }

    ///<summary>
    /// Обновляет текст с координатами игрока
    ///</summary>
    private void UpdatePlayerPosition()
    {
        if (player != null && positionText != null)
        {
            Vector3 pos = player.position;

            // Форматируем каждую координату отдельно
            string x = pos.x.ToString("F1");
            string y = pos.y.ToString("F1");
            string z = pos.z.ToString("F1");

            // Отображаем все три координаты
            positionText.text = $"Pos: X:{x} Y:{y} Z:{z}";

            // Визуальная обратная связь: если игрок высоко — меняем цвет
            if (pos.y > 2.0f)
            {
                positionText.color = warningColor;
            }
            else
            {
                positionText.color = defaultColor;
            }
        }
    }

    ///<summary>
    /// Увеличивает счёт на указанное количество очков и обновляет интерфейс.
    ///</summary>
    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreUI();
    }

    ///<summary>
    /// Обновляет текстовое поле счёта
    ///</summary>
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Coins: {currentScore}";
        }
    }

    ///<summary>
    /// Отображает сообщение о победе на экране.
    ///</summary>
    public void ShowVictoryMessage()
    {
        // Создаём временное сообщение о победе
        if (scoreText != null)
        {
            scoreText.text = "ПОБЕДА! ";
            scoreText.color = Color.green;
            scoreText.fontSize = 48;
        }

        // Опционально: можно создать отдельный текстовый объект для победы
        Debug.Log("Победа отображена на экране!");
    }
}