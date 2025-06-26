using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject hullBreachPrefab;
    public float spawnInterval = 3f;

    private BoxCollider2D spawnArea;
    public static bool gameFinished = false;

    void Start()
    {
        spawnArea = GetComponent<BoxCollider2D>();
        if (spawnArea == null)
        {
            Debug.LogError("На объекте PlayingField должен быть BoxCollider2D с isTrigger = true!");
            return;
        }

        InvokeRepeating(nameof(SpawnBreach), spawnInterval, spawnInterval);
    }

    void SpawnBreach()
    {
        if (gameFinished) return;

        Vector2 spawnPos = GetRandomPointInBounds(spawnArea.bounds);
        GameObject newBreach = Instantiate(hullBreachPrefab, spawnPos, Quaternion.identity);
        newBreach.SetActive(true);
    }

    Vector2 GetRandomPointInBounds(Bounds bounds)
    {
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        return new Vector2(x, y);
    }

    // 👉 Вызывай это при рестарте игры
    public void RestartGame()
    {
        gameFinished = false;

        // 🔁 Сброс статических флагов
        HullBreachSwitch.ResetGameState();

        CancelInvoke();
        InvokeRepeating(nameof(SpawnBreach), 0f, spawnInterval);

        Debug.Log("🔄 Игра перезапущена");
    }

}
