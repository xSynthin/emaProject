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
    public Waypoint WaypointTarget { get; set; }
    public ChrysalisUtils chrysalisUtils;
    [HideInInspector] public MoveToWaypointCH moveToWaypointCh;
    public float maxDistanceToWaypoint = 20f;
    private void Awake()
    {
        var navMeshAgent = GetComponent<NavMeshAgent>();
        _stateMachine = new StateMachine();
        var search = new SearchForWaypointCH(this);
        moveToWaypointCh = new MoveToWaypointCH(this, navMeshAgent);
        At(search, moveToWaypointCh, HasWaypoint());
        At(moveToWaypointCh, search, StuckForOverASecond());
        void At(IState to, IState from, Func<bool> condition) => _stateMachine.AddTransition(to, from, condition);
        _stateMachine.SetState(search);
    }
    Func<bool> HasWaypoint() => () => WaypointTarget != null;
    // CHASE STUFF
    Func<bool> HasBeenAttacked() => () =>
    {
        if (chrysalisUtils.health < ChrysalisStats.health && PlayerInAttackRange().Invoke() == false)
            return true;
        return false;
    };

    Func<bool> PlayerInAttackRange() => () => false;
    Func<bool> StuckForOverASecond() => () => moveToWaypointCh.timeStuck > 1f;
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
