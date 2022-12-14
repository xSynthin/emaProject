using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    [SerializeField] public PlayerUtils playerUtils;
    [SerializeField] public PFragSpeedMultiplier playerSpeedBoostScript;
    public event Action OnBoostStopEvent;
    private void Awake()
    {
        instance = this;
    }
    public void CallBoostStopEvent() => OnBoostStopEvent?.Invoke();
}
