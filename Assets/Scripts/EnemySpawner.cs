using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabs;  
    [SerializeField] List<GameObject> spawners;
    [SerializeField] float spawnInterval = 2f;  
    [SerializeField] int maxEnemies = 10;
    [SerializeField] float startDelay = 0f;    

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

    public void EnemyDestroyed()
    {
        currentEnemies--;
    }
}