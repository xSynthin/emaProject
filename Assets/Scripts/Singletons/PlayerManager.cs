using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    [SerializeField] public PlayerUtils playerUtils;
    [SerializeField] public PFragSpeedMultiplier playerSpeedBoostScript;
    public event Action playerSpeedBoostActivateEvent;
    public event Action playerSpeedBoostDeactivateEvent;
    private void Awake()
    {
        instance = this;
    }
    public void CallPlayerSpeedBoostActivateEvent() => playerSpeedBoostActivateEvent?.Invoke();
    public void CallPlayerSpeedBoostDeactivateEvent() => playerSpeedBoostDeactivateEvent?.Invoke();
}
