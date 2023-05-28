using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class PlayerShooting : MonoBehaviour
{

    [SerializeField] Camera sceneCamera;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] float bulletSpeed = 20;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] float startDelay = 0.25f;
    [SerializeField] float bulletLifespan = 1.75f;
    [SerializeField] float bulletStrength = 15;

    private List<GameObject> maxBulletCount = new List<GameObject>();


    Coroutine firingCoroutine;

    private void Awake() {
        maxBulletCount.Capacity = 20;
    }
    private void Start() {
        InvokeRepeating(nameof(Shoot), startDelay, fireRate);
    }

    void Shoot(){
        if(maxBulletCount.Count < maxBulletCount.Capacity){
            Vector2 mousePosition = Mouse.current.position.ReadValue();

            Vector2 fireDirection = mousePosition - (Vector2)sceneCamera.WorldToScreenPoint(bulletSpawnPoint.position);
            
            fireDirection.Normalize();

            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = fireDirection * bulletSpeed;
            maxBulletCount.Add(bullet);
            Destroy(bullet, bulletLifespan);
        }else if (maxBulletCount.Count != 0) {
            Destroy(maxBulletCount.First());
            maxBulletCount.RemoveAt(0);
            Shoot();
        }
        
    }

    public void updateSpeed() {
        bulletSpeed *= 1.10f;
        fireRate *= 0.95f;
    }

    public void updateStrength() {
        bulletStrength *= 1.10f;
    }

    public void IncreaseBulletLifeSpan() {
        bulletLifespan *= 1.10f;
    }

    public float GetBulletStrength(){
        return bulletStrength;
    }

    public float GetBulletLifeSpan(){
        return bulletLifespan;
    }

    public float GetBulletSpeed(){
        return bulletSpeed;
    }
    public float GetBulletFireRate(){
        return fireRate;
    }

}
