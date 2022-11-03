using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MothmanBulletMagnetism : EnemyUtils 
{
    public GameObject mothmanUtils;

    public override void TakeDamage(int hpToTake)
    {
        mothmanUtils.GetComponent<MothmanUtils>().TakeDamage(hpToTake);
    }
    public override void DeathCheck(){}
}
