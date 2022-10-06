using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [Header("Assignables")]
    [SerializeField] private TextMeshProUGUI playerAmmo;
    [SerializeField] private TextMeshProUGUI playerHp;
    public Image SpeedSlider;
    public event Action playerAmmoChange;
    public event Action PlayerHPChange;
    private int ammo;
    private int ammoMax;
    private void Awake()
    {
        instance = this;
        GManager.instance.onGameStartEvent += RepaintPlayerAmmo;
        GManager.instance.onGameStartEvent += RepaintPlayerHP;
    }
    private void Start()
    {
        PlayerHPChange += RepaintPlayerHP;
        playerAmmoChange += RepaintPlayerAmmo;
    }
    private void RepaintPlayerHP()
    {
        playerHp.text = $"HP: {PlayerManager.instance.playerUtils.hp}";
    }

    public void CallPlayerHpChangeEvent() => PlayerHPChange?.Invoke();
    private void RepaintPlayerAmmo()
    {
        playerAmmo.text = $"Ammo: {PlayerManager.instance.playerUtils.ammo}/{PlayerManager.instance.playerUtils.ammoMax}";
    }
    public void CallPlayerAmmoChangeEvent() => playerAmmoChange?.Invoke();
}
