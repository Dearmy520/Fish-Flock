using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class CohesionBehavior : FlockBehavior
{
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
        return cohensionMove;
    }
}
