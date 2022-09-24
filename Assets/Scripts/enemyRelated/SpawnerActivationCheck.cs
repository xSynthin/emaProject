using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerActivationCheck : MonoBehaviour
{
    [HideInInspector] public bool activate;

    private void Start()
    {
        activate = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            activate = true;
    }
}
