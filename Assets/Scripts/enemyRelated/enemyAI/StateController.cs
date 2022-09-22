using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    public State currentState;
    public EnemyStats EnemyStats;
    public Transform eyes;
    public State remainState;
    public float patrolSpeed;
    public float chaseSpeed;
    [HideInInspector] public NavMeshAgent NavMeshAgent;
    public List<Transform> wayPointList;
    [HideInInspector] public int nextWayPoint;
    [HideInInspector] public Transform chaseTarget;
    private bool aiActive;
    [HideInInspector] public float localHealth;

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        aiActive = true;
    }

    private void Start()
    {
        localHealth = PlayerManager.instance.playerUtils.hp;
    }

    public void SetupAI(bool aiActivationFromEnemyManger, List<Transform> waypointsFromEnemyManager)
    {
        wayPointList = waypointsFromEnemyManager;
        aiActive = aiActivationFromEnemyManger;
        if (aiActive)
            NavMeshAgent.enabled = true;
        else
            NavMeshAgent.enabled = false;
    }
    private void Update()
    {
        if (!aiActive)
            return;
        currentState.UpdateState(this);
    }

    private void OnDrawGizmos()
    {
        if (currentState != null && eyes != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(eyes.position, EnemyStats.lookSphereCastRadius);
        }
    }

    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
            currentState = nextState;
    }
}
