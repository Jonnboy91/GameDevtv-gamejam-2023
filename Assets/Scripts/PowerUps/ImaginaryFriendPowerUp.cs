using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImaginaryFriendPowerUp : MonoBehaviour
{
    public static ImaginaryFriendPowerUp instance;

    [SerializeField] List<GameObject> imaginaryFriendPrefabs;
    [SerializeField] List<GameObject> bossImaginaryFriendPrefabs; 

    public float rotationSpeed = 60f; 
    public float spawnDistance = 5f;
    public float spawnBossDistance = 20f;  

    private GameObject player;
    private GameObject boss;

    private GameObject imaginaryFriend; 
    private GameObject bossImaginaryFriend;

            


    private void Awake() {
        if(instance == null){
            instance = this;
        }
        player = GameObject.FindGameObjectWithTag("Player");
        if(SceneManager.GetActiveScene().name == "Boss"){
                boss = GameObject.FindGameObjectWithTag("Boss");
        }
    }

    public void DestroyScriptInstance()
    {
        // Done because sometimes when you die, it might have gotten inside of the line 44 and tries to rotate the ghost around the player and neither exist in the next scene.
        Destroy(this);
    }

    private void LateUpdate()
    {
        if (imaginaryFriend != null)
        {
            imaginaryFriend.transform.RotateAround(player.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);

            Vector3 desiredPosition = player.transform.position + (imaginaryFriend.transform.position - player.transform.position).normalized * spawnDistance;
            
            imaginaryFriend.transform.position = desiredPosition;
        }
        if (bossImaginaryFriend != null && boss != null)
        {
            bossImaginaryFriend.transform.RotateAround(boss.transform.position, Vector3.forward, rotationSpeed * Time.deltaTime);

            Vector3 desiredBossPosition = boss.transform.position + (bossImaginaryFriend.transform.position - boss.transform.position).normalized * spawnBossDistance;
            
            bossImaginaryFriend.transform.position = desiredBossPosition;
        } else if(boss == null){
            Destroy(bossImaginaryFriend);
        }
    }

    public void DestroyImaginaryFriend(bool isPlayerFriend){
        if(isPlayerFriend){
            Destroy(imaginaryFriend);
        }else{
            Destroy(bossImaginaryFriend);
        }
    }

    public void ActivatePowerup()
    {
        if (imaginaryFriend == null)
        {
            Vector3 spawnPosition = player.transform.position + (Vector3.right * spawnDistance);
            int randomIndex = Random.Range(0, imaginaryFriendPrefabs.Count);
            imaginaryFriend = Instantiate(imaginaryFriendPrefabs[randomIndex], spawnPosition, Quaternion.identity);
        }
        if(imaginaryFriend != null && bossImaginaryFriend == null){
            boss = GameObject.FindGameObjectWithTag("Boss");
        }
        if (imaginaryFriend != null && bossImaginaryFriend == null && boss != null)
        {
            Vector3 spawnBossPosition = boss.transform.position + (Vector3.right * spawnBossDistance);
            int randomIndex = Random.Range(0, imaginaryFriendPrefabs.Count);
            bossImaginaryFriend = Instantiate(bossImaginaryFriendPrefabs[randomIndex], spawnBossPosition, Quaternion.identity);
        }
    }
}
