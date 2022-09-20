using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject tEnemyPrefab;
    [SerializeField] private List<Vector3> enemiesPositions;
    private List<GameObject> currentEnemies = new List<GameObject>();

    private void Start()
    {
        spawnBalls();
        EntitiesManager.instance.EnemyDeathEvent += CheckDeathStatus;
    }

    void CheckDeathStatus()
    {
        StartCoroutine(SpawnDelay());
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(3f);
        foreach (var enemy in currentEnemies)
        {
            if (!enemy.activeSelf)
            {
                GetRandomColor(enemy);
                enemy.SetActive(true);
                enemy.GetComponent<TestingEnemy>().health = enemy.GetComponent<TestingEnemy>().startHealth;
            }
        }
    }

    void spawnBalls()
    {
        int index = 0;
        foreach (var enemy in enemiesPositions)
        { 
            currentEnemies.Add(Instantiate(tEnemyPrefab, enemy, Quaternion.identity));
            GetRandomColor(currentEnemies[index++]);
        }
    }

    void GetRandomColor(GameObject entity)
    {
        entity.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    } 
}
