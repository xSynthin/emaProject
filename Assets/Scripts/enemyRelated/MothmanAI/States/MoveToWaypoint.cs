using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToWaypoint : IState
{
    private readonly mothmanController _mothmanController;
    private readonly NavMeshAgent _navMeshAgent;
    private Vector3 _lastPosition = Vector3.zero;
    public float timeStuck;

    public MoveToWaypoint(mothmanController mothmanController, NavMeshAgent navMeshAgent)
    {
        _mothmanController = mothmanController;
        _navMeshAgent = navMeshAgent;
    }

    public void Tick()
    {
        MoveEnemyToWaypoint();
        if (Vector3.Distance(_mothmanController.transform.position, _lastPosition) <= 0f)
            timeStuck += Time.deltaTime;
        _lastPosition = _mothmanController.transform.position;
    }
    private void MoveEnemyToWaypoint()
    {
        Vector3 direction = (_mothmanController.Target.transform.position - _mothmanController.transform.position).normalized;
        _mothmanController.RotateToDirection(direction);
        _navMeshAgent.Move(direction * (_mothmanController.mothmanStats.patrolSpeed * Time.deltaTime));
    }

    public void OnEnter()
    {
        timeStuck = 0f;
        _navMeshAgent.enabled = true;
        //_navMeshAgent.SetDestination(_mothmanController.Target.transform.position);
    }

    public void OnExit()
    {
        _navMeshAgent.enabled = false;
        _mothmanController.wayPointReached = true;
    }
}
