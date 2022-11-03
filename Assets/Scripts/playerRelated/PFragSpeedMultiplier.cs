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
    private float baseMoveSpeed;
    private float baseAirSpeed;
    public PlayerCollections.SpeedStages CurrentSpeedBoost = PlayerCollections.SpeedStages.normal;
    [HideInInspector] public Dictionary<PlayerCollections.SpeedStages, float> SpeedStagesVals = new Dictionary<PlayerCollections.SpeedStages, float>();
    [HideInInspector] public Dictionary<PlayerCollections.SpeedStages, float> airStagesVals = new Dictionary<PlayerCollections.SpeedStages, float>();
    private Dictionary<PlayerCollections.SpeedStages, Color> ColorStages;
    private List<PlayerCollections.SpeedStages> speedStages = new List<PlayerCollections.SpeedStages>()
    {
        PlayerCollections.SpeedStages.normal,
        PlayerCollections.SpeedStages.boosted1,
        PlayerCollections.SpeedStages.boosted2,
        PlayerCollections.SpeedStages.boosted3,
        PlayerCollections.SpeedStages.boosted4,
        PlayerCollections.SpeedStages.boosted5,
    };
    public float[] timerArray = new float[5];
    private Dictionary<PlayerCollections.SpeedStages, float> speedStageTimerDict = new Dictionary<PlayerCollections.SpeedStages, float>()
    {
        {PlayerCollections.SpeedStages.normal, 0},
        {PlayerCollections.SpeedStages.boosted1, 0},
        {PlayerCollections.SpeedStages.boosted2, 0},
        {PlayerCollections.SpeedStages.boosted3, 0},
        {PlayerCollections.SpeedStages.boosted4, 0},
        {PlayerCollections.SpeedStages.boosted5, 0},
    };
    public AudioClip boostUP1;
    public AudioClip boostUP2;
    public AudioClip boostUP3;
    public AudioClip BoostDown;
    [SerializeField] private GameObject uniClipSpeaker;
    [SerializeField] private float finalSpeedBoost = 120f;
    private void Awake()
    {
        baseMoveSpeed = playerController.moveSpeed;
        baseAirSpeed = playerController.airMultiplier;
        // this should be cleaned TODO
        //--------------------------------------------------------------------
        SpeedStagesVals.Add(PlayerCollections.SpeedStages.normal, baseMoveSpeed);
        SpeedStagesVals.Add(PlayerCollections.SpeedStages.boosted1, baseMoveSpeed * killMultiplier);
        SpeedStagesVals.Add(PlayerCollections.SpeedStages.boosted2, (baseMoveSpeed * Mathf.Pow(killMultiplier, 2)));
        SpeedStagesVals.Add(PlayerCollections.SpeedStages.boosted3, (baseMoveSpeed * Mathf.Pow(killMultiplier, 3)));
        SpeedStagesVals.Add(PlayerCollections.SpeedStages.boosted4, (baseMoveSpeed * Mathf.Pow(killMultiplier, 4)));
        SpeedStagesVals.Add(PlayerCollections.SpeedStages.boosted5, (baseMoveSpeed * Mathf.Pow(killMultiplier, 5)));
        //---------------------------------------------------------------------
        airStagesVals.Add(PlayerCollections.SpeedStages.normal, baseAirSpeed);
        airStagesVals.Add(PlayerCollections.SpeedStages.boosted1, baseAirSpeed * 1.10f);
        airStagesVals.Add(PlayerCollections.SpeedStages.boosted2, baseAirSpeed * 1.15f);
        airStagesVals.Add(PlayerCollections.SpeedStages.boosted3, baseAirSpeed * 1.25f);
        airStagesVals.Add(PlayerCollections.SpeedStages.boosted4, baseAirSpeed * 1.30f);
        airStagesVals.Add(PlayerCollections.SpeedStages.boosted5, baseAirSpeed * 1.35f);
        // this should be also cleaned
        speedStageTimerDict[PlayerCollections.SpeedStages.boosted1] = timerArray[0];
        speedStageTimerDict[PlayerCollections.SpeedStages.boosted2] = timerArray[1];
        speedStageTimerDict[PlayerCollections.SpeedStages.boosted3] = timerArray[2];
        speedStageTimerDict[PlayerCollections.SpeedStages.boosted4] = timerArray[3];
        speedStageTimerDict[PlayerCollections.SpeedStages.boosted5] = timerArray[4];
        ColorStages = new Dictionary<PlayerCollections.SpeedStages, Color>()
        {
            {PlayerCollections.SpeedStages.boosted1, Color.cyan},
            {PlayerCollections.SpeedStages.boosted2, Color.yellow},
            {PlayerCollections.SpeedStages.boosted3, Color.green},
            {PlayerCollections.SpeedStages.boosted4, Color.red},
            {PlayerCollections.SpeedStages.boosted5, Color.magenta},
        };
    }
    void checkParticles()
    {
        if(playerController.moveSpeed > minEffectSpeed && playerController.moveDirection != Vector3.zero)
            speedGraph.gameObject.SetActive(true);
        else
            speedGraph.gameObject.SetActive(false);
    }
    void Start()
    {
        EntitiesManager.instance.EnemyDeathEvent += Booster;
        speedGraph.gameObject.SetActive(false);
    }
    //
    private void Update()
    {
        checkParticles();
    }

    private IEnumerator UnBoost()
    {
        if (CurrentSpeedBoost != PlayerCollections.SpeedStages.normal)
        {
            GameObject speaker = Instantiate(uniClipSpeaker, transform.position, transform.rotation);
            speaker.GetComponent<UniversalClipSpeaker>().PlayCLip(BoostDown);
            CurrentSpeedBoost = speedStages[speedStages.IndexOf(CurrentSpeedBoost) - 1];
            float currentWaitTime = speedStageTimerDict[CurrentSpeedBoost];
            if (CurrentSpeedBoost != PlayerCollections.SpeedStages.normal){
                UIManager.instance.SpeedSlider.GetComponent<SpeedSlider>().TweenSpeedSlider(currentWaitTime, ColorStages[CurrentSpeedBoost]);
            }
            setSpeed(CurrentSpeedBoost);
            yield return new WaitForSeconds(currentWaitTime);
            StartCoroutine(UnBoost());
        }
    }

    private IEnumerator Boost()
    {
        if (CurrentSpeedBoost != PlayerCollections.SpeedStages.boosted5)
        {
            CurrentSpeedBoost = speedStages[speedStages.IndexOf(CurrentSpeedBoost) + 1];
            if (CurrentSpeedBoost is > PlayerCollections.SpeedStages.boosted2 and < PlayerCollections.SpeedStages.boosted4)
            {
                // src.clip = boostUP2;
                GameObject speaker = Instantiate(uniClipSpeaker, transform.position, transform.rotation);
                speaker.GetComponent<UniversalClipSpeaker>().PlayCLip(boostUP1);
            }
            else if (CurrentSpeedBoost > PlayerCollections.SpeedStages.boosted3)
            {
                // src.clip = boostUP3;
                GameObject speaker = Instantiate(uniClipSpeaker, transform.position, transform.rotation);
                speaker.GetComponent<UniversalClipSpeaker>().PlayCLip(boostUP1);
            }
            else
            {
                // src.clip = boostUP1;
                GameObject speaker = Instantiate(uniClipSpeaker, transform.position, transform.rotation);
                speaker.GetComponent<UniversalClipSpeaker>().PlayCLip(boostUP1);
            }

            if (CurrentSpeedBoost == PlayerCollections.SpeedStages.boosted5)
            {
                playerController.rb.AddForce(playerController.moveDirection.normalized * finalSpeedBoost, ForceMode.Impulse);
            }
            float currentWaitTime = speedStageTimerDict[CurrentSpeedBoost];
            UIManager.instance.SpeedSlider.GetComponent<SpeedSlider>().TweenSpeedSlider(currentWaitTime, ColorStages[CurrentSpeedBoost]);
            setSpeed(CurrentSpeedBoost);
            yield return new WaitForSeconds(currentWaitTime);
            StartCoroutine(UnBoost());
        }
        else
        {
            // src.clip = boostUP3;
            GameObject speaker = Instantiate(uniClipSpeaker, transform.position, transform.rotation);
            speaker.GetComponent<UniversalClipSpeaker>().PlayCLip(boostUP1);
            float currentWaitTime = speedStageTimerDict[CurrentSpeedBoost];
            UIManager.instance.SpeedSlider.GetComponent<SpeedSlider>().TweenSpeedSlider(currentWaitTime, ColorStages[CurrentSpeedBoost]);
            yield return new WaitForSeconds(currentWaitTime);
            StartCoroutine(UnBoost());
        }
    }
    void Booster()
    {
        StopAllCoroutines();
        StartCoroutine(Boost());
    }
    private void setSpeed(PlayerCollections.SpeedStages speedVal)
    {
        playerController.moveSpeed = SpeedStagesVals[CurrentSpeedBoost];
        playerController.airMultiplier = airStagesVals[CurrentSpeedBoost];
    }
}
