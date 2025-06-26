using UnityEngine;

public class MonkeyController : MonoBehaviour
{
    public float speed = 5f;
    public bool isPlayer1 = true;

    void Update()
    {
        float moveInput = isPlayer1 ? Input.GetAxisRaw("Horizontal") : Input.GetAxisRaw("HorizontalPlayer2");
        transform.Translate(Vector2.right * moveInput * speed * Time.deltaTime);

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -8f, 8f),
            transform.position.y,
            transform.position.z
        );
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("banana"))
        {
            MinigameScoreManager.instance.AddScore(1); // Исправлено: используем MinigameScoreManager
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("trash"))
        {
            MinigameScoreManager.instance.AddScore(-1); // Исправлено: используем MinigameScoreManager
            Destroy(other.gameObject);
        }
    }
}