using System.Collections.Generic;
using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    public GameObject agentKeyPrefab;
    public GameObject playerKeyPrefab;
    private GameObject keyInstance;
    private GameObject playerKey; //new
    private GameObject agentKey; //new
    private GameObject lastUsedSpawnPoint;
    private GameObject keyPrefab;
    public List<GameObject> availableKeySpawnPoints = new List<GameObject>();

    public GameObject SpawnKey(bool agent)
    {
        // Kies een willekeurige spawnplaats
        int randomIndex = Random.Range(0, availableKeySpawnPoints.Count);
        Vector3 spawnPosition = availableKeySpawnPoints[randomIndex].transform.position;

        // Zet Y-positie vast
        spawnPosition.y = 0.25f;

        // Instantieer sleutel
        if (agent is true)
        {
            Debug.Log("agentKey spawning");
            keyPrefab = agentKeyPrefab;
            keyInstance = Instantiate(keyPrefab, spawnPosition, Quaternion.identity);//new
            agentKey = keyInstance;//new
        }
        else
        {
            Debug.Log("playerKey spawning");
            keyPrefab = playerKeyPrefab;
            keyInstance = Instantiate(keyPrefab, spawnPosition, Quaternion.identity);//new
            playerKey = keyInstance;//new
        }
        //remove: keyInstance = Instantiate(keyPrefab, spawnPosition, Quaternion.identity);
        //availableKeySpawnPoints.RemoveAt(randomIndex);
        // Zorg dat de sleutel niet zweeft of valt
        Rigidbody rb = keyInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        return keyInstance;
    }

    public void DestroyKey(GameObject gameObject, bool agent)
    {
        if (agent) //new
        { //new
            Destroy(gameObject); //new
            gameObject = null; //new
            keyInstance = null; //new
        } //new
        else //new
        { //new
            Destroy(gameObject); //new
            gameObject = null; //new
            keyInstance = null; //new
        } //new
        // remove other if
    }
}