using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;


public class Health : MonoBehaviour
{

    [SerializeField] float maxHealthPoints = 300f;
    private float currentHealth = 0f;
    [SerializeField] float enemyHealth = 30f;
    [SerializeField] bool isPlayer;

    [SerializeField] Slider healthBar;

    [SerializeField] ParticleSystem dieEffect;

    [SerializeField] float damageDelay = 1f;

    [SerializeField] float killDistance = 150f;

    EnemySpawner spawner;
    private CinemachineImpulseSource impulseSource;

    Experience experience;

    private GameObject player;
    private AudioSource _audioSource;

    private bool canTakeDamage = true;
    
    private float damageTimer = 0f;

    ColorGrading colorGrading;
    Vignette vignette;
    [SerializeField] PostProcessVolume _postProcessVolume;


    private void Awake() {
        spawner = FindObjectOfType<EnemySpawner>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
        if(SceneManager.GetActiveScene().name == "Level 1"){
                experience = FindObjectOfType<Experience>();
            }
        player = GameObject.FindGameObjectWithTag("Player");
        if(isPlayer){
        colorGrading = _postProcessVolume.profile.GetSetting<ColorGrading>();
        vignette = _postProcessVolume.profile.GetSetting<Vignette>();
        }
    }

    private void Start() {
        _audioSource = GetComponent<AudioSource>(); 
        if(isPlayer){
            currentHealth = maxHealthPoints;
            healthBar.maxValue = maxHealthPoints;
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
        if(!isPlayer){
            KillIfTooFar();
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
        } else if(gameObject != null){
            _audioSource.Play();
            enemyHealth -= damage;
            DamageTextManager.Instance.ShowDamageText(gameObject, damage);
            if(enemyHealth <= 0){
                enemyHealth = 0;
                Die();
            }
        }
    }

    void updateHealth(){
        healthBar.maxValue = maxHealthPoints;
        healthBar.value = currentHealth;
        float saturationValue = currentHealth / maxHealthPoints * 100f - 100f;
        float vignetteValue = 1 - (currentHealth / maxHealthPoints);
        if (_postProcessVolume.profile.TryGetSettings(out colorGrading))
    {
        colorGrading.saturation.value = saturationValue;
    }

    if (_postProcessVolume.profile.TryGetSettings(out vignette))
    {
        vignette.intensity.value = vignetteValue;
    }
    }

    private void Die()
    {
        if(isPlayer){
            CameraShakeManager.instance.DestroyScriptInstance();
            ImaginaryFriendPowerUp.instance.DestroyImaginaryFriend(true);
            ImaginaryFriendPowerUp.instance.DestroyScriptInstance(); // Needs this, since otherwise when starting the new level, it tries to find an instance that does not exist.
            TwinPowerUp.instance.DestroyScriptInstance();
            Destroy(healthBar.gameObject);
            if(SceneManager.GetActiveScene().name == "Level 1"){
                PlayerPrefs.DeleteAll();
            }
            DieManager.instance.ReloadLevelWithDelay();
        } else {
                experience.IncreaseExperience(5);
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

    private void KillIfTooFar(){
        if(!isPlayer && player != null){
            float distance = Vector2.Distance(transform.position, player.transform.position);
            if(distance > killDistance){
                spawner.EnemyDestroyed();
                Destroy(gameObject);
            }
        }
    }

    public void addHealth(){
        float addAmount = maxHealthPoints * 1.2f - maxHealthPoints;
        maxHealthPoints *= 1.2f;
        if(currentHealth != maxHealthPoints){
            currentHealth += addAmount;
        }
        updateHealth();
    }

    public void healUp(){
        if(currentHealth < maxHealthPoints){
            if(maxHealthPoints - currentHealth < 20){
                currentHealth = maxHealthPoints;
            }else{
                currentHealth += 20;
            }
            updateHealth();
        }
    }

    public float getPlayerHealth(){
        return maxHealthPoints;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(gameObject.tag == "Enemy" && (other.gameObject.tag == "Bullet" || other.gameObject.tag == "Imaginary") && player != null){
            TakeDamage(player.GetComponent<PlayerShooting>().GetBulletStrength());
        }
        if(gameObject.tag == "Player" && (other.gameObject.tag == "EnemyBullet" || other.gameObject.tag == "EnemyImaginary") && GameObject.FindGameObjectWithTag("Boss") != null){
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
