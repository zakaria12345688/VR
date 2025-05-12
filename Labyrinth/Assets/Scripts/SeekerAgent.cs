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



    public override void OnEpisodeBegin()
    {
        keySpawnerScript.SpawnKey();
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        // Agent should observer world with camera
    }
}
