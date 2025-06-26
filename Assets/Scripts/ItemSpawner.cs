using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject bananaPrefab;
    public GameObject trashPrefab;
    public float spawnRate = 1f;
    private float spawnTimer;

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnRate)
        {
            // 70% шанс спавна банана, 30% — мусора
            GameObject prefab = Random.value < 0.7f ? bananaPrefab : trashPrefab;
            Vector2 pos = new Vector2(Random.Range(-8f, 8f), 6f);
            Instantiate(prefab, pos, Quaternion.identity);
            spawnTimer = 0;
        }
    }
}