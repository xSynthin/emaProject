using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class MothmanAttack : IState
{
    private readonly mothmanController _mothmanController;
    private readonly NavMeshAgent _navMeshAgent;

    public MothmanAttack(mothmanController mothmanController, NavMeshAgent navMeshAgent)
    {
        _mothmanController = mothmanController;
        _navMeshAgent = navMeshAgent;
    }
    public void Tick() {}
    public void OnEnter()
    {
        _navMeshAgent.enabled = true;
        //_navMeshAgent.Move(-_mothmanController.transform.forward * 5f);
    }
    public void OnExit()
    {
        _navMeshAgent.enabled = false;
    }
}
