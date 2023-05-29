using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabs;  
    [SerializeField] List<GameObject> spawners;
    [SerializeField] float startSpawnInterval = 1.5f;
    [SerializeField] float spawnInterval = 0f;
    [SerializeField] float finalSpawnInterval = 0.6f; 
    [SerializeField] float spawnIntervalChangeTime = 2f;
    [SerializeField] int maxEnemies = 125;
    [SerializeField] float startDelay = 0.5f; // Can't be 0, since it might try to spawn before getting NavMesh

    private int currentEnemies = 0;

    private void Start()
    {
        spawnInterval = startSpawnInterval;
        InvokeRepeating(nameof(SpawnEnemy), startDelay, spawnInterval);
        StartCoroutine(GraduallyIncreaseSpawnRate());
    }

    private void SpawnEnemy()
    {
        if (currentEnemies >= maxEnemies)
        {
            return;
        }

        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Count);
        GameObject enemyPrefab = enemyPrefabs[randomEnemyIndex];

        int randomSpawnerIndex = Random.Range(0, spawners.Count);
        GameObject spawner = spawners[randomSpawnerIndex];


        GameObject enemy = Instantiate(enemyPrefab, spawner.transform.position, Quaternion.identity);

        currentEnemies++;
    }

    private IEnumerator GraduallyIncreaseSpawnRate()
    {
        while (spawnInterval > finalSpawnInterval)
        {
            spawnInterval -= 0.1f;
            yield return new WaitForSeconds(spawnIntervalChangeTime);
        }
        spawnInterval = finalSpawnInterval;

    }
    public void IncreaseSpawnRate(){
        spawnInterval *= 0.8f;
    }

    public void EnemyDestroyed()
    {
        currentEnemies--;
    }
}
