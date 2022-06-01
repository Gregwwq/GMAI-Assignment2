using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Movement")]
public class WanderRandomly : Action
{
    public SharedFloat Speed, RotateSpeed;
    public float WanderRange;

    bool done;
    Vector3 targetLocation;

    public override void OnAwake()
    {
        done = false;

        SetNewPosition();
    }

    public override TaskStatus OnUpdate()
    {
        if (Vector3.Distance(transform.position, targetLocation) < 0.001f)
        {
            SetNewPosition();
        }

        Move();

        return TaskStatus.Running;
    }

    public override void OnEnd()
    {
        if (done)
        {

        }
    }

    void Move()
    {
        // setting the target rotation
        Quaternion lookRotation = Quaternion.LookRotation((targetLocation - transform.position), Vector3.up);

        // using Vector3.MoveTowards() to move from the current location to the target position
        transform.position =
            Vector3.MoveTowards(transform.position, targetLocation, Speed.Value * Time.deltaTime);

        // using Quaternion.RotateTowards() to smoothly rotate towards the target rotation
        transform.rotation =
            Quaternion.RotateTowards(transform.rotation, lookRotation, RotateSpeed.Value * Time.deltaTime);
    }

    void SetNewPosition()
    {
        // setting the boundaries of the targt location
        float minX = transform.position.x - WanderRange;
        float maxX = transform.position.x + WanderRange;
        float minZ = transform.position.z - WanderRange;
        float maxZ = transform.position.z + WanderRange;

        // setting the random target location within the boundaries
        targetLocation = new Vector3(Random.Range(minX, maxX), transform.position.y, Random.Range(minZ, maxZ));
    }
}