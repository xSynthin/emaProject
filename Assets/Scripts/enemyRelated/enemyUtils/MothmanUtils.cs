using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothmanUtils : EnemyUtils
{
    public EnemyStats mothManStats;
    public GameObject EnemyDeathSound;
    [HideInInspector] public float health;
    private void Update()
    {
       DeathCheck(); 
       OnOneShot();
    }

    private void Awake()
    {
        health = mothManStats.health;
    }

    public override void DeathCheck()
    {
        if (health <= 0)
        {
            Death();
            EntitiesManager.instance.CallEnemyDeathEvent();
        }
    }

    public void OnOneShot()
    {
        if (health == 10)
            GetComponent<Renderer>().material.color = Color.red;
    }

    private void Death()
    {
        Instantiate(EnemyDeathSound, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public override void TakeDamage(int hpToTake) => health -= hpToTake;
}
