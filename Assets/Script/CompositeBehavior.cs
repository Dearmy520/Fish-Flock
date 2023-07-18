using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : FlockBehavior
{
    public FlockBehavior[] beha;
    public float[] weights;
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {

        if (weights.Length != beha.Length)
        {
            return Vector2.zero;
        }

        Vector2 move = Vector2.zero;

        for (int i = 0; i < beha.Length; i++)
        {
            Vector2 partialMove = beha[i].CalculateMove(agent, context, flock) * weights[i];

            if (partialMove != Vector2.zero)
            {
                if (partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }
                move += partialMove;
            }

        }

        return move;
    }


}
