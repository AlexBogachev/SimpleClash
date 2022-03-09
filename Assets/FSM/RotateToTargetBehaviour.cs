using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToTargetBehaviour : StateMachineBehaviour
{
    Soldier soldier;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        soldier = animator.GetComponent<Soldier>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 direction = soldier.GetTarget().GetPosition() - soldier.GetPosition();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        soldier.transform.rotation = Quaternion.Lerp(soldier.transform.rotation, q, Time.deltaTime*4.5f);

        float currentAngle = Vector3.Angle(direction, soldier.transform.right);
        if (currentAngle < 1.0f)
        {
            animator.SetTrigger("isLookingOnTarget");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("isLookingOnTarget");
    }
}
