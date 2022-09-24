using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Assignable")]
    [SerializeField] private GameObject enemyToSpawnPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int enemyCount;
    [SerializeField] private Transform SpawnerActivationPoint;
    [SerializeField] private float spawnerActivationRange;
    [SerializeField] private List<Transform> EnemyWaypointsList;
    private List<Transform> enemyList = new List<Transform>();
    [SerializeField] private bool shouldPatrol;
    

    private void Awake()
    {
        foreach (var waypoint in EnemyWaypointsList)
        {
            enemyToSpawnPrefab.GetComponent<StateController>().wayPointList.Add(waypoint);
        }
        if (!shouldPatrol)
            enemyToSpawnPrefab.GetComponent<StateController>().noPatrol = true;
    }
    void Update()
    {
        SpawnEnemys();
    }
    void SpawnEnemys()
    {
        if(enemyList.Count < enemyCount)
            if(SpawnerActivationPoint.GetComponent<SpawnerActivationCheck>().activate)
                for (int i = 0; i < enemyCount; i++)
                    enemyList.Add(Instantiate(enemyToSpawnPrefab, spawnPoint.position + new Vector3(0,0,i), Quaternion.identity).transform);
    }
}
