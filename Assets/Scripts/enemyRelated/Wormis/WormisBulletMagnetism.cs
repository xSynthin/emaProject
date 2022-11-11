using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormisBulletMagnetism : EnemyUtils
{
    public GameObject WormisUtils;
    public override void TakeDamage(int hpToTake)
    {
        WormisUtils.GetComponent<Wormis>().TakeDamage(hpToTake);
    }
    public override void DeathCheck(){}
}
