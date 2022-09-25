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
    private List<Transform> enemyList = new List<Transform>();
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
