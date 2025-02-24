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
    Transform player;

    void Start()
    {
      player = FindObjectOfType<Player>().transform;
      CalculateWaveCount();
      SpawnEnemies();  
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if(waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota)
        {
            foreach (var enemyGroups in waves[currentWaveCount].enemyGroups)
            {
                if(enemyGroups.spawnCount < enemyGroups.enemyCount)
                {
                    Vector2 spawnPosition = new Vector2(player.position.x + Random.Range(-10f, 10f), player.position.y + Random.Range(-10f, 10f));
                    Instantiate(enemyGroups.enemyPrefab, spawnPosition, Quaternion.identity);
                    enemyGroups.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                }
            }
        }
    }
}
