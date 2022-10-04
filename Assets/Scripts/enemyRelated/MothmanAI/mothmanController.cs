using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class mothmanController : MonoBehaviour
{
    private StateMachine _stateMachine;
    [HideInInspector] public Transform PlayerLocation;
    public EnemyStats mothmanStats;
    public Waypoint WaypointTarget { get; set; }
    public MothmanUtils mothmanUtils;
    //public bool wayPointReached { get; set; }
    public float maxDistanceToWaypoint = 20f;
    private MoveToWaypoint moveToWaypoint;
    private ChasePlayer chasePlayer;
    private SearchForWaypoint search;
    private void Awake()
    {
        var navMeshAgent = GetComponent<NavMeshAgent>();
        _stateMachine = new StateMachine();
        chasePlayer = new ChasePlayer(this, navMeshAgent);
        search = new SearchForWaypoint(this);
        var attack = new MothmanAttack(this, navMeshAgent);
        moveToWaypoint = new MoveToWaypoint(this, navMeshAgent);
        At(search, moveToWaypoint, HasWaypoint());
        At(moveToWaypoint, search, StuckForOverASecond());
        _stateMachine.AddAnyTransition(chasePlayer, PlayerInSight());
        _stateMachine.AddAnyTransition(chasePlayer, HasBeenAttacked());
        _stateMachine.AddAnyTransition(attack, PlayerInAttackRange());
        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
        _stateMachine.SetState(search);
    }

    Func<bool> HasBeenAttacked() => () =>
    {
        if (mothmanUtils.health < mothmanStats.health && PlayerInAttackRange().Invoke() == false)
            return true;
        return false;
    };
    Func<bool> PlayerInAttackRange() => () =>
    {
        if (PlayerLocation.position != Vector3.zero)
            if (Vector3.Distance(PlayerLocation.transform.position, transform.position) <= mothmanStats.attackRange)
                return true;
        return false;
    };
    Func<bool> HasWaypoint() => () => WaypointTarget != null;
    Func<bool> StuckForOverASecond() => () => moveToWaypoint.timeStuck > 1f;
    Func<bool> PlayerInSight() => () =>
    {
        RaycastHit hit;
        if (PlayerLocation.position != Vector3.zero)
        {
            RotateToDirection(PlayerLocation.position);
            if (Physics.Raycast(transform.position, (PlayerLocation.position - transform.position).normalized,
                    out hit,
                    mothmanStats.lookRange) && hit.collider.CompareTag("Player") && PlayerInAttackRange().Invoke() == false)
            {
                return true;
            }
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
