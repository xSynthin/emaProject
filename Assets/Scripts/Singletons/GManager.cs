using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GManager : MonoBehaviour
{
    public static GManager instance;
    private void Awake()
    {
        instance = this;
    }
}
