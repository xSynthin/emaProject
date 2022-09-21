using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUtils : MonoBehaviour
{
    [SerializeField] public int hp;
    [SerializeField] private int damageToTake;
    [SerializeField] public int ammo;
    [SerializeField] public float reloadTime;
    internal int ammoMax;
    internal float defaultReloadTime;
    private void Start()
    {
        //ScoreSystem.instance.changePlayerHp += TakeDamage;
        defaultReloadTime = reloadTime;
        ammoMax = ammo;
    }
    private void TakeDamage() => hp -= damageToTake;
    public void decreaseAmmo(int ammoToDecrease) => ammo -= ammoToDecrease;
}
