using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wormis : EnemyUtils
{
    [HideInInspector] public float health;
    public EnemyStats wormisStats;
    private void Awake()
    {
        health = wormisStats.health;
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
        Destroy(gameObject);
    }
    public override void TakeDamage(int hpToTake)
    {
        health -= hpToTake;
    }
}
