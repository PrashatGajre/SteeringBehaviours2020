using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathFollowSteeringBehaviour : ArriveSteeringBehaviour
{
	public Transform PathTarget;
	public float WaypointSeekDist = 0.5f;
	public bool loop = false;

	public int currentWaypointIndex = 0;

	private NavMeshPath path;

	public bool arrived = false;

    void Start()
    {
		//GeneratePath();
    }

	public void GeneratePath()
	{
		arrived = false;
		path = new NavMeshPath();
		currentWaypointIndex = 0;
		NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, path);
		if (path != null && path.corners.Length > 0)
		{
			target = path.corners[currentWaypointIndex];
		}
	}

	public override Vector3 calculateForce()
	{
		if (currentWaypointIndex >= path.corners.Length - 1)
		{
			if ((target - transform.parent.position).magnitude < WaypointSeekDist)
			{
				arrived = true; 
			}
			return base.calculateForce();
		}
		else if ((path.corners[currentWaypointIndex] - transform.parent.position).magnitude < WaypointSeekDist)
		{
			currentWaypointIndex++;
			target = path.corners[currentWaypointIndex];
		}
		return base.calculateForce();
	}

	private void OnDrawGizmos()
	{
		if (path != null)
		{
			for(int i = 1; i < path.corners.Length; i++)
			{
				DebugExtension.DebugWireSphere(path.corners[i - 1], Color.blue, 0.5f);
				Debug.DrawLine(path.corners[i - 1], path.corners[i], Color.blue);
			}
		}
	}

}
