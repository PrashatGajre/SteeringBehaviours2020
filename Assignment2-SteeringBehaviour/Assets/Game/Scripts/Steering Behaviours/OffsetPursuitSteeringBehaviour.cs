using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetPursuitSteeringBehaviour : ArriveSteeringBehaviour
{
	public SteeringAgent pursuitAgent;
	public Vector3 offset;

	public override Vector3 calculateForce()
	{
		if (pursuitAgent != null)
		{
			Vector3 worldOffsetPos = (pursuitAgent.transform.rotation * offset) +
									  pursuitAgent.transform.position;

			Vector3 toOffset = worldOffsetPos - transform.position;
			float lookAheadTime = toOffset.magnitude / (steeringAgent.maxSpeed + pursuitAgent.velocity.magnitude);

			target = (worldOffsetPos + pursuitAgent.velocity * lookAheadTime);

			return base.calculateForce();
		}

		return Vector3.zero;
	}
}
