using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Follow : IState
{
    private readonly chrysalisController _chrysalisController;
    private readonly NavMeshAgent _navMeshAgent;
    public Follow(chrysalisController chrysalisController, NavMeshAgent navMeshAgent)
    {
         _chrysalisController = chrysalisController;
        _navMeshAgent = navMeshAgent;
    }

    public void OnEnter()
    {
        _navMeshAgent.enabled = true;
        _chrysalisController.GetComponent<Renderer>().material.color = Color.green;
    }

    public void OnExit()
    {
        _navMeshAgent.enabled = false;
    }
    public void Tick()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        Vector3 dir = (PlayerManager.instance.playerUtils.transform.position - _chrysalisController.transform.position).normalized;
        _chrysalisController.RotateToDirection(dir);
        _navMeshAgent.Move(dir * (_chrysalisController.chrysalisUtils.chrysalisStats.chaseSpeed * Time.deltaTime));
    }
}
