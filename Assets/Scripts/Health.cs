using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    [SerializeField] float healthPoints = 100f;
    private float currentHealth = 0f;
    [SerializeField] float enemyHealth = 30f;
    [SerializeField] bool isPlayer;

    [SerializeField] Slider healthBar;

    [SerializeField] ParticleSystem dieEffect;

    [SerializeField] float damageDelay = 1f;

    EnemySpawner spawner;
    private CinemachineImpulseSource impulseSource;

    Experience experience;

    private GameObject player;
    private GameObject boss;

    private bool canTakeDamage = true;
    
    private float damageTimer = 0f;


    private void Awake() {
        spawner = FindObjectOfType<EnemySpawner>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        if(SceneManager.GetActiveScene().name == "Level 1"){
                experience = FindObjectOfType<Experience>();
            }
        if(SceneManager.GetActiveScene().name == "Boss"){
            boss = GameObject.FindGameObjectWithTag("Boss");
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start() {
        if(isPlayer){
            currentHealth = healthPoints;
            healthBar.maxValue = healthPoints;
            updateHealth();
        }
    }

    private void Update()
    {
        if(isPlayer){
            if (!canTakeDamage)
            {
                damageTimer += Time.deltaTime;
                if (damageTimer >= damageDelay)
                {
                    canTakeDamage = true;
                    damageTimer = 0f;
                }
            }
        }
    }

    private void FixedUpdate() {
        if(isPlayer){
            Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);
            healthBar.transform.position = new Vector2(playerScreenPos.x, playerScreenPos.y - 80);
        }
    }

    public void TakeDamage(float damage){
        StartCoroutine(FlashColor(gameObject.GetComponent<SpriteRenderer>()));
        if(isPlayer){
            currentHealth -= damage;
            updateHealth();
            CameraShakeManager.instance.CameraShake(impulseSource);
            if(currentHealth <= 0){
                currentHealth = 0;
                Die();
            }
        } else{
            enemyHealth -= damage;
            DamageTextManager.Instance.ShowDamageText(gameObject, damage);
            if(enemyHealth <= 0){
                enemyHealth = 0;
                Die();
            }
        }
    }

    void updateHealth(){
        healthBar.maxValue = healthPoints;
        healthBar.value = currentHealth;
    }

    private void Die()
    {
        if(isPlayer){
            CameraShakeManager.instance.DestroyScriptInstance();
            ImaginaryFriendPowerUp.instance.DestroyScriptInstance(); // Needs this, since otherwise when starting the new level, it tries to find an instance that does not exist.
            PlayerPrefs.DeleteAll(); // TODO: Deletes all powerUps if you die! and starts the level again! Might want to have a gameOver screen and play again instead of straightaway going to level 1!
            Destroy(healthBar.gameObject);
            DieManager.instance.ReloadLevelWithDelay();
        } else {
                experience.IncreaseExperience(1);
                spawner.EnemyDestroyed();
        }
        Destroy(gameObject);
        PlayHitEffect();
    }

    void PlayHitEffect(){
        if(dieEffect != null){
            ParticleSystem instance = Instantiate(dieEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(isPlayer && (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")){
            TakeDamage(10);
            canTakeDamage = false;
            // Here we could play a screen shake or something to show the player that the character has been hit
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
         if(isPlayer && (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")){
            if (canTakeDamage)
            {
                TakeDamage(10);
                canTakeDamage = false;
            }
        }
    }

    public void addHealth(){
        float addAmount = healthPoints * 1.2f - healthPoints;
        healthPoints *= 1.2f;
        if(currentHealth != healthPoints){
            currentHealth += addAmount;
        }
        updateHealth();
    }

    public float getPlayerHealth(){
        return healthPoints;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(gameObject.tag == "Enemy" && other.gameObject.tag == "Bullet" && player != null){
            TakeDamage(player.GetComponent<PlayerShooting>().GetBulletStrength());
        }
        if(gameObject.tag == "Player" && other.gameObject.tag == "EnemyBullet" && boss != null){
            TakeDamage(10);
        }
    }

    private IEnumerator FlashColor(SpriteRenderer spriteRenderer)
    {
        // Change the sprite color to white
        spriteRenderer.color = Color.red;

        // Wait for 0.1 seconds
        yield return new WaitForSeconds(0.1f);

        // Reset the sprite color to its original value
        spriteRenderer.color = Color.white;
    }

}
