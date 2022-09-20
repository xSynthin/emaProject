using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingEnemy : MonoBehaviour
{
    [SerializeField] internal int health = 100;
    internal int startHealth;

    private void Start()
    {
        startHealth = health;
    }

    private void Update()
    {
        CheckIfDead(); 
    }

    private void CheckIfDead()
    {
        if (health <= 0)
        {
            Death();
            EntitiesManager.instance.CallEnemyDeathEvent();
        }
    }
    private void Death()
    {
        gameObject.SetActive(false);
        // Destroy(gameObject);
    }
    public void TakeDamage(int hpToTake) => health -= hpToTake;
}
