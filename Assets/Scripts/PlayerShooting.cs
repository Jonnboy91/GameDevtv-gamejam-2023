using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{

    [SerializeField] Camera sceneCamera;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] float bulletSpeed;
    [SerializeField] float fireRate = 0.3f;
    [SerializeField] float bulletLifespan = 2f;

    private bool fireContinuously = false;
    Coroutine firingCoroutine;

     private void OnEnable()
    {
        // Subscribe to the "Fire" action's events
        InputActionAsset actionAsset = GetComponent<PlayerInput>().actions;
        InputAction fireAction = actionAsset.FindAction("Fire");
        fireAction.started += OnFireStarted;
        fireAction.canceled += OnFireCanceled;
    }

    private void OnDisable()
    {
        // Unsubscribe from the "Fire" action's events
        InputActionAsset actionAsset = GetComponent<PlayerInput>().actions;
        InputAction fireAction = actionAsset.FindAction("Fire");
        fireAction.started -= OnFireStarted;
        fireAction.canceled -= OnFireCanceled;
    }

    void Update()
    {
        if(fireContinuously && firingCoroutine == null){
            firingCoroutine = StartCoroutine(ShootWithDelay());
        } else if(!fireContinuously && firingCoroutine != null){
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    void OnFireStarted(InputAction.CallbackContext context)
    {
        fireContinuously = true;
    }

    void OnFireCanceled(InputAction.CallbackContext context)
    {
        fireContinuously = false;
    }

    void Shoot(){
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        Vector2 fireDirection = mousePosition - (Vector2)sceneCamera.WorldToScreenPoint(bulletSpawnPoint.position);
        
        fireDirection.Normalize();

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = fireDirection * bulletSpeed;
        Destroy(bullet, bulletLifespan);
    }

    IEnumerator ShootWithDelay()
    {
        while (fireContinuously)
        {
            Shoot();
            yield return new WaitForSeconds(fireRate);
        }
    }

}