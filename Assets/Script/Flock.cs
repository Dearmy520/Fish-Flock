using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefebs;

    List<FlockAgent> agents = new List<FlockAgent>();

    public FlockBehavior behavior;

    [Range(10, 500)]
    public int startCount = 250;
    const float AgentDensity = 0.05f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;

    [Range(1f, 100f)]
    public float maxSpeed = 5f;

    [Range(1f, 10f)]
    public float neighborRaidus = 1.5f;

    [Range(0f, 1f)]
    public float avoidRadius = 0.5f;

    float SquareMaxSpeed;
    float SquarrNeighborRadius;
    float squareAvoidRadius;
    private float rayLength = 10f;
    public float SquareAvoidDanceRadius { get { return squareAvoidRadius; } }
    // Start is called before the first frame update
    void Start()
    {
        SquareMaxSpeed = maxSpeed * maxSpeed;
        SquarrNeighborRadius = neighborRaidus * neighborRaidus;
        squareAvoidRadius = SquarrNeighborRadius * avoidRadius * avoidRadius;

        for (int i = 0; i < startCount; i++)
        {
            FlockAgent newAgent = Instantiate(
            agentPrefebs,
            Random.insideUnitCircle * startCount * AgentDensity,
            Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
            transform
            );
            newAgent.name = "Fish" + i;
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearObject(agent);

            agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.blue, context.Count / 6f);

            Vector2 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;

            if (move.sqrMagnitude > SquareMaxSpeed)
            {
                move = move.normalized / maxSpeed;
            }
            agent.Move(move);

        }
        MouseAvoid();
    }

    List<Transform> GetNearObject(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();

        Collider2D[] contextCOllider = Physics2D.OverlapCircleAll(agent.transform.position, neighborRaidus);

        foreach (Collider2D c in contextCOllider)
        {
            if (c!= agent.AgnetCollider)
            {
                context.Add(c.transform);

            }
        }
        return context;

    }

    void MouseAvoid()
    {
        // 检测鼠标左键点击事件
        if (Input.GetMouseButtonDown(0))
        {
            // 发射射线
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.red, 1f);
            // 检测是否点击到FlockAgent组件
            if (hit && hit.collider.GetComponent<FlockAgent>() && hit.collider.gameObject.layer == LayerMask.NameToLayer("Fish"))
            {
                // 获取被点击的鱼的FlockAgent组件
                FlockAgent clickedAgent = hit.collider.GetComponent<FlockAgent>();

                // 调用StartRandomMovement方法触发随机移动
                clickedAgent.StartRandomMovement();
                clickedAgent.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
            }
        }
    }
}
