using System;
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
    private float baseMoveSpeed;
    public PlayerCollections.SpeedStages CurrentSpeedBoost = PlayerCollections.SpeedStages.normal;
    [HideInInspector] public Dictionary<PlayerCollections.SpeedStages, float> SpeedStagesVals = new Dictionary<PlayerCollections.SpeedStages, float>();
    private void Awake()
    {
        // this should be cleaned TODO
        //--------------------------------------------------------------------
        SpeedStagesVals.Add(PlayerCollections.SpeedStages.normal, playerController.moveSpeed);
        SpeedStagesVals.Add(PlayerCollections.SpeedStages.boosted1, playerController.moveSpeed * killMultiplier);
        SpeedStagesVals.Add(PlayerCollections.SpeedStages.boosted2, (playerController.moveSpeed * Mathf.Pow(killMultiplier, 2)));
        SpeedStagesVals.Add(PlayerCollections.SpeedStages.boosted3, (playerController.moveSpeed * Mathf.Pow(killMultiplier, 3)));
        SpeedStagesVals.Add(PlayerCollections.SpeedStages.boosted4, (playerController.moveSpeed * Mathf.Pow(killMultiplier, 4)));
        SpeedStagesVals.Add(PlayerCollections.SpeedStages.boosted5, (playerController.moveSpeed * Mathf.Pow(killMultiplier, 5)));
        //---------------------------------------------------------------------
    }
    void SpeedBoostState()
    {
        foreach (KeyValuePair<PlayerCollections.SpeedStages, float> entry in SpeedStagesVals)
        {
            if (Math.Abs(playerController.moveSpeed - entry.Value) < 0.001f)
                CurrentSpeedBoost = entry.Key;
        }
    }
    void Start()
    {
        baseMoveSpeed = playerController.moveSpeed;
        EntitiesManager.instance.EnemyDeathEvent += KillAchevied;
        PlayerManager.instance.playerSpeedBoostActivateEvent += boostMoveSpeed;
        PlayerManager.instance.playerSpeedBoostActivateEvent += boostAirControl;
        PlayerManager.instance.playerSpeedBoostDeactivateEvent += unBoostAirControl;
        PlayerManager.instance.playerSpeedBoostDeactivateEvent += unBoostMoveSpeed;
        PlayerManager.instance.playerSpeedBoostActivateEvent += SpeedBoostState;
        PlayerManager.instance.playerSpeedBoostDeactivateEvent += SpeedBoostState;
    }
    void KillAchevied()
    {
        if (playerController.moveSpeed < SpeedStagesVals[PlayerCollections.SpeedStages.boosted5])
        {
            StopAllCoroutines();
            StartCoroutine(UIManager.instance.CountDownTime(killTimer));
            StartCoroutine(SpeedBuff());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(UIManager.instance.CountDownTime(killTimer));
            StartCoroutine(MaxSpeedBuff());
        }
    }

    IEnumerator MaxSpeedBuff()
    {
        yield return new WaitForSeconds(killTimer);
        PlayerManager.instance.CallPlayerSpeedBoostDeactivateEvent();
    }
    IEnumerator SpeedBuff()
    {
        PlayerManager.instance.CallPlayerSpeedBoostActivateEvent();
        yield return new WaitForSeconds(killTimer);
        PlayerManager.instance.CallPlayerSpeedBoostDeactivateEvent();
    }
    private void boostMoveSpeed() => playerController.moveSpeed *= killMultiplier;
    private void unBoostMoveSpeed() => playerController.moveSpeed = baseMoveSpeed;
    private void boostAirControl() => playerController.airMultiplier += 0.05f;
    private void unBoostAirControl() => playerController.airMultiplier -= 0.05f;
}
