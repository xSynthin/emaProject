using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChrysalisBulletMagnetism : EnemyUtils
{
    public GameObject chrysalisUtils;
    public override void TakeDamage(int hpToTake)
    {
        chrysalisUtils.GetComponent<ChrysalisUtils>().TakeDamage(hpToTake);
    }
    public override void DeathCheck(){}
}

