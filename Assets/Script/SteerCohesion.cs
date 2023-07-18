using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Steer")]
public class SteerCohesion : FlockBehavior
{
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {


        if (context.Count == 0) return Vector2.zero;

        Vector2 cohensionMove = Vector2.zero;

        foreach (Transform item in context)
        {
            cohensionMove += (Vector2)item.position;
        }

        cohensionMove /= context.Count;

        cohensionMove -= (Vector2)agent.transform.position;
        cohensionMove = Vector2.SmoothDamp(agent.transform.up, cohensionMove, ref currentVelocity, agentSmoothTime);
        return cohensionMove;
    }
}
