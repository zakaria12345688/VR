using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class SeekerAgent : Agent
{
    [Header("Environment")]
    public KeySpawner keySpawnerScript;
    public GameObject agentTargetDoor;
    private GameObject AgentKey;



    public override void OnEpisodeBegin()
    {
        AgentKey = keySpawnerScript.SpawnKey();
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        // Agent positie
        sensor.AddObservation(this.transform.localPosition);
    }

    public float speedMultiplier = 0.5f;
    public float rotationMultiplier = 5f;

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Acties, size = 2
        //Vector3 controlSignal = Vector3.zero;
        //controlSignal.z = actionBuffers.ContinuousActions[0];

        //transform.Translate(controlSignal * speedMultiplier);

        //transform.Rotate(0.0f, rotationMultiplier * actionBuffers.ContinuousActions[1], 0.0f);
        Vector3 controlSignal = Vector3.zero;
        controlSignal.x = actionBuffers.ContinuousActions[0];
        controlSignal.z = actionBuffers.ContinuousActions[1];
        transform.Translate(controlSignal * speedMultiplier);

        // Beloningen
        float distanceToTarget = Vector3.Distance(this.transform.localPosition, AgentKey.transform.localPosition);

        // target bereikt
        if (distanceToTarget < 1.42f)
        {
            SetReward(1.0f);
            EndEpisode();
        }
        // Van het platform. gevallen?
        else if (this.transform.localPosition.y < 0)
        {
            EndEpisode();
        }
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Vertical");
        continuousActionsOut[1] = Input.GetAxis("Horizontal");
    }
}
