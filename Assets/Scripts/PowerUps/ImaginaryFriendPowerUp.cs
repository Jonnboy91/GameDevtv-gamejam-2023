using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImaginaryFriendPowerUp : MonoBehaviour
{

   /*  Might need to set it up in one PowerUp script that's on the player,
   since this way it's cleaner and only one instance is created,
   BUT you need to have this "manager" script in all of the scenes,
   where it COULD be used by the player and it will create the instance,
   not sure how taxing that is for the system. 
   AND this would have to be for all the 9 different powerups 
   (obviously on childhood there would be none, adolesence there 
   would be 3 adulthood 6 and final boss would have the 9) */

    public static ImaginaryFriendPowerUp instance;

    [SerializeField] GameObject imaginaryFriendPrefab;
    [SerializeField] GameObject bossImaginaryFriendPrefab; 
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

    public void ActivatePowerup()
    {
        if (imaginaryFriend == null)
        {
            Vector3 spawnPosition = player.transform.position + (Vector3.right * spawnDistance);
            imaginaryFriend = Instantiate(imaginaryFriendPrefab, spawnPosition, Quaternion.identity);
        }
        if (bossImaginaryFriend == null && boss != null)
        {
            Vector3 spawnBossPosition = boss.transform.position + (Vector3.right * spawnBossDistance);
            bossImaginaryFriend = Instantiate(bossImaginaryFriendPrefab, spawnBossPosition, Quaternion.identity);
        }
    }
}
