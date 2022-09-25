using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : IState
{
    private readonly mothmanController _mothmanController;
    private readonly NavMeshAgent _navMeshAgent;

    public ChasePlayer(mothmanController mothmanController, NavMeshAgent navMeshAgent)
    {
        _mothmanController = mothmanController;
        _navMeshAgent = navMeshAgent;
    }
    public void Tick()
    {
        MoveEnemyToPlayer();
    }
    private void MoveEnemyToPlayer()
    {
        Vector3 direction = (_mothmanController.PlayerLocation.position - _mothmanController.transform.position).normalized;
        _mothmanController.RotateToDirection(direction);
        _navMeshAgent.Move(direction * (_mothmanController.mothmanStats.chaseSpeed * Time.deltaTime));
    }
    public void OnEnter()
    {
        _navMeshAgent.enabled = true;
    }

    public void OnExit()
    {
        _navMeshAgent.enabled = false;
    }
}
