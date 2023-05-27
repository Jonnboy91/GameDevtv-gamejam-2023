using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Boss : MonoBehaviour
{
    
    
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject homingBulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] ParticleSystem dieEffect;
    [SerializeField] Slider healthBar;


    [SerializeField] float bossHealth = 10f;
    private float bossCurrentHealth = 0f;
    private float bulletSpeed;
    private float fireRate = 2f;
    private float fireRateHoming = 2f;
    private float bulletLifespan = 1f;

    private int bulletCount = 8;

    private GameObject player;
    private Coroutine bossFiringCoroutine;

    NavMeshAgent agent;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
    }
    void Start()
    {
        bossHealth += player.GetComponent<Health>().getPlayerHealth();
        bulletSpeed = player.GetComponent<PlayerShooting>().GetBulletSpeed();
        fireRate -= player.GetComponent<PlayerShooting>().GetBulletFireRate();
        bulletLifespan += player.GetComponent<PlayerShooting>().GetBulletLifeSpan();
        agent.speed = player.GetComponent<PlayerMovement>().GetSpeed() - 5f;
        bossCurrentHealth = bossHealth;
        healthBar.maxValue = bossHealth;
        updateHealth();
        InvokeRepeating(nameof(Shoot360), fireRate, fireRate);
        InvokeRepeating(nameof(HomingBullet), fireRateHoming, fireRateHoming);
    }

    void FixedUpdate() {
        if(player != null && gameObject != null){
            SetAgentPosition();
            FlipEnemy();         
        }
        if(gameObject != null){
            Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);
            healthBar.transform.position = new Vector2(playerScreenPos.x, playerScreenPos.y - 80);
        }
        
    }

    void updateHealth(){
        healthBar.maxValue = bossHealth;
        healthBar.value = bossCurrentHealth;
    }

    void SetAgentPosition()
    {
        agent.SetDestination(player.transform.position);
    }

    void FlipEnemy(){
        spriteRenderer.flipX = player.transform.position.x < gameObject.transform.position.x;
    }

    void Shoot360(){

        float angleStep = 360f / bulletCount;
        float currentAngle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            // Calculate the direction of the bullet
            Vector2 direction = Quaternion.Euler(0f, 0f, currentAngle) * Vector2.up;

            // Instantiate bullet prefab and set its position and direction
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

            currentAngle += angleStep;

            Destroy(bullet, bulletLifespan);
        }
    }

    void HomingBullet(){

        if (player != null)
        {
            GameObject bullet = Instantiate(homingBulletPrefab, transform.position, Quaternion.identity);
            Destroy(bullet, bulletLifespan);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Bullet" && player != null){
            TakeDamage(player.GetComponent<PlayerShooting>().GetBulletStrength());
            // Right now the enemy only has one life, so they die instantly, we could have a separate life for them if we want to (I tested it, but went back to this).
        }
    }

    public void TakeDamage(float damage){
            StartCoroutine(FlashColor());
            updateHealth();
            bossCurrentHealth -= damage;
            if(bossCurrentHealth <= 0){
                bossCurrentHealth = 0;
                Die();
            }
        }

    private void Die()
    {
        healthBar.enabled = false;
        Destroy(healthBar.gameObject);
        PlayHitEffect();
        Destroy(gameObject);
        DieManager.instance.WinGame();
    }

    private IEnumerator FlashColor()
    {
        // Change the sprite color to white
        spriteRenderer.color = Color.red;

        // Wait for 0.1 seconds
        yield return new WaitForSeconds(0.1f);

        // Reset the sprite color to its original value
        spriteRenderer.color = Color.white;
    }

    void PlayHitEffect(){
        if(dieEffect != null){
            ParticleSystem instance = Instantiate(dieEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

}
