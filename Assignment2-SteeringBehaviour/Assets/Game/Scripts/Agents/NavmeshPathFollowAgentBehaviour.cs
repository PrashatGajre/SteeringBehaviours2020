using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavmeshPathFollowAgentBehaviour : SteeringBehaviourManager
{
    public bool loop = false;
    PathFollowSteeringBehaviour pathFollowSteering;

    void Start()
    {
        base.Start();
        pathFollowSteering = GetComponentInChildren<PathFollowSteeringBehaviour>();
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
