using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothmanUtils : EnemyUtils
{
    public EnemyStats mothManStats;
    private void Update()
    {
       DeathCheck(); 
    }

    public override void DeathCheck()
    {
        if (mothManStats.health <= 0)
        {
            Death();
            EntitiesManager.instance.CallEnemyDeathEvent();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    public override void TakeDamage(int hpToTake) => mothManStats.health -= hpToTake;
}
