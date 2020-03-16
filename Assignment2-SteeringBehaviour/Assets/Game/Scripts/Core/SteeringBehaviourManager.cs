using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteeringAgent))]
public class SteeringBehaviourManager : MonoBehaviour
{
    protected SteeringAgent steeringAgent;
    [SerializeField]protected List<Vector3> path;
    [SerializeField]protected Vector3 targetPosition;
    protected int currentWaypoint = 0;

    protected void Start()
    {
        steeringAgent = GetComponent<SteeringAgent>();
    }

    private void OnDrawGizmos()
    {
        if (path.Count > 0)
        {
            for (int i = 1; i < path.Count; i++)
            {
                DebugExtension.DrawCone(path[i - 1], Color.white, 30f);
                Debug.DrawLine(path[i - 1], path[i], Color.white);
            }
            DebugExtension.DrawCone(path[path.Count - 1], Color.white, 30f);
            Debug.DrawLine(path[path.Count - 1], path[0], Color.white);

        }
    }
}
