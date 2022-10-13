using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class mothmanController : MonoBehaviour
{
    private StateMachine _stateMachine;
    [HideInInspector] public Vector3 PlayerLocation;
    public EnemyStats mothmanStats;
    public MothmanUtils mothmanUtils;
    private ChasePlayer chasePlayer;
    private PlayerScanner playerScanner;
    private MothmanAttack mothmanAttack;
    private NavMeshAgent navMeshAgent;
    private float localHealth;
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        _stateMachine = new StateMachine();
        chasePlayer = new ChasePlayer(this, navMeshAgent);
        mothmanAttack = new MothmanAttack(this, navMeshAgent);
        playerScanner = new PlayerScanner(this, navMeshAgent);
        _stateMachine.AddTransition(playerScanner, chasePlayer, HasBeenAttacked());
        _stateMachine.AddTransition(playerScanner, chasePlayer, PlayerInSight());
        _stateMachine.AddTransition(chasePlayer, playerScanner, PlayerMovedTooMuch());
        _stateMachine.AddTransition(chasePlayer, playerScanner, HasBeenAttacked());
        _stateMachine.AddTransition(chasePlayer,playerScanner, PathAchievied());
        _stateMachine.AddTransition(chasePlayer, mothmanAttack, PlayerInAttackRange());
        _stateMachine.AddTransition(mothmanAttack, chasePlayer, HasBeenAttacked());
        _stateMachine.AddTransition(mothmanAttack, chasePlayer, PlayerInSight());
        _stateMachine.SetState(playerScanner);
    }
    Func<bool> HasBeenAttacked() => () =>
    {
        if (mothmanUtils.health < localHealth)
        {
            localHealth = mothmanUtils.health;
            return true;
        }
        return false;
    };
    Func<bool> PathAchievied() => () =>
    {
        if (Vector3.Distance(PlayerLocation, transform.position) < 0.5f)
        {
            return true;
        }
        return false;
    };
    Func<bool> PlayerMovedTooMuch() => () =>
    {
        if (Vector3.Distance(PlayerLocation, PlayerManager.instance.playerUtils.transform.position) >
            5f)
        {
            return true;
        }
        return false;
    };
    Func<bool> PlayerInAttackRange() => () =>
    {
        if (PlayerManager.instance.playerUtils.transform.position != Vector3.zero)
            if (Vector3.Distance(PlayerManager.instance.playerUtils.transform.position, transform.position) <= mothmanStats.attackRange)
                return true;
        return false;
    };
    Func<bool> PlayerInSight() => () =>
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (PlayerManager.instance.playerUtils.transform.position - transform.position).normalized,
                out hit,
                mothmanStats.lookRange) && hit.collider.CompareTag("Player") && PlayerInAttackRange().Invoke() == false)
        {
            return true;
        }
        return false;
    };

    private void Start()
    {
        PlayerLocation = PlayerManager.instance.playerUtils.transform.position;
        localHealth = mothmanStats.health;
    }

    private void Update()
    {
        //print(_stateMachine._currentState);
        _stateMachine.Tick();
    }
}
