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
    private Slider healthSlider;



    [SerializeField] float bossHealth = 10f;
    private float bossCurrentHealth = 0f;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] float fireRate = 2f;
    [SerializeField] float fireRateHoming = 2f;
    [SerializeField] float bulletLifespan = 5f;
    [SerializeField] float homingBulletLifeSpan = 2f;

    [SerializeField] float bossDamage = 10f;

    [SerializeField] int bulletCount = 8;

    private GameObject player;
    private Coroutine bossFiringCoroutine;
    float bossNormalSpeed;

    NavMeshAgent agent;
    private SpriteRenderer spriteRenderer;

    Animator animator;


    private void Awake() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
        GameObject sliderObject = GameObject.FindWithTag("BossHealth");
        healthSlider = sliderObject.GetComponent<Slider>();
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        animator = GetComponent<Animator>();
        bossHealth += player.GetComponent<Health>().getPlayerHealth();
        bossDamage += player.GetComponent<PlayerShooting>().GetBulletStrength();
        bulletSpeed += player.GetComponent<PlayerShooting>().GetBulletSpeed();
        fireRate -= player.GetComponent<PlayerShooting>().GetBulletFireRate();
        bulletLifespan += player.GetComponent<PlayerShooting>().GetBulletLifeSpan();
        agent.speed = player.GetComponent<PlayerMovement>().GetSpeed() - 5f;
        bossNormalSpeed = agent.speed;
        bossCurrentHealth = bossHealth;
        healthSlider.maxValue = bossHealth;
        updateHealth();
        StartCoroutine(ActivateSpecialAttack());
        ImaginaryFriendPowerUp.instance.ActivatePowerup();
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
            healthSlider.transform.position = new Vector2(playerScreenPos.x, playerScreenPos.y - 80);
        }
        if(agent.speed != 0){
            animator.SetBool("isMoving", true);
        }else{
            animator.SetBool("isMoving", false);
        }
    }

    void updateHealth(){
        healthSlider.maxValue = bossHealth;
        healthSlider.value = bossCurrentHealth;
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
        float startingAngle = Random.Range(0f, 360f); 
        float currentAngle = startingAngle;

        for (int i = 0; i < bulletCount; i++)
        {
            Vector2 direction = Quaternion.Euler(0f, 0f, currentAngle) * Vector2.up;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyDamage>().setEnemyDamage(bossDamage);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            
            currentAngle += angleStep;
            currentAngle %= 360f;

            Destroy(bullet, bulletLifespan);
        }
    }

    void Shoot360Special(){

        float angleStep = 360f / bulletCount;
        float startingAngle = Random.Range(0f, 360f); 
        float currentAngle = startingAngle;

        for (int i = 0; i < bulletCount; i++)
        {
            Vector2 direction = Quaternion.Euler(0f, 0f, currentAngle) * Vector2.up;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<EnemyDamage>().setEnemyDamage(bossDamage - 5f);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * (bulletSpeed + 5f);
            
            currentAngle += angleStep;
            currentAngle %= 360f;

            Destroy(bullet, 2.5f);
        }
    }

    void HomingBullet(){

        if (player != null)
        {
            GameObject bullet = Instantiate(homingBulletPrefab, transform.position, Quaternion.identity);
            Destroy(bullet, homingBulletLifeSpan);
        }
    }

    IEnumerator ActivateSpecialAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3, 6));
            InvokeRepeating(nameof(Shoot360Special), 1, 0.6f);
            StartCoroutine(FlashBossColorSpecialAttack());
            agent.speed = 0;
            yield return new WaitForSeconds(Random.Range(3, 6)); // Keep the special attack active for 2 seconds
            CancelInvoke(nameof(Shoot360Special));
            agent.speed = bossNormalSpeed;
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
        healthSlider.enabled = false;
        Destroy(healthSlider.gameObject);
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

    private IEnumerator FlashBossColorSpecialAttack()
    {
        spriteRenderer.color = Color.blue;
        Color changedColor;
        if(ColorUtility.TryParseHtmlString("#FFE500", out changedColor)){
            yield return new WaitForSeconds(0.5f);
            spriteRenderer.color = changedColor;
        }else{
             yield return new WaitForSeconds(0.5f);
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
