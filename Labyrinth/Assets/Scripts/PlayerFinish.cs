using Unity.MLAgents;
using UnityEngine;

public class PlayerFinish : MonoBehaviour
{
    [Header("Environment")]
    public GameObject PlayerDoor; // Deur GameObject
    public KeySpawner keySpawnerScript; // Verwijzing naar KeySpawner script
    public GameObject Player; // Speler GameObject

    [Header("Instellingen")]
    public bool enableDoorSystem = true; // Schakelt het deursysteem in/uit

    private Vector3 startPosition = new Vector3(1f, 0.5f, 1f); // Startpositie van de speler
    private GameObject playerKey; // Huidige sleutel

    public SeekerRays Agent;

    private bool keyCollected = false; // later in code nodig

    public float startX = 0.0f;
    public float startY = 0.1f;
    public float startZ = 0.0f;


    private void OnTriggerEnter(Collider other)
    {
        // Controleer of het deursysteem actief is
        if (!enableDoorSystem) return;

        // Controleer of de sleutel de trigger raakt
        if (other.CompareTag("playerKey"))
        {
            Debug.Log("Sleutel raakt deur");
            ScoreManager.Instance.AddPlayerScore(1);
            // Verplaats speler naar startpositie
            // Player.transform.position = startPosition;
            Debug.Log($"Speler verplaatst naar startpositie: {startPosition}");

            Agent.PlayerEndEpisode();

            // Vernietig de oude sleutel
            // keySpawnerScript.DestroyKey();
            // Debug.Log("Oude sleutel vernietigd");

            // Spawn een nieuwe sleutel

            /*playerKey = keySpawnerScript.SpawnKey(false);
            if (playerKey != null)
            {
                Debug.Log($"Nieuwe sleutel gespaund: {playerKey.name}");
            }
            else
            {
                Debug.LogError("Kon geen nieuwe sleutel spawnen!");
            }*/
        }
    }
}