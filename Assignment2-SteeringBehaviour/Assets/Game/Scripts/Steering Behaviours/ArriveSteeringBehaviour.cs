using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArriveSteeringBehaviour : SeekSteeringBehaviour
{
	public float slowDownDistance = 5.0f;
	public float deceleration = 2.5f;
	public float stoppingDistance = 0.5f;

	public override Vector3 calculateForce()
	{
		checkMouseInput();

		Vector3 toTarget = target - transform.parent.position;
		float distanceToTarget = toTarget.magnitude;

		steeringAgent.reachedGoal = false;
		if (distanceToTarget > slowDownDistance)
		{
			return base.calculateForce();
		}
		else if (distanceToTarget > stoppingDistance)
		{
			float speed = distanceToTarget / deceleration;
			if (speed > steeringAgent.maxSpeed)
			{
				speed = steeringAgent.maxSpeed;
			}

			speed = speed / distanceToTarget;
			Vector3 desiredVelocity = toTarget.normalized * speed;
			return desiredVelocity - steeringAgent.velocity;
		}

		steeringAgent.reachedGoal = true;
		return Vector3.zero;
	}
}
