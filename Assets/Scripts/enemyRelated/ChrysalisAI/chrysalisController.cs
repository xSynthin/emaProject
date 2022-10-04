using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class chrysalisController : MonoBehaviour
{
    private StateMachine _stateMachine;
    [HideInInspector] public Transform PlayerLocation;
    public EnemyStats ChrysalisStats;
    public ChrysalisUtils chrysalisUtils;
    private ChrysalisFollowPlayer chasePlayer;
    private ChrysalisAttack chrysalisAttack;
    private ChrysalisDistanceKeeper chrysalisDistanceKeeper;
    private void Awake()
    {
        var navMeshAgent = GetComponent<NavMeshAgent>();
        _stateMachine = new StateMachine();
        chasePlayer = new ChrysalisFollowPlayer(this, navMeshAgent);
        chrysalisAttack = new ChrysalisAttack(this, navMeshAgent);
        chrysalisDistanceKeeper = new ChrysalisDistanceKeeper(this, navMeshAgent);
        _stateMachine.AddAnyTransition(chasePlayer, PlayerInSight());
        _stateMachine.AddAnyTransition(chasePlayer, HasBeenAttacked());
        _stateMachine.AddAnyTransition(chrysalisAttack, PlayerInAttackRange());
        _stateMachine.AddAnyTransition(chrysalisDistanceKeeper, PlayerTooClose());
        //void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
    }

    Func<bool> PlayerInSight() => () =>
    {
        RaycastHit hit;
        if (PlayerLocation.position != Vector3.zero)
        {
            RotateToDirection(PlayerLocation.position);
            if (Physics.Raycast(transform.position, (PlayerLocation.position - transform.position).normalized,
                    out hit,
                    ChrysalisStats.lookRange) && hit.collider.CompareTag("Player") &&
                PlayerInAttackRange().Invoke() == false && PlayerTooClose().Invoke() == false)
            {
                return true;
            }
        }

        return false;
    };
    Func<bool> HasBeenAttacked() => () =>
    {
        if (chrysalisUtils.health < ChrysalisStats.health && PlayerInAttackRange().Invoke() == false && PlayerTooClose().Invoke() == false)
            return true;
        return false;
    };
    Func<bool> PlayerTooClose() => () =>
    {
        if (PlayerLocation.position != Vector3.zero)
            if (Vector3.Distance(PlayerLocation.position, transform.position) <= ChrysalisStats.attackRange / 1.3)
                return true;
        return false;
    };
    Func<bool> PlayerInAttackRange() => () =>
    {
        if (PlayerLocation.position != Vector3.zero)
            if (Vector3.Distance(PlayerLocation.position, transform.position) <= ChrysalisStats.attackRange && PlayerTooClose().Invoke() == false)
            {
                return true;
            }

        return false;
    };
    private void Start()
    {
        PlayerLocation = PlayerManager.instance.playerUtils.transform;
    }
    private void Update()
    {
        _stateMachine.Tick();
    }
    public void RotateToDirection(Vector3 direction)
    {
        Vector3 rotation = Quaternion.LookRotation(direction).eulerAngles;
        transform.localEulerAngles = new Vector3(0, rotation.y, 0);
    }
}
