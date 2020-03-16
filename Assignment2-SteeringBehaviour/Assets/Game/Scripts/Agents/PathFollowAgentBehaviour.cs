using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowAgentBehaviour : SteeringBehaviourManager
{
    public bool loop = false;
    PathFollowSteeringBehaviourAlternative pathFollowSteering;
    ObstacleAvoidSteeringBehaviour obstacleAvoidSteering;

    void Start()
    {
        base.Start();
        pathFollowSteering = GetComponentInChildren<PathFollowSteeringBehaviourAlternative>();
        obstacleAvoidSteering = GetComponentInChildren<ObstacleAvoidSteeringBehaviour>();

        pathFollowSteering.weight = 1;
        obstacleAvoidSteering.weight = 5;

        targetPosition = path[currentWaypoint++];
        pathFollowSteering.target = targetPosition;
        pathFollowSteering.GeneratePath();
    }

    void Update()
    {
        if (pathFollowSteering.arrived)
        {
            targetPosition = path[currentWaypoint++];
            if (currentWaypoint >= path.Count)
            {
                currentWaypoint = loop ? 0 : path.Count - 1;
            }
            pathFollowSteering.target = targetPosition;
            pathFollowSteering.GeneratePath();
        }
    }
}
