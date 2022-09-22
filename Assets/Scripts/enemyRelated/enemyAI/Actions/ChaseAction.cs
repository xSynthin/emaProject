using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu (menuName = "EnemyAI/Actions/Chase")]
public class ChaseAction : SAction
{
    public override void Act(StateController controller)
    {
        Chase(controller);
    }

    private void Chase(StateController controller)
    {
        controller.NavMeshAgent.speed = controller.chaseSpeed;
        controller.NavMeshAgent.destination = controller.chaseTarget.position;
        controller.NavMeshAgent.isStopped = false;
    }
}
