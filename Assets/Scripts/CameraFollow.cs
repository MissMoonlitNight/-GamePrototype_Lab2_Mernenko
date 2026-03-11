using UnityEngine;

///<summary>
/// Обеспечивает плавное следование камеры за целевым объектом.
///</summary>
public class CameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform target; // Объект, за которым следует камера

    [Header("Camera Settings")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 2f, -5f); // Смещение относительно цели
    [SerializeField] private float smoothSpeed = 5f; // Скорость интерполяции

    private void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("CameraFollow: target не назначен в инспекторе!");
            return;
        }

        // Желаемая позиция камеры
        Vector3 desiredPosition = target.position + offset;

        // Плавное перемещение 
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;

        // Поворот камеры в сторону 
        transform.LookAt(target);
    }
}