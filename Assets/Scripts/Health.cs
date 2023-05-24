using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.UI;
using System.Linq;

public class Health : MonoBehaviour
{

    [SerializeField] int healthPoints = 3;
    [SerializeField] bool isPlayer;

    [SerializeField] List<Image> hearts;

    [SerializeField] ParticleSystem dieEffect;

    EnemySpawner spawner;
    private CinemachineImpulseSource impulseSource;

    Experience experience;

    public void Start(){
            spawner = FindObjectOfType<EnemySpawner>();
            impulseSource = GetComponent<CinemachineImpulseSource>();
            experience = FindObjectOfType<Experience>();
        }

    public void TakeDamage(int damage){
        if(isPlayer){
            healthPoints -= damage;
            hearts.Last().enabled = false;
            hearts.RemoveAt(hearts.Count - 1);
            CameraShakeManager.instance.CameraShake(impulseSource);
            if(healthPoints <= 0){
                healthPoints = 0;
                Die();
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        PlayHitEffect();
        if(isPlayer){
            SceneManager.LoadScene("Topdown level 1"); // atm just a restart if you die! Needs to be in LevelManager and just called here (since this is destroyed on death)
        } else {
            experience.IncreaseExperience(1);
            spawner.EnemyDestroyed();
        }
    }

    void PlayHitEffect(){
        if(dieEffect != null){
            ParticleSystem instance = Instantiate(dieEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(isPlayer && other.gameObject.tag == "Enemy"){
            TakeDamage(1);
            // Here we could play a screen shake or something to show the player that the character has been hit
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(gameObject.tag == "Enemy" && other.gameObject.tag == "Bullet"){
            Die();
            // Right now the enemy only has one life, so they die instantly, we could have a separate life for them if we want to (I tested it, but went back to this).
        }
    }
}
