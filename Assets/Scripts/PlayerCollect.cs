using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollect : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("banana"))
        {
            MainScoreManager.instance.AddScore(); // Увеличиваем общий счет
            Destroy(other.gameObject); // Удаляем предмет
        }
        else if (other.CompareTag("Minigame2"))
        {
            SceneManager.LoadScene("MonkeyMinigame");
        }
    }
}