using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothmanUtils : EnemyUtils
{
    public EnemyStats mothManStats;
    [HideInInspector] public float health;
    public void Start()
    {
        health = mothManStats.health;
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

    public override void TakeDamage(int hpToTake) => health -= hpToTake;
}
