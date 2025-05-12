using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    public GameObject keyPrefab;
    private GameObject KeyInstance;
    public List<GameObject> keySpawnPoints = new List<GameObject>();
    public GameObject SpawnKey()
    {
        int randomIndex = Random.Range(0, keySpawnPoints.Count);
        // Spawn key on random location before instantiating key prefab
        Vector3 SpawnPosition = keySpawnPoints[randomIndex].transform.position;
        KeyInstance = Instantiate(keyPrefab, SpawnPosition, Quaternion.identity);
        return KeyInstance;
    }
}
