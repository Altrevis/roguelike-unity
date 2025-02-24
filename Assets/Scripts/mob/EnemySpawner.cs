using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("⚔️ Paramètres de Spawn")]
    public GameObject enemyPrefab; // Associe ton prefab MobBase ici
    public List<Transform> spawnPoints; // Liste des positions de spawn
    public int enemiesPerWave = 5; // Nombre d'ennemis par vague
    public float timeBetweenSpawns = 1.5f; // Délai entre chaque spawn
    public float timeBetweenWaves = 5f; // Délai entre chaque vague

    private int waveNumber = 0;
    
    void Start()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("❌ Aucun prefab d'ennemi assigné !");
            return;
        }

        if (spawnPoints.Count == 0)
        {
            Debug.LogError("❌ Aucun point de spawn défini !");
            return;
        }

        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            waveNumber++;
            Debug.Log("🌊 Début de la vague " + waveNumber);

            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(timeBetweenSpawns);
            }

            Debug.Log("⏳ Pause avant la prochaine vague...");
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    void SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
        Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
        Debug.Log("✅ Ennemi spawné à " + spawnPoint.position);
    }
}
