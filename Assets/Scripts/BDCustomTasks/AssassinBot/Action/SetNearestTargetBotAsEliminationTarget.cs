using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Assassin Bot")]
public class SetNearestTargetBotAsEliminationTarget : Action
{
    public SharedGameObject EliminationTarget;

    GameObject nearest;
    float nearestDist;

    bool done;

    public override void OnAwake()
    {
        done = false;
    }

    public override TaskStatus OnUpdate()
    {
        GameObject[] targetBots = GameObject.FindGameObjectsWithTag("Target");

        foreach (GameObject bot in targetBots)
        {
            // finding nearest bot
            float dist = Vector3.Distance(transform.position, bot.transform.position);
            nearestDist = Mathf.Infinity;

            if (dist < nearestDist)
            {
                nearest = bot;
                nearestDist = dist;
            }
        }

        EliminationTarget.Value = nearest;
        done = true;
        return TaskStatus.Success;
    }

    public override void OnEnd()
    {
        if (done)
        {
            Debug.Log("target found!");
        }
    }
}