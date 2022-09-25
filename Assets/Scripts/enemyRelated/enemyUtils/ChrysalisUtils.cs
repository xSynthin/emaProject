using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChrysalisUtils : EnemyUtils
{
    public EnemyStats chrysalisStats;
    [HideInInspector] public float health;
    private void Update()
    {
       DeathCheck(); 
    }
    private void Awake()
    {
        health = chrysalisStats.health;
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
