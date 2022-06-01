using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class Test : Action
{
    public SharedArsenal Weapon;

    public override TaskStatus OnUpdate()
    {
        Weapon.Value = Arsenal.Sword;
        return TaskStatus.Success;
    }
}