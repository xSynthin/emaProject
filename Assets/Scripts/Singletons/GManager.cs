using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager instance;
    public event Action onGameStartEvent;
    public event Action GameRestart;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        onGameStartEvent?.Invoke();
    }
}
