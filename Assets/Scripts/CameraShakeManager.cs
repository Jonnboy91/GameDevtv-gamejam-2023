using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShakeManager : MonoBehaviour
{
    public static CameraShakeManager instance;

    [SerializeField] private float globalShakeForce = 1f;

    private void Awake() {
        if(instance == null){
            instance = this;
        }
    }

    public void DestroyScriptInstance()
    {
        // Done because sometimes when you die, it might have gotten inside of the line 44 and tries to rotate the ghost around the player and neither exist in the next scene.
        Destroy(this);
    }

    public void CameraShake(CinemachineImpulseSource impulseSource){
        impulseSource.GenerateImpulseWithForce(globalShakeForce);
    }
}
