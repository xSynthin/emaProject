using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingEnemy : EnemyUtils
{
    public float health;
    [HideInInspector] public float startHealth;

    private void Start()
    {
        startHealth = health;
    }

    private void Update()
    {
        DeathCheck(); 
    }

    public override void DeathCheck()
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
    }
    public override void TakeDamage(int hpToTake) => health -= hpToTake;
}
