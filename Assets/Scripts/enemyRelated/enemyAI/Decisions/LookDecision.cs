using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu (menuName = "EnemyAI/Decisions/Look")]
public class LookDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetVisible = Look(controller);
        if (controller.noPatrol)
        {
            controller.chaseTarget = PlayerManager.instance.playerUtils.transform;
            return true;
        }
        return targetVisible;
    }

    private bool Look(StateController controller)
    {
        RaycastHit hit;
        //Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.EnemyStats.lookRange, Color.green);
        if (Physics.SphereCast(controller.eyes.position, controller.EnemyStats.lookSphereCastRadius,
                controller.eyes.forward, out hit, controller.EnemyStats.lookRange)
            && hit.collider.CompareTag("Player"))
        {
            controller.EnemyStats.attacked = true;
            controller.chaseTarget = hit.transform;
            return true;
        }
        return false;
    }
}
