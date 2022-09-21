using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private TextMeshProUGUI speedTimer;
    [SerializeField] private TextMeshProUGUI playerAmmo;
    public event Action playerAmmoChange;
    private int ammo;
    private int ammoMax;
    private void Awake()
    {
        instance = this;
        speedTimer.gameObject.SetActive(false);
        playerAmmoChange += RepaintPlayerAmmo;
        GManager.instance.onGameStartEvent += RepaintPlayerAmmo;
    }
    internal IEnumerator CountDownTime(float timeToCount)
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
    private void RepaintPlayerAmmo()
    {
        playerAmmo.text = $"Ammo: {PlayerManager.instance.playerUtils.ammo}/{PlayerManager.instance.playerUtils.ammoMax}";
    }
    public void CallPlayerAmmoChangeEvent() => playerAmmoChange?.Invoke();
}
