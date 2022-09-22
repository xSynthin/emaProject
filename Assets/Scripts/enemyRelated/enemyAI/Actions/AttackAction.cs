using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu (menuName = "EnemyAI/Actions/Attack")]
public class AttackAction : SAction 
{
    public override void Act(StateController controller)
    {
        Attack(controller);
    }

    private void Attack(StateController controller)
    {
        RaycastHit hit;
        if (Physics.Raycast(controller.eyes.position, controller.eyes.forward, out hit, 1f) && hit.collider.CompareTag("Player"))
        {
            //ATACK COROUTINE
        }
    }
}
