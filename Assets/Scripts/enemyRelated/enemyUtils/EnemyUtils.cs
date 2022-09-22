using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyUtils : MonoBehaviour
{
    public abstract void DeathCheck();
    public abstract void TakeDamage(int hpToTake);
}
