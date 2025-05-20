using System.Collections;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class SeekerAgent : Agent
{
    [Header("Environment")]
    public KeySpawner keySpawnerScript;
    public GameObject agentTargetDoor;
    private GameObject agentKey;

    public float speedMultiplier = 0.5f;

    public override void OnEpisodeBegin()
    {
        // Geen reset positie, agent blijft waar die is

        // Verwijder oude sleutel
        keySpawnerScript.DestroyKey();
        agentKey = null;

        // Spawn nieuwe sleutel
        agentKey = keySpawnerScript.SpawnKey();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);

        if (agentKey != null)
        {
            sensor.AddObservation(agentKey.transform.localPosition);
        }
        else
        {
            sensor.AddObservation(Vector3.zero);
        }
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        Vector3 move = new Vector3(actionBuffers.ContinuousActions[0], 0, actionBuffers.ContinuousActions[1]);
        transform.Translate(move * speedMultiplier);

        // Val check
        if (transform.localPosition.y < -1f)
        {
            keySpawnerScript.DestroyKey();
            agentKey = null;
            EndEpisode();
        }
    }

    // Nieuw: Trigger check om sleutel te pakken
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("agentKey"))
        {
            SetReward(1.0f);
            Debug.Log("KeyTouched");
            keySpawnerScript.DestroyKey();
            agentKey = null;

            // Spawn nieuwe sleutel na korte delay
            StartCoroutine(RespawnKeyAfterPickup());
        }
    }

    private IEnumerator RespawnKeyAfterPickup()
    {
        yield return new WaitForSeconds(0.2f);
        agentKey = keySpawnerScript.SpawnKey();
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Horizontal");
        continuousActionsOut[1] = Input.GetAxis("Vertical");
    }
}
