using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MinigameScoreManager : MonoBehaviour
{
    public static MinigameScoreManager instance;
    public TextMeshProUGUI scoreText;
    private int score = 0;
    public int winScore = 20;
    public string mainLevelSceneName = "MainLevel";

    void Awake()
    {
        instance = this;
        if (scoreText != null)
            scoreText.text = "Очки: " + score;
    }

    public void AddScore(int value)
    {
        score += value;
        if (scoreText != null)
            scoreText.text = "Очки: " + score;
        if (score >= winScore)
        {
            SceneManager.LoadScene(mainLevelSceneName);
        }
        else if (score < 0)
        {
            score = 0;
            scoreText.text = "Очки: " + score;
        }
    }
}