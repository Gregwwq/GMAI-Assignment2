using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Target Bot")]
public class ChaseNearestDecoy : Action
{
    public SharedBool Chasing;
    public SharedFloat Speed, RotateSpeed;

    bool done;

    GameObject[] decoys;
    Transform nearest;

    public override void OnStart()
    {
        done = false;
    }

    public override TaskStatus OnUpdate()
    {
        decoys = GameObject.FindGameObjectsWithTag("Decoy");

        if (decoys.Length < 1)
        {
            return TaskStatus.Success;
        }

        FindNearestDecoy();
        Chase();

        return TaskStatus.Running;
    }

    void FindNearestDecoy()
    {
        float nearestDist = Mathf.Infinity;
        
        foreach (GameObject decoy in decoys)
        {
            // finding nearest decoy
            float dist = Vector3.Distance(transform.position, decoy.transform.position);

            if (dist < nearestDist)
            {
                nearest = decoy.transform;
                nearestDist = dist;
            }
        }
    }

    public override void OnEnd()
    {
        if (done)
        {
            Chasing.Value = false;
        }
    }

    void Chase()
    {
        // setting the target rotation
        Quaternion lookRotation = Quaternion.LookRotation((nearest.position - transform.position), Vector3.up);

        // using Vector3.MoveTowards() to move from the current location to the target position
        transform.position =
            Vector3.MoveTowards(transform.position, nearest.position, Speed.Value * Time.deltaTime);

        // using Quaternion.RotateTowards() to smoothly rotate towards the target rotation
        transform.rotation =
            Quaternion.RotateTowards(transform.rotation, lookRotation, RotateSpeed.Value * Time.deltaTime);
    }
}