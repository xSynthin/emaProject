using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitiesManager : MonoBehaviour
{
    public static EntitiesManager instance;
    public event Action EnemyDeathEvent;
    private void Awake()
    {
        instance = this;
    }
    public void CallEnemyDeathEvent() => EnemyDeathEvent?.Invoke();
}
