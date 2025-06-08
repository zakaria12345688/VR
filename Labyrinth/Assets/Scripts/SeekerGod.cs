using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class SeekerGod : Agent
{
    [Header("Environment")]
    public KeySpawner keySpawnerScript;
    public GameObject agentTargetDoor; // Leeglaten mag, als enableDoorSystem false blijft
    public bool enableDoorSystem = false; // false: trainen zonder deur
    private GameObject agentKey;
    private bool keyCollected = false; // later in code nodig

    private GameObject playerKey;
    public float startX = 0.0f;
    public float startY = 0.1f;
    public float startZ = 0.0f;
    [Header("Movement")]
    public float speedMultiplier = 0.5f;
    public float rotationSpeed = 100f;
    public float forceMultiplier = 50f;
    public Rigidbody agentRigid;
    [Header("Training")]
    public float keyCollectionReward = 1.0f;
    public float doorReachReward = 1.0f;
    public float fallPunishment = -1.0f;
    public float durationPunishment = -0.001f;

    public override void OnEpisodeBegin()
    {
        // Geen reset positie, agent blijft waar die is
        this.transform.localPosition = new Vector3(startX, startY, startZ);

        // Verwijder oude sleutel
        keySpawnerScript.DestroyKey(agentKey, true);

        // agentKey = null;

        // Spawn nieuwe sleutel
        agentKey = keySpawnerScript.SpawnKey(true);
        playerKey = keySpawnerScript.SpawnKey(false);

        // Reset keyCollected
        keyCollected = false;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // override functie sowieso nodig, maar mag leeg zijn aangezien we met camerasensor werken (zie componenenten op agent object)
        // Indien enkel camera sensor, zet space size op 0 in inspector.

        // Probeer eerst zonder dat agent zijn exacte co�rdinaten weet, als dat niet werkt, uncomment de volgende lijn
        sensor.AddObservation(transform.localPosition);

        if (agentKey != null)
        {
            sensor.AddObservation(agentKey.transform.localPosition);
        }
        else
        {
            sensor.AddObservation(Vector3.zero); // Als er geen key is, voeg een nulvector toe
        }
        if (agentTargetDoor != null)
        {
            sensor.AddObservation(agentTargetDoor.transform.localPosition);
        }
        else
        {
            sensor.AddObservation(Vector3.zero); // Als er geen deur is, voeg een nulvector toe
        }

        // Volgende lijnen zorgen dat agent exacte co�rdinaten van agentKey weet, maar we gebruiken liever een camera sensor
        /*if (agentKey != null)
        {
        sensor.AddObservation(agentKey.transform.localPosition);
        }
        else
        {
            sensor.AddObservation(Vector3.zero);
        }*/
    }

    public void PlayerEndEpisode()
    {
        keySpawnerScript.DestroyKey(playerKey, false);
        keySpawnerScript.DestroyKey(agentKey, true);
        EndEpisode();

    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        AddReward(durationPunishment);
        float moveForwardInput = actionBuffers.ContinuousActions[1];
        float moveSidewaysInput = actionBuffers.ContinuousActions[0];
        Vector3 move = (transform.forward * moveForwardInput) + (transform.right * moveSidewaysInput);
        agentRigid.AddForce(move.normalized * forceMultiplier, ForceMode.Force);

        float rotateAction = actionBuffers.ContinuousActions[2];
        agentRigid.angularVelocity = Vector3.up * rotateAction * rotationSpeed;

        // Val check
        if (transform.localPosition.y < -1f)
        {
            Debug.Log("Fallen");
            keySpawnerScript.DestroyKey(agentKey, true);
            keySpawnerScript.DestroyKey(playerKey, false);
            AddReward(fallPunishment);
            // agentKey = null;
            Debug.Log(GetCumulativeReward());
            EndEpisode();
        }
    }

    // Nieuw: Trigger check om sleutel te pakken
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Collision with {other.gameObject.tag}");
        if (other.gameObject.CompareTag("agentKey")) // check of het de juiste key is m.b.v. tag
        {
            AddReward(keyCollectionReward);
            keySpawnerScript.DestroyKey(agentKey, true);
            if (enableDoorSystem is false) // voor trainen zonder deur (eerste trainingen)
            {
                Debug.Log("Key collected, door system disabled, ending episode.");
                EndEpisode();
            }
            else
            {
                Debug.Log("Key collected, door system enabled, continuing episode.");
                keyCollected = true;
            }
            keyCollected = true;
            // agentKey = null;
            // volgende 2 lijnen: denk dat dit heel raar gaat doen bij trainen
            // Spawn nieuwe sleutel na korte delay
            // StartCoroutine(RespawnKeyAfterPickup());
        }
        if (other.gameObject.CompareTag("agentDoor") && keyCollected is true)
        {
            AddReward(doorReachReward);
            Debug.Log("Door reached, ending episode.");
            EndEpisode();
        }
    }
    // Volgende methode: wordt nutteloos bij nieuwe versie van script, maar ik laat het staan
    /*private IEnumerator RespawnKeyAfterPickup()
    {
        yield return new WaitForSeconds(0.2f);
        agentKey = keySpawnerScript.SpawnKey();
    }*/

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
        float rotateInput = 0f;
        if (Input.GetKey(KeyCode.Q))
        {
            rotateInput = -1f; // Rotate Left
        }
        else if (Input.GetKey(KeyCode.E))
        {
            rotateInput = 1f; // Rotate Right
        }
        continuousActionsOut[2] = rotateInput;
    }
}
