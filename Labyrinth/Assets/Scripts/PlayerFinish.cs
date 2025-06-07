using UnityEngine;

public class PlayerFinish : MonoBehaviour
{
    [Header("Environment")]
    public GameObject PlayerDoor;
    public KeySpawner keySpawnerScript;
    public GameObject Player; // Player object

    [Header("Instellingen")]
    public bool enableDoorSystem = true;


    private Vector3 startPosition;       // Startpositie speler
    private GameObject playerKey;          // Huidige sleutel
    private bool keyCollected = false;     // Heeft speler sleutel

    private void Start()
    {
        if (Player == null)
        {
            Debug.LogError("Player niet ingesteld in PlayerFinish script!");
            return;
        }


        // Spawn een nieuwe sleutel voor de speler
        playerKey = keySpawnerScript.SpawnKey(false);
        Debug.Log($"Sleutel gespawned: {playerKey}");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!enableDoorSystem) return;

        if (other.CompareTag("playerKey"))
        {
            Debug.Log("Sleutel raakt deur");
            Player.transform.position = new Vector3(0f, 0.34f, 0f);

            // Optioneel: reset rotatie speler ook, als nodig
            // Player.transform.rotation = Quaternion.identity;

            // Verwijder oude sleutel
            keySpawnerScript.DestroyKey();

            // Spawn nieuwe sleutel
            playerKey = keySpawnerScript.SpawnKey(false);

            // Reset flag
            keyCollected = false;

        }


    }
}
