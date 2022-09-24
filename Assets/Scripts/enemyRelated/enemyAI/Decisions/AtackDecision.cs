using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "EnemyAI/Decisions/AttackDecision")]
public class AtackDecision : Decision 
{
    public override bool Decide(StateController controller)
    {
        bool attackDecision = CanAttack(controller);
        return attackDecision;
    }

    private bool CanAttack(StateController controller)
    {
        if (Vector3.Distance(controller.transform.position, controller.chaseTarget.position) < 1f)
        {
            return true;
        }
        return false;
    }
}
