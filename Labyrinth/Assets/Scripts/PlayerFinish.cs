using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinish : MonoBehaviour
{
    [Header("Environment")]
    public GameObject playerDoor;
    public KeySpawner keySpawnerScript;
    public bool enableDoorSystem = true;
    private GameObject playerKey;
    private void Start()
    {
        playerKey = keySpawnerScript.SpawnKey(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (enableDoorSystem is true)
        {
            if (other.gameObject.CompareTag("playerKey"))
            {
                Debug.Log("playerKey touched door");
                // Insert end of competition code here
            }
        }

    }
}
