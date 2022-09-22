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
    public event Action<float> playerDamageTakenEvent;
    private void Awake()
    {
        instance = this;
    }
    public void CallPlayerSpeedBoostActivateEvent() => playerSpeedBoostActivateEvent?.Invoke();
    public void CallPlayerDamageTakenEvent(float dmgToTake) => playerDamageTakenEvent?.Invoke(dmgToTake);
    public void CallPlayerSpeedBoostDeactivateEvent() => playerSpeedBoostDeactivateEvent?.Invoke();
}
