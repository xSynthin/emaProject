using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "EnemyAI/Decisions/DamageInflictedOnAI")]
public class DamageInflictedOnAIDecision : Decision
{
    private RaycastHit[] hits;
    public override bool Decide(StateController controller)
    {
        bool targetVisible = FindDamageSource(controller);
        return targetVisible;
    }
    private bool FindDamageSource(StateController controller)
    {
        if (controller.mothmanUtils.health != controller.localEnemyHealth || controller.EnemyStats.attacked)
        {
            controller.EnemyStats.attacked = true;
            //TODO THIS NEEDS SERIOUS CHANGES
            //LAME IMPLEMENTATION
            controller.chaseTarget = PlayerManager.instance.playerUtils.transform;
            controller.localEnemyHealth = controller.mothmanUtils.health;
            return true;
        }
        controller.EnemyStats.attacked = false;
        controller.localEnemyHealth = controller.mothmanUtils.health;
        return false;
    }
}
