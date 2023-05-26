using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{

    [SerializeField] Camera sceneCamera;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] float bulletSpeed = 20;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] float startDelay = 0.5f;
    [SerializeField] float bulletLifespan = 2f;
    [SerializeField] int bulletStrength = 10;

    Coroutine firingCoroutine;

    private void Start() {
        InvokeRepeating(nameof(Shoot), startDelay, fireRate);
    }

    void Shoot(){
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        Vector2 fireDirection = mousePosition - (Vector2)sceneCamera.WorldToScreenPoint(bulletSpawnPoint.position);
        
        fireDirection.Normalize();

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = fireDirection * bulletSpeed;
        Destroy(bullet, bulletLifespan);
    }

    public void updateSpeed() {
        bulletSpeed += 2f;
        fireRate -= 0.1f;
    }

    public void updateStrength() {
        bulletStrength += 1;
    }

    public void IncreaseBulletLifeSpan() {
        bulletLifespan += 1;
    }

    public int GetBulletStrength(){
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
