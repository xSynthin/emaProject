using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.AI;

public class PlayerScanner : IState
{
    private readonly mothmanController _mothmanController;
    private readonly NavMeshAgent _navMeshAgent;
    public PlayerScanner(mothmanController mothmanController, NavMeshAgent navMeshAgent)
    {
        _mothmanController = mothmanController;
        _navMeshAgent = navMeshAgent;
    }
    public void Tick() {}
    public void OnEnter()
    {
    }
    public void OnExit()
    {
        _mothmanController.PlayerLocation = PlayerManager.instance.playerUtils.transform.position;
    }
}
