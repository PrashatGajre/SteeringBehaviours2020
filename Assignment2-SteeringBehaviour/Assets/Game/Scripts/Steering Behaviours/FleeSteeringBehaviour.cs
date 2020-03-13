using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeSteeringBehaviour : SteeringBehaviourBase
{
	public float fleeDistance = 5.0f;
	public Transform enemyTarget;

	public override Vector3 calculateForce()
	{
		if (enemyTarget == null)
		{
			checkMouseInput();
		}
		else
		{
			target = enemyTarget.position;
		}

		// check the distance return zero if we are far away
		float distance = (target - transform.parent.position).magnitude;
		if (distance > fleeDistance)
		{
			return Vector3.zero;
		}

		Vector3 desiredVelocity = (transform.parent.position - target).normalized;
		desiredVelocity = desiredVelocity * steeringAgent.maxSpeed;
		return desiredVelocity - steeringAgent.velocity;
	}

	private void OnDrawGizmos()
	{
		DebugExtension.DrawCircle(transform.parent.position, fleeDistance);
		DebugExtension.DebugWireSphere(target, Color.red);
	}

}
