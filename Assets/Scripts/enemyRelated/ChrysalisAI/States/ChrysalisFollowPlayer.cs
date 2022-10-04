using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChrysalisFollowPlayer : IState
{
    private readonly chrysalisController _chrysalisController;
    private readonly NavMeshAgent _navMeshAgent;

    public ChrysalisFollowPlayer(chrysalisController chrysalisController, NavMeshAgent navMeshAgent)
    {
        _chrysalisController = chrysalisController;
        _navMeshAgent = navMeshAgent;
    }

    public void Tick()
    {
        MoveChrysalisToPlayer();
    }

    private void MoveChrysalisToPlayer()
    {
        Vector3 direction = (_chrysalisController.PlayerLocation.position - _chrysalisController.transform.position).normalized;
        _chrysalisController.RotateToDirection(direction);
        _navMeshAgent.Move(direction * (_chrysalisController.ChrysalisStats.chaseSpeed * Time.deltaTime));
    }

    public void OnEnter()
    {
        _chrysalisController.GetComponent<Renderer>().material.color = Color.black;
        _navMeshAgent.enabled = true;
    }
    public void OnExit() => _navMeshAgent.enabled = false;
}
