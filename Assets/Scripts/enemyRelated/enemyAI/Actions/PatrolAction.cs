using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyAI/Actions/Patrol")]
public class PatrolAction : SAction
{
    public override void Act(StateController controller)
    {
        Patrol(controller);
    }
    private void Patrol(StateController controller)
    {
        controller.NavMeshAgent.speed = controller.patrolSpeed;
        controller.NavMeshAgent.destination = controller.wayPointList[controller.nextWayPoint].position;
        controller.NavMeshAgent.isStopped = false;
        if (controller.NavMeshAgent.remainingDistance <= controller.NavMeshAgent.stoppingDistance &&
            !controller.NavMeshAgent.pathPending)
        {
            controller.nextWayPoint = (controller.nextWayPoint + Random.Range(1,5)) % controller.wayPointList.Count;
        }
    }
}
