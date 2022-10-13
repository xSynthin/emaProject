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
    [SerializeField] private TextMeshProUGUI gameTimeUI;
    public Image SpeedSlider;
    public event Action playerAmmoChange;
    public event Action PlayerHPChange;
    private int ammo;
    private int ammoMax;
    private float gameTime;
    private void Awake()
    {
        instance = this;
        GManager.instance.onGameStartEvent += RepaintPlayerAmmo;
        GManager.instance.onGameStartEvent += RepaintPlayerHP;
    }
    public IEnumerator timer()
    {
        while (true)
        {
            gameTime += Time.deltaTime;
            RepaintGameTime();
            yield return null;
        }
    }
    private void Start()
    {
        PlayerHPChange += RepaintPlayerHP;
        StartCoroutine(timer());
        playerAmmoChange += RepaintPlayerAmmo;
    }
    private void RepaintPlayerHP()
    {
        playerHp.text = $"HP: {PlayerManager.instance.playerUtils.hp}";
    }
    private void RepaintGameTime()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(gameTime);
        gameTimeUI.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }

    public void CallPlayerHpChangeEvent() => PlayerHPChange?.Invoke();
    private void RepaintPlayerAmmo()
    {
        playerAmmo.text = $"Ammo: {PlayerManager.instance.playerUtils.ammo}/{PlayerManager.instance.playerUtils.ammoMax}";
    }
    public void CallPlayerAmmoChangeEvent() => playerAmmoChange?.Invoke();
}
