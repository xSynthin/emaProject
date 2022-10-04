using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChrysalisAttack : IState
{
    private readonly chrysalisController _chrysalisController;
    private readonly NavMeshAgent _navMeshAgent;

    public ChrysalisAttack(chrysalisController chrysalisController, NavMeshAgent navMeshAgent)
    {
        _chrysalisController = chrysalisController;
        _navMeshAgent = navMeshAgent;
    }
    public void Tick() {}

    public void OnEnter()
    {
        _chrysalisController.GetComponent<Renderer>().material.color = Color.magenta;
    }

    public void OnExit() { }
}
