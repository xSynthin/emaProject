using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    [SerializeField] public PlayerUtils playerUtils;
    [SerializeField] public PFragSpeedMultiplier playerSpeedBoostScript;
    public event Action<float> playerDamageTakenEvent;
    public event Action PlayerDeathEvent;
    public event Action OnBoostStopEvent;
    private void Awake()
    {
        instance = this;
    }
    public void CallPlayerDamageTakenEvent(float dmgToTake) => playerDamageTakenEvent?.Invoke(dmgToTake);
    public void CallPlayerDeathEvent() => PlayerDeathEvent?.Invoke();
    public void CallBoostStopEvent() => OnBoostStopEvent?.Invoke();
}
