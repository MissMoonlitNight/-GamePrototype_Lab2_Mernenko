using UnityEngine;

///<summary>
/// Управление игроком: перемещение через Transform, прыжок через физику.
///</summary>
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 5f;      // Скорость ходьбы
    [SerializeField] private float jumpForce = 7f;       // Сила прыжка

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;      // Слои, считающиеся землёй
    [SerializeField] private Transform groundCheckPoint; // Точка проверки земли
    [SerializeField] private float groundCheckRadius = 0.2f; // Радиус сферы проверки

    private Rigidbody rb;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.freezeRotation = true; // Предотвращаем опрокидывание куба при прыжках
        }
        else
        {
            Debug.LogError("Player не имеет компонента Rigidbody!");
        }
    }

    private void Update()
    {
        // Проверка наличия земли под игроком с помощью сферического каста
        isGrounded = Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, groundLayer);

        // Получение ввода от игрока (стрелки или WASD)
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // Перемещение объекта в мировом пространстве (трансформационный подход)
        transform.Translate(moveDirection * walkSpeed * Time.deltaTime, Space.World);

        // Поворот лица в сторону движения (только если есть направление)
        if (moveDirection != Vector3.zero)
        {
            transform.forward = moveDirection;
        }

        // Обработка прыжка: только при нажатии кнопки и наличии земли под ногами
        if (Input.GetButtonDown("Jump") && isGrounded && rb != null)
        {
            
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}