using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

[TaskCategory("Assassin Bot")]
public class TurnInvisible : Action
{
    public SharedInt InvisibleCount;

    bool done;

    GameObject original, disguise, invisible;

    float elap;

    public override void OnAwake()
    {
        done = false;
        elap = 0f;

        original = transform.Find("Original").gameObject;
        disguise = transform.Find("Disguise").gameObject;
        invisible = transform.Find("Invisible").gameObject;

        Debug.Log("going invisible now");
    }

    public override TaskStatus OnUpdate()
    {
        // adding a slight delay to signify putting on of the disguise
        if(elap >= 0.5f)
        {
            original.SetActive(false);
            disguise.SetActive(false);
            invisible.SetActive(true);

            InvisibleCount.Value--;

            done = true;
            return TaskStatus.Success;
        }
        else 
        {
            elap += Time.deltaTime;
            return TaskStatus.Running;
        }
    }

    public override void OnEnd()
    {
        if (done)
        {
            Debug.Log("invisibility successfully activated");
            Debug.Log(InvisibleCount.Value + " invisibility skill charges left");
        }
    }
}