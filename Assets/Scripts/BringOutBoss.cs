using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringOutBoss : MonoBehaviour
{
    [SerializeField] GameObject bossPrefab;  
    [SerializeField] GameObject spawner;
    [SerializeField] ParticleSystem spawnparticles;
    [SerializeField] float startDelay = 0.5f; // Can't be 0, since it might try to spawn before getting NavMesh



    private void Start()
    {
        StartCoroutine(SpawnBoss());
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(bossPrefab, spawner.transform.position, Quaternion.identity);
    }

    void PlayHitEffect(){
        if(spawnparticles != null){
            ParticleSystem instance = Instantiate(spawnparticles, spawner.transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    private IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(startDelay);
        PlayHitEffect();
        yield return new WaitForSeconds(startDelay);
        SpawnEnemy();
    }
}
