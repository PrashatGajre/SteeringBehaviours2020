using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteeringAgent))]
public class WanderingAgentBehaviour : MonoBehaviour
{
    protected SteeringAgent steeringAgent;
    
    public float innerBoundsRadius = 15.0f;
    public float outerBoundsRadius = 35.0f;

    Vector3 terrainPosition;
    Vector3 terrainCenter;

    SeekSteeringBehaviour seekSteering;
    WanderSteeringBehaviour wanderSteering;
    ObstacleAvoidSteeringBehaviour obstacleAvoidSteering;

    Vector3 seekPosition;

    bool isSeeking = false;
    public float seekRadius = 1.2f;
    bool isWandering = false;

    protected void Start()
    {
        steeringAgent = GetComponent<SteeringAgent>();

        seekSteering = GetComponentInChildren<SeekSteeringBehaviour>();
        seekSteering.useMouseInput = false;
        wanderSteering = GetComponentInChildren<WanderSteeringBehaviour>();
        obstacleAvoidSteering = GetComponentInChildren<ObstacleAvoidSteeringBehaviour>();

        terrainPosition = Terrain.activeTerrain.gameObject.transform.position;
        terrainCenter = new Vector3(terrainPosition.x + Terrain.activeTerrain.terrainData.size.x / 2, terrainPosition.y, terrainPosition.z + Terrain.activeTerrain.terrainData.size.z / 2);

        isSeeking = true;
        SeekToCenter();
    }

    public void SeekToCenter()
    {
        isSeeking = true;
        seekPosition = Random.insideUnitCircle * innerBoundsRadius;
        seekPosition = new Vector3(seekPosition.x, 0, seekPosition.z);
        seekSteering.target = seekPosition;
        seekSteering.weight = 1;
        wanderSteering.weight = 0;
    }

    public void Wander()
    {
        isWandering = true;
        seekSteering.weight = 0;
        wanderSteering.weight = 1;
    }

    void Update()
    {
        if (isSeeking)
        {
            if ((transform.position - seekPosition).magnitude < seekRadius)
            {
                isSeeking = false;
                Wander();
            }
        }
        else if (isWandering)
        {
            if ((transform.position - terrainCenter).magnitude > outerBoundsRadius)
            {
                isWandering = false;
                SeekToCenter();
            }
        }
    }

    private void OnDrawGizmos()
    {
        //Bounds
        DebugExtension.DrawCircle(terrainCenter, Color.green, innerBoundsRadius);
        DebugExtension.DrawCircle(terrainCenter + Vector3.up, Color.green, innerBoundsRadius);
        DebugExtension.DrawCircle(terrainCenter, Color.red, outerBoundsRadius);
        DebugExtension.DrawCircle(terrainCenter + Vector3.up, Color.red, outerBoundsRadius);


        if (isSeeking)
        {
            Debug.DrawLine(transform.position, seekPosition, Color.yellow);
            DebugExtension.DebugWireSphere(seekPosition, Color.yellow);
        }
    }
}
