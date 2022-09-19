using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingEnemy : MonoBehaviour
{
    [SerializeField] private int health = 100;
    private void Update()
    {
       CheckIfDead(); 
    }
    private void CheckIfDead()
    {
        if (health <= 0)
        {
            EntitiesManager.instance.CallEnemyDeathEvent();
            Death();
        }
    }
    private void Death()
    {
        Destroy(gameObject);
    }
    public void TakeDamage(int hpToTake) => health -= hpToTake;
}
