using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToTargetBehaviour : StateMachineBehaviour
{
    Soldier soldier;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        soldier = animator.GetComponent<Soldier>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 newPosition = Vector2.MoveTowards(soldier.GetPosition(), soldier.GetTarget().GetPosition(), Time.deltaTime * soldier.GetCharacteristic().speed);
        soldier.Move(newPosition);
    }

}
