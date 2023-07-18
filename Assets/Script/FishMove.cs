using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMove : MonoBehaviour
{
    public GameObject FishManager;

    public Vector2 location = Vector2.zero;
    public Vector2 velocity;
    Vector2 GoalPosition = Vector2.zero;
    Vector2 currentForce;

    // Start is called before the first frame update
    void Start()
    {
        velocity = new Vector2(Random.Range(0.01f, 0.1f), Random.Range(0.01f, 0.1f));
        location = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        flock();
        GoalPosition = FishManager.transform.position;
    }

    Vector2 SeekTar(Vector2 target)
    {
        return (target - location);
    }

    void ApplyForce(Vector2 force)
    {
        Vector3 f = new Vector3(force.x, force.y, 0);

        this.GetComponent<Rigidbody2D>().AddForce(f);

        Debug.DrawRay(this.transform.position, force, Color.red);

    }

    void flock()
    {
        location = this.transform.position;

        velocity = this.GetComponent<Rigidbody2D>().velocity;

        Vector2 gp = SeekTar(GoalPosition);

        currentForce = gp;

        currentForce = currentForce.normalized;

        ApplyForce(currentForce);
    }
}
