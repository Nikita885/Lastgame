using UnityEngine;

public class HullBreachSwitch : MonoBehaviour
{
    public GameObject hullBreach_1;
    public GameObject hullBreach_2;

    private bool isSwitched = false;
    private float timeSinceSpawn;

    private static int totalFixed = 0;
    private static bool gameEnded = false;

    void Start()
    {
        if (hullBreach_1 == null || hullBreach_2 == null)
        {
            Debug.LogError("Не назначены hullBreach_1 или hullBreach_2!");
            return;
        }

        hullBreach_1.SetActive(true);
        hullBreach_2.SetActive(false);
        timeSinceSpawn = Time.time;
    }

    void Update()
    {
        if (!isSwitched && !gameEnded)
        {
            if (Time.time - timeSinceSpawn >= 5f)
            {
                Debug.Log("ПРОИГРЫШ — протечка не была починена за 5 секунд.");
                LoseGame();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isSwitched && other.CompareTag("Player") && !gameEnded)
        {
            hullBreach_1.SetActive(false);
            hullBreach_2.SetActive(true);
            isSwitched = true;
            totalFixed++;

            Debug.Log($"Починено протечек: {totalFixed}");

            if (totalFixed >= 10)
            {
                Debug.Log("ПОБЕДА — все 10 протечек успешно заделаны!");
                gameEnded = true;
                RandomSpawner.gameFinished = true;

                // ⬅️ Сбросим всё, что активировал портал
                PortalTrigger portal = FindObjectOfType<PortalTrigger>();
                if (portal != null)
                {
                    portal.ResetPortalState();
                }
            }

        }
    }

    private void LoseGame()
    {
        gameEnded = true;
        totalFixed = 0;
        RandomSpawner.gameFinished = true;

        HullBreachSwitch[] allBreaches = FindObjectsOfType<HullBreachSwitch>();
        foreach (var breach in allBreaches)
        {
            Destroy(breach.gameObject);
        }

        // Запускаем рестарт через живой объект
        RandomSpawner spawner = FindObjectOfType<RandomSpawner>();
        if (spawner != null)
        {
            spawner.Invoke(nameof(spawner.RestartGame), 3f);
        }
    }


    private void RestartGame()
    {
        Debug.Log("🔄 Игра перезапущена");
        gameEnded = false;
        RandomSpawner.gameFinished = false;

        // Ищем объект со спавнером и вызываем его рестарт
        RandomSpawner spawner = FindObjectOfType<RandomSpawner>();
        if (spawner != null)
        {
            spawner.CancelInvoke();
            spawner.InvokeRepeating("SpawnBreach", 0f, spawner.spawnInterval);
        }
    }
    public static void ResetGameState()
    {
        totalFixed = 0;
        gameEnded = false;
    }

}
