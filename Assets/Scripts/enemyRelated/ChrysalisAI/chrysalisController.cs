using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class chrysalisController : MonoBehaviour
{
    private StateMachine _stateMachine;
    [HideInInspector] public Transform PlayerLocation;
    public LineRenderer chrysalisShootTracer;
    public Transform shootPosition;
    public ChrysalisUtils chrysalisUtils;
    private Idle idleChrysalis;
    private Follow followPlayer;
    private Attack attackPlayer;
    public float timeBetweenShots = 0.7f;
    private void Awake()
    {
        var navMeshAgent = GetComponent<NavMeshAgent>();
        idleChrysalis = new Idle();
        followPlayer = new Follow(this, navMeshAgent);
        attackPlayer = new Attack(this);
        _stateMachine = new StateMachine();
        _stateMachine.AddTransition(idleChrysalis, followPlayer,PlayerInSight());
        _stateMachine.AddTransition(idleChrysalis, followPlayer, HasBeenAttacked());
        _stateMachine.AddTransition(idleChrysalis, attackPlayer, PlayerInAttackRange());
        _stateMachine.AddTransition(followPlayer, attackPlayer, PlayerInAttackRange());
        _stateMachine.AddTransition(attackPlayer, followPlayer, PlayerNotInAttackRange());
        _stateMachine.SetState(idleChrysalis);
    }

    Func<bool> PlayerInSight() => () =>
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (PlayerLocation.position - transform.position).normalized,
                out hit,
                chrysalisUtils.chrysalisStats.lookRange) && hit.collider.CompareTag("Player") && PlayerInAttackRange().Invoke() == false)
        {
            return true;
        }
        return false;
    };
    Func<bool> PlayerNotInAttackRange() => () => !PlayerInAttackRange().Invoke();
    Func<bool> HasBeenAttacked() => () =>
    {
        if (chrysalisUtils.health < chrysalisUtils.chrysalisStats.health && PlayerInAttackRange().Invoke() == false)
            return true;
        return false;
    };
    Func<bool> PlayerInAttackRange() => () =>
    {
        if (PlayerLocation.position != Vector3.zero)
            if (Vector3.Distance(PlayerLocation.position, transform.position) <= chrysalisUtils.chrysalisStats.attackRange)
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
    public void SpawnBulletTrail(Vector3 point)
    {
        Vector3 position = shootPosition.position;
        GameObject bulletTrail = Instantiate(chrysalisShootTracer.gameObject, position, Quaternion.identity);
        LineRenderer lineTrail = bulletTrail.GetComponent<LineRenderer>();
        lineTrail.SetPosition(0, position);
        lineTrail.SetPosition(1, point);
        Destroy(bulletTrail, 0.2f);
    }
}
