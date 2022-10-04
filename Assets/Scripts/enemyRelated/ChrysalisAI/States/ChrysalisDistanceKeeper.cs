using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChrysalisDistanceKeeper : IState
{
    private readonly chrysalisController _chrysalisController;
    private readonly NavMeshAgent _navMeshAgent;
    public ChrysalisDistanceKeeper(chrysalisController chrysalisController, NavMeshAgent navMeshAgent)
    {
        _chrysalisController = chrysalisController;
        _navMeshAgent = navMeshAgent;
    }

    public void Tick()
    {
        KeepDistance();
    }

    void KeepDistance()
    {
        Vector3 direction = -(_chrysalisController.PlayerLocation.position - _chrysalisController.transform.position).normalized;
        _chrysalisController.RotateToDirection(-direction);
        _navMeshAgent.Move(direction * (_chrysalisController.ChrysalisStats.patrolSpeed * Time.deltaTime));
    }

    public void OnEnter()
    {
        _navMeshAgent.enabled = true;
        _chrysalisController.GetComponent<Renderer>().material.color = Color.yellow;
    }

    public void OnExit()
    {
        _navMeshAgent.enabled = false;
    }
}
