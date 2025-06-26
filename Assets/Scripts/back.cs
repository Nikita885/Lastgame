using UnityEngine;

public class Background : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main; // Находим главную камеру
    }

    void LateUpdate()
    {
        // Привязываем позицию фона к позиции камеры, сохраняя Z координату фона
        transform.position = new Vector3(
            mainCamera.transform.position.x,
            mainCamera.transform.position.y,
            transform.position.z
        );
    }
}