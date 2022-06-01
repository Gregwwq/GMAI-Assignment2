using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Assassin Bot")]
public class Suicide : Action
{
    bool done;

    GameObject original, disguise, invisible, dead;

    public override void OnAwake()
    {
        done = false;

        original = transform.Find("Original").gameObject;
        disguise = transform.Find("Disguise").gameObject;
        invisible = transform.Find("Invisible").gameObject;
        dead = transform.Find("Dead").gameObject;

        Debug.Log("assassin has been caught, committing suicide to protect its secrets...");
    }

    public override TaskStatus OnUpdate()
    {
        original.SetActive(false);
        disguise.SetActive(false);
        invisible.SetActive(false);
        dead.SetActive(true);

        return TaskStatus.Success;
    }

    public override void OnEnd()
    {
        if (done)
        {
            Debug.Log("SUICIDE: assassin bot is dead");
        }
    }
}