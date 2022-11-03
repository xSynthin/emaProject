using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyDeathAudio : MonoBehaviour
{
    public float time = 5f;
    void Start()
    {
        Destroy(this, time);
    }
}
