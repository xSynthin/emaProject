using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PFragSpeedMultiplier : MonoBehaviour
{
    [Header("Assignables")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private VisualEffect speedGraph;
    [SerializeField] private float minEffectSpeed;
    [Header("Frag Multiplier Variables")]
    [SerializeField] private float killMultiplier;
    [SerializeField] private float killTimer;
    private float baseMoveSpeed;
    private float radius = 2;
    public PlayerCollections.SpeedStages CurrentSpeedBoost = PlayerCollections.SpeedStages.normal;
    [HideInInspector] public Dictionary<PlayerCollections.SpeedStages, float> SpeedStagesVals = new Dictionary<PlayerCollections.SpeedStages, float>();
    private Dictionary<PlayerCollections.SpeedStages, Color> ColorStages;
    private void Awake()
    {
        baseMoveSpeed = playerController.moveSpeed;
        // this should be cleaned TODO
        //--------------------------------------------------------------------
        SpeedStagesVals.Add(PlayerCollections.SpeedStages.normal, baseMoveSpeed);
        SpeedStagesVals.Add(PlayerCollections.SpeedStages.boosted1, baseMoveSpeed * killMultiplier);
        SpeedStagesVals.Add(PlayerCollections.SpeedStages.boosted2, (baseMoveSpeed * Mathf.Pow(killMultiplier, 2)));
        SpeedStagesVals.Add(PlayerCollections.SpeedStages.boosted3, (baseMoveSpeed * Mathf.Pow(killMultiplier, 3)));
        SpeedStagesVals.Add(PlayerCollections.SpeedStages.boosted4, (baseMoveSpeed * Mathf.Pow(killMultiplier, 4)));
        SpeedStagesVals.Add(PlayerCollections.SpeedStages.boosted5, (baseMoveSpeed * Mathf.Pow(killMultiplier, 5)));
        //---------------------------------------------------------------------
        ColorStages = new Dictionary<PlayerCollections.SpeedStages, Color>()
        {
            {PlayerCollections.SpeedStages.boosted1, Color.cyan},
            {PlayerCollections.SpeedStages.boosted2, Color.yellow},
            {PlayerCollections.SpeedStages.boosted3, Color.cyan},
            {PlayerCollections.SpeedStages.boosted4, Color.red},
            {PlayerCollections.SpeedStages.boosted5, Color.magenta},
        };
    }
    void SpeedBoostState()
    {
        foreach (KeyValuePair<PlayerCollections.SpeedStages, float> entry in SpeedStagesVals)
        {
            if (Math.Abs(playerController.moveSpeed - entry.Value) < 0.001f)
                CurrentSpeedBoost = entry.Key;
        }
    }

    void checkParticles()
    {
        if(playerController.moveSpeed > minEffectSpeed && playerController.moveDirection != Vector3.zero)
            speedGraph.gameObject.SetActive(true);
        else
            speedGraph.gameObject.SetActive(false);
    }
    void Boost()
    {
        boostMoveSpeed();
        SpeedBoostState();
    }
    void UnBoost()
    {
        unBoostMoveSpeed();
        SpeedBoostState();
        PlayerManager.instance.CallBoostStopEvent();
    }
    void Start()
    {
        EntitiesManager.instance.EnemyDeathEvent += Booster;
        speedGraph.gameObject.SetActive(false);
    }

    private void Update()
    {
        checkParticles();
    }

    void Booster()
    {
        if (playerController.moveSpeed < SpeedStagesVals[PlayerCollections.SpeedStages.boosted5])
        {
            StopAllCoroutines();
            StartCoroutine(SpeedBuff());
            UIManager.instance.SpeedSlider.GetComponent<SpeedSlider>().TweenSpeedSlider(killTimer, ColorStages[CurrentSpeedBoost]);
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(MaxSpeedBuff());
            UIManager.instance.SpeedSlider.GetComponent<SpeedSlider>().TweenSpeedSlider(killTimer, ColorStages[CurrentSpeedBoost]);
        }
    }

    IEnumerator MaxSpeedBuff()
    {
        yield return new WaitForSeconds(killTimer);
        UnBoost();
    }
    IEnumerator SpeedBuff()
    {
        Boost();
        yield return new WaitForSeconds(killTimer);
        UnBoost();
    }

    private void boostMoveSpeed()
    {
        playerController.moveSpeed *= killMultiplier;
        playerController.airMultiplier += 0.05f;
    }
    private void unBoostMoveSpeed()
    {
        playerController.moveSpeed = baseMoveSpeed;
        playerController.airMultiplier -= 0.05f;
    }
}
