using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]

    public class Wave
    {
        public string waveName;
        public List<EnemyGroup> enemyGroups; 
        public int waveQuota;
        public float spawnInterval;
        public int spawnCount;
    }

    [System.Serializable]

    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount;
        public int spawnCount;
        public GameObject enemyPrefab;
    }

    public List<Wave> waves;
    public int currentWaveCount;

    [Header("Spawner Attributes")]
    float spawnTimer;
    public int enemiesAlive;
    public int maxEnemiesAllowed;
    public bool maxEnemiesReached = false;
    public float waveInteval;

    [Header("Spaner Position")]
    public List<Transform> relativeSpawnPoints;

    Transform player;

    void Start()
    {
      player = FindObjectOfType<Player>().transform;
      CalculateWaveCount();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0)
        {
            StartCoroutine(BeginNextWave());
        }
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    IEnumerator BeginNextWave()
    {
        yield return new WaitForSeconds(waveInteval);
        if(currentWaveCount < waves.Count - 1)
        {
            currentWaveCount++;
            CalculateWaveCount();
        }
    }
    void CalculateWaveCount()
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroups in waves [currentWaveCount].enemyGroups)
        {
            currentWaveCount += enemyGroups.enemyCount;
        }
        waves[currentWaveCount].waveQuota = currentWaveCount;
        Debug.LogWarning(currentWaveQuota);
    }

    void SpawnEnemies()
    {
        if(waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            foreach (var enemyGroups in waves[currentWaveCount].enemyGroups)
            {
                if(enemyGroups.spawnCount < enemyGroups.enemyCount)
                {
                    if(enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }

                    Instantiate(enemyGroups.enemyPrefab, player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position, Quaternion.identity);

                    enemyGroups.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                }
            }
        }
        if(enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }

    public void OneEnemyKilled()
    {
        enemiesAlive--;
    }
}
