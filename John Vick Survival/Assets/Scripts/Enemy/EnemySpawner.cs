using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemyType
    {
        public GameObject prefab;      // enemy prefab to spawn
        public float unlockTime;       // time before this enemy starts spawning
    }

    public List<EnemyType> enemyTypes = new List<EnemyType>(); 
    public Transform player;
    public float spawnRadius = 10f;
    public int baseEnemyCount = 10;
    public float spawnInterval = 0.5f;
    public float waveDelay = 10f;

    float gameTimer = 0f;

    void Start()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerMovement>().transform;
        }

        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        gameTimer += Time.deltaTime;
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            // scale difficulty
            float minutes = gameTimer / 60f;
            int enemyCount = Mathf.FloorToInt((baseEnemyCount + minutes) * 5f + 1f);
            float statMultiplier = 1f + (minutes * 0.5f);

            Debug.Log($"Wave: {enemyCount} enemies (x{statMultiplier:F2} stats)");

            for (int i = 0; i < enemyCount; i++)
            {
                GameObject prefab = GetWeightedEnemyPrefab(gameTimer);
                if (prefab == null) yield break;

                Vector2 spawnPos = GetSpawnPosition();
                GameObject enemy = Instantiate(prefab, spawnPos, Quaternion.identity);

                EnemyStats stats = enemy.GetComponent<EnemyStats>();
                if (stats != null)
                {
                    stats.ApplyScaling(statMultiplier);
                }

                yield return new WaitForSeconds(spawnInterval);
            }

            yield return new WaitForSeconds(waveDelay);
        }
    }

    GameObject GetWeightedEnemyPrefab(float time)
    {
        List<EnemyType> unlocked = new List<EnemyType>();
        foreach (EnemyType type in enemyTypes)
        {
            if (time >= type.unlockTime)
                unlocked.Add(type);
        }

        if (unlocked.Count == 0)
            return null;

        // calculate a weight for each enemy based on how long its been unlocked
        List<float> weights = new List<float>();
        foreach (EnemyType type in unlocked)
        {
            float age = time - type.unlockTime;
            float weight = Mathf.Clamp01(age / 120f); 
            weights.Add(weight);
        }


        float totalWeight = 0f;
        foreach (float w in weights) totalWeight += w;

        float randomValue = Random.value * totalWeight;
        float cumulative = 0f;

        for (int i = 0; i < unlocked.Count; i++)
        {
            cumulative += weights[i];
            if (randomValue <= cumulative)
            {
                return unlocked[i].prefab;
            }
        }

        return unlocked[unlocked.Count - 1].prefab;
    }

    Vector2 GetSpawnPosition()
    {
        Vector2 direction = Random.insideUnitCircle.normalized;
        Vector2 position = (Vector2)player.position + direction * spawnRadius;
        return position;
    }
}
