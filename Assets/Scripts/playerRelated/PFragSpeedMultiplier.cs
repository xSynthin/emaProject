using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFragSpeedMultiplier : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float killMultiplier;
    [SerializeField] private float killTimer;
    private float baseMoveSpeed;

    void Start()
    {
        baseMoveSpeed = playerController.moveSpeed;
        EntitiesManager.instance.EnemyDeathEvent += KillAchevied;
    }
    void KillAchevied()
    {
        StopAllCoroutines();
        StartCoroutine(UIManager.instance.CountDownTime(killTimer));
        StartCoroutine(SpeedBuff());
    }

    IEnumerator SpeedBuff()
    {
        playerController.moveSpeed *= killMultiplier;
        yield return new WaitForSeconds(killTimer);
        playerController.moveSpeed = baseMoveSpeed;
    }
}
