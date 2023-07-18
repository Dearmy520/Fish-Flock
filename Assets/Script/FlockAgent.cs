using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{

    public float moveForce = 10f;
    Collider2D agentCollider;
    public Collider2D AgnetCollider { get { return agentCollider; } }
    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Move(Vector2 velocity)
    {
        transform.up = velocity;

        transform.position += (Vector3)velocity * Time.deltaTime;
    }
    public void StartRandomMovement()
    {
        // 生成随机方向向量
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        // 计算随机移动力量
        Vector2 randomForce = randomDirection * moveForce;

        // 应用随机移动力量
        ApplyForce(randomForce);
    }
    void ApplyForce(Vector2 force)
    {
        Vector3 f = new Vector3(force.x, force.y, 0);

        this.GetComponent<Rigidbody2D>().AddForce(f);

    }
}
