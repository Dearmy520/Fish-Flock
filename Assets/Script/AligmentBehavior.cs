using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Aligment")]
public class AligmentBehavior : FlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {


        if (context.Count == 0) return agent.transform.up;

        Vector2 AligmentMvoe = Vector2.zero;

        foreach (Transform item in context)
        {
            AligmentMvoe += (Vector2)item.transform.up;
        }

        AligmentMvoe /= context.Count;

        return AligmentMvoe;
    }
}
