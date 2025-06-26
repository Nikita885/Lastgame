using UnityEngine;
using TMPro;

public class MainScoreManager : MonoBehaviour // Переименовал класс, чтобы избежать конфликта
{
    public static MainScoreManager instance; // Исправлено: тип соответствует имени класса
    public TextMeshProUGUI scoreText;
    private int score = 0;

    void Awake()
    {
        instance = this;
        if (scoreText != null)
            scoreText.text = "Собрано легендарных бананов: " + score;
    }

    public void AddScore()
    {
        score++;
        if (scoreText != null)
            scoreText.text = "Собрано легендарных бананов: " + score;
    }
}