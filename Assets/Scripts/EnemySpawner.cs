using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemyPrefabs;  
    [SerializeField] float spawnInterval = 2f;  
    [SerializeField] int maxEnemies = 10;
    [SerializeField] float startDelay = 0f;
    [SerializeField] int spawnerIndex; 
    

    private int currentEnemies = 0;


    private void Start()
    {
        Invoke(nameof(StartSpawning), startDelay);
    }


    private void StartSpawning()
    {
        InvokeRepeating(nameof(StartSpawning), 0f, spawnInterval);
    }

    private void SpawnEnemy()
    {
        if (currentEnemies >= maxEnemies)
        {
            return;
        }

        int randomIndex = Random.Range(0, enemyPrefabs.Count);
        GameObject enemyPrefab = enemyPrefabs[randomIndex];

        GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        currentEnemies++;

        if (currentEnemies >= maxEnemies)
        {
            // Maximum number of enemies reached, stop spawning
            CancelInvoke("SpawnEnemy");
        }
    }

    public void EnemyDestroyed()
    {
        currentEnemies--;

        if (currentEnemies < maxEnemies)
        {
            // Resume spawning if there is room for more enemies
            Invoke("SpawnEnemy", spawnInterval);
        }
    }
}
