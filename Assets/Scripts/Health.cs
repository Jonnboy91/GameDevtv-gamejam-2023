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
    [SerializeField] int experienceForKill = 5;

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
        if(isPlayer && healthBar != null){
            Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);
            healthBar.transform.position = new Vector2(playerScreenPos.x, playerScreenPos.y - 50);
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
            ImaginaryFriendPowerUp.instance.DestroyImaginaryFriend(true);
            ImaginaryFriendPowerUp.instance.DestroyScriptInstance(); // Needs this, since otherwise when starting the new level, it tries to find an instance that does not exist.
            PlayerPrefs.DeleteAll();
            Destroy(healthBar.gameObject);
            DieManager.instance.ReloadLevelWithDelay();
        } else {
                experience.IncreaseExperience(experienceForKill);
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
        if(isPlayer && other.gameObject.tag == "Enemy"){
            TakeDamage(other.gameObject.GetComponent<EnemyDamage>().GetDamage());
            canTakeDamage = false;
        }
        if(isPlayer && other.gameObject.tag == "Boss"){
            TakeDamage(other.gameObject.GetComponent<Boss>().GetDamage());
            canTakeDamage = false;
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
         if(isPlayer && other.gameObject.tag == "Enemy"){
            if (canTakeDamage)
            {
                TakeDamage(other.gameObject.GetComponent<EnemyDamage>().GetDamage());
                canTakeDamage = false;
            }
        }
        if(isPlayer && other.gameObject.tag == "Boss"){
            if (canTakeDamage)
            {
                TakeDamage(other.gameObject.GetComponent<Boss>().GetDamage());
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
        if(gameObject.tag == "Enemy" && (other.gameObject.tag == "Bullet" || other.gameObject.tag == "Imaginary") && player != null){
            TakeDamage(player.GetComponent<PlayerShooting>().GetBulletStrength());
        }
        if(gameObject.tag == "Player" && (other.gameObject.tag == "EnemyBullet" || other.gameObject.tag == "EnemyImaginary") && boss != null){
            TakeDamage(other.gameObject.GetComponent<EnemyDamage>().GetDamage());
        }
    }

    private IEnumerator FlashColor(SpriteRenderer spriteRenderer)
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

}
