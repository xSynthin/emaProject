using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUtils : MonoBehaviour
{
    [SerializeField] public float hp;
    [SerializeField] public int ammo;
    [SerializeField] public float reloadTime;
    [HideInInspector] public int ammoMax;
    [HideInInspector] public float defaultReloadTime;
    private void Start()
    {
        //ScoreSystem.instance.changePlayerHp += TakeDamage;
        PlayerManager.instance.playerDamageTakenEvent += TakeDamage;
        defaultReloadTime = reloadTime;
        ammoMax = ammo;
    }
    private void TakeDamage(float dmgToTake) => hp -= dmgToTake;
    public void decreaseAmmo(int ammoToDecrease) => ammo -= ammoToDecrease;
}
