using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabs;  
    [SerializeField] List<GameObject> spawners;
    [SerializeField] float spawnInterval = 0.8f;  
    [SerializeField] int maxEnemies = 10;
    [SerializeField] float startDelay = 0.5f; // Can't be 0, since it might try to spawn before getting NavMesh

    private int currentEnemies = 0;


    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), startDelay, spawnInterval);
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

    public void IncreaseSpawnRate(){
        spawnInterval *= 0.8f;
    }

    public void EnemyDestroyed()
    {
        currentEnemies--;
    }
}
