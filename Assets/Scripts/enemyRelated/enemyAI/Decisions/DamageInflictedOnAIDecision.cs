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
        if (controller.mothmanUtils.health != controller.localHealth)
        {
            //TODO THIS NEEDS SERIOUS CHANGES
            //LAME IMPLEMENTATION
            controller.chaseTarget = PlayerManager.instance.playerUtils.transform;
            controller.localHealth = controller.mothmanUtils.health;
            return true;
        }
        controller.localHealth = controller.mothmanUtils.health;
        return false;
    }
}