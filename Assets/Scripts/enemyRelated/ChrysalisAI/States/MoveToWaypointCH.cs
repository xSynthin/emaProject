using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class MoveToWaypointCH : IState
{
    // TODO CH function need to be removed with use of interface or abstract idk
    private readonly chrysalisController _chrysalisController;
    private readonly NavMeshAgent _navMeshAgent;
    private Vector3 _lastPosition = Vector3.zero;
    public float timeStuck;

    public MoveToWaypointCH(chrysalisController chrysalisController , NavMeshAgent navMeshAgent)
    {
        _chrysalisController = chrysalisController;
        _navMeshAgent = navMeshAgent;
    }

    public void Tick()
    {
        MoveEnemyToWaypoint();
        if (Vector3.Distance(_chrysalisController.transform.position, _lastPosition) <= 0f)
            timeStuck += Time.deltaTime;
        _lastPosition = _chrysalisController.transform.position;
    }
    private void MoveEnemyToWaypoint()
    {
        Vector3 direction = (_chrysalisController.WaypointTarget.transform.position - _chrysalisController.transform.position).normalized;
        _chrysalisController.RotateToDirection(direction);
        _navMeshAgent.Move(direction * (_chrysalisController.ChrysalisStats.patrolSpeed * Time.deltaTime));
    }

    public void OnEnter()
    {
        timeStuck = 0f;
        _navMeshAgent.enabled = true;
    }

    public void OnExit()
    {
        _navMeshAgent.enabled = false;
        //_mothmanController.wayPointReached = true;
    }
}
