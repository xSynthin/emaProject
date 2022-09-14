using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager instance;
    public event Action<string> PlayerShootEvent;
    public event Action<string> PlayerReloadEvent;
    private void Awake()
    {
        instance = this;
    }
    public void CallPlayerShotEvent(string aName) => PlayerShootEvent?.Invoke(aName);
    public void CallPlayerReloadEvent(string aName) => PlayerReloadEvent?.Invoke(aName);
}
