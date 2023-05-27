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

    private float bossDamage = 10f;

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
        bossDamage += player.GetComponent<PlayerShooting>().GetBulletStrength();
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
            Vector2 direction = Quaternion.Euler(0f, 0f, currentAngle) * Vector2.up;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyDamage>().setEnemyDamage(bossDamage);
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
        if((other.gameObject.tag == "Bullet"  || other.gameObject.tag == "Imaginary") && player != null){
            TakeDamage(player.GetComponent<PlayerShooting>().GetBulletStrength());
        }
    }

    public void TakeDamage(float damage){
            StartCoroutine(FlashBossColor());
            updateHealth();
            bossCurrentHealth -= damage;
            if(bossCurrentHealth <= 0){
                bossCurrentHealth = 0;
                Die();
            }
    }

    public float GetDamage(){
        return bossDamage;
    }

    private void Die()
    {
        healthBar.enabled = false;
        Destroy(healthBar.gameObject);
        PlayHitEffect();
        ImaginaryFriendPowerUp.instance.DestroyImaginaryFriend(false);
        Destroy(gameObject);
        DieManager.instance.WinGame();
    }

    private IEnumerator FlashBossColor()
    {
        spriteRenderer.color = Color.red;
        Color changedColor;
        if(ColorUtility.TryParseHtmlString("#FFE500", out changedColor)){
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = changedColor;
        }else{
             yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
        }
    }

    void PlayHitEffect(){
        if(dieEffect != null){
            ParticleSystem instance = Instantiate(dieEffect, transform.position, Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

}
