using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFragSpeedMultiplier : MonoBehaviour
{
    [Header("Assignables")]
    [SerializeField] private PlayerController playerController;
    [Header("Frag Multiplier Variables")]
    [SerializeField] private float killMultiplier;
    [SerializeField] private float killTimer;
    [SerializeField] private float maxAchievableSpeed = 60f;
    private float baseMoveSpeed;

    void Start()
    {
        baseMoveSpeed = playerController.moveSpeed;
        EntitiesManager.instance.EnemyDeathEvent += KillAchevied;
        PlayerManager.instance.playerSpeedBoostActivateEvent += boostMoveSpeed;
        PlayerManager.instance.playerSpeedBoostActivateEvent += boostAirControl;
        PlayerManager.instance.playerSpeedBoostDeactivateEvent += unBoostAirControl;
        PlayerManager.instance.playerSpeedBoostDeactivateEvent += unBoostMoveSpeed;
    }
    void KillAchevied()
    {
        if (playerController.moveSpeed < maxAchievableSpeed)
        {
            StopAllCoroutines();
            StartCoroutine(UIManager.instance.CountDownTime(killTimer));
            StartCoroutine(SpeedBuff());
        }
    }

    IEnumerator SpeedBuff()
    {
        // Dwa eventy na speed buff
        // trzeba zmienic tez predkosc przeladowania
        PlayerManager.instance.CallPlayerSpeedBoostActivateEvent();
        yield return new WaitForSeconds(killTimer);
        PlayerManager.instance.CallPlayerSpeedBoostDeactivateEvent();
    }
    private void boostMoveSpeed() => playerController.moveSpeed *= killMultiplier;
    private void unBoostMoveSpeed() => playerController.moveSpeed = baseMoveSpeed;
    private void boostAirControl() => playerController.airMultiplier += 0.05f;
    private void unBoostAirControl() => playerController.airMultiplier -= 0.05f;
}
