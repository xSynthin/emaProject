using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [Header("Assignables")]
    [SerializeField] public TextMeshProUGUI speedTimer;
    [SerializeField] private TextMeshProUGUI playerAmmo;
    [SerializeField] private TextMeshProUGUI speedBoostStage;
    public event Action playerAmmoChange;
    private int ammo;
    private int ammoMax;
    // this enum reference needs some thinking about
    private Dictionary<PlayerCollections.SpeedStages, Color> SpeedBoostColors;
    private void Awake()
    {
        SpeedBoostColors = new Dictionary<PlayerCollections.SpeedStages, Color>
        {
            {PlayerCollections.SpeedStages.normal, Color.grey},
            {PlayerCollections.SpeedStages.boosted1, Color.cyan},
            {PlayerCollections.SpeedStages.boosted2, Color.blue},
            {PlayerCollections.SpeedStages.boosted3, Color.yellow},
            {PlayerCollections.SpeedStages.boosted4, Color.red},
            {PlayerCollections.SpeedStages.boosted5, Color.magenta},
        };
        instance = this;
        speedTimer.gameObject.SetActive(false);
        playerAmmoChange += RepaintPlayerAmmo;
        GManager.instance.onGameStartEvent += RepaintPlayerAmmo;
        GManager.instance.onGameStartEvent += SetUiSpeedBoostColor;
    }
    private void Start()
    {
        PlayerManager.instance.playerSpeedBoostActivateEvent += SetUiSpeedBoostColor;
        PlayerManager.instance.playerSpeedBoostDeactivateEvent += SetUiSpeedBoostColor;
    }
    public IEnumerator CountDownTime(float timeToCount)
    {
        speedTimer.gameObject.SetActive(true);
        for (float i = timeToCount; i > 0; i -= Time.deltaTime)
        {
            speedTimer.text = $"SpeedBoostTime: {System.Math.Round(i, 2)}";
            yield return null;
        }
        speedTimer.gameObject.SetActive(false);
        yield return null;
    }
    private void SetUiSpeedBoostColor()
    {
        speedBoostStage.color = SpeedBoostColors[PlayerManager.instance.playerSpeedBoostScript.CurrentSpeedBoost];
    }
    private void RepaintPlayerAmmo()
    {
        playerAmmo.text = $"Ammo: {PlayerManager.instance.playerUtils.ammo}/{PlayerManager.instance.playerUtils.ammoMax}";
    }
    public void CallPlayerAmmoChangeEvent() => playerAmmoChange?.Invoke();
}
