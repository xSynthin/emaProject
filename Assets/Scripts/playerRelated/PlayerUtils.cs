using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUtils : MonoBehaviour
{
    [SerializeField] public int hp;
    [SerializeField] private int damageToTake;
    [SerializeField] public int ammo;
    [SerializeField] internal float reloadTime;
    internal int ammoMax;
    private void Start()
    {
        //ScoreSystem.instance.changePlayerHp += TakeDamage;
        ammoMax = ammo;
    }
    private void TakeDamage() => hp -= damageToTake;
    public void decreaseAmmo(int ammoToDecrease) => ammo -= ammoToDecrease;
}
