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
    }
    public void OnEnter()
    {
        _navMeshAgent.enabled = true;
        _navMeshAgent.SetDestination(_mothmanController.PlayerLocation);
    }

    public void OnExit()
    {
        _navMeshAgent.enabled = false;
    }
}
