using System.Collections.Generic;
using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    public GameObject keyPrefab;
    private GameObject keyInstance;
    public List<GameObject> keySpawnPoints = new List<GameObject>();

    public GameObject SpawnKey()
    {
        // Kies een willekeurige spawnplaats
        int randomIndex = Random.Range(0, keySpawnPoints.Count);
        Vector3 spawnPosition = keySpawnPoints[randomIndex].transform.position;

        // Zet Y-positie vast
        spawnPosition.y = 0.25f;

        // Instantieer sleutel
        keyInstance = Instantiate(keyPrefab, spawnPosition, Quaternion.identity);

        // Zorg dat de sleutel niet zweeft of valt
        Rigidbody rb = keyInstance.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }

        return keyInstance;
    }

    public void DestroyKey()
    {
        if (keyInstance != null)
        {
            Destroy(keyInstance);
            keyInstance = null;
        }
    }
}
