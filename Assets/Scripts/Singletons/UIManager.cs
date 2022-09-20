using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [SerializeField] private TextMeshProUGUI speedTimer;
    private void Awake()
    {
        instance = this;
        speedTimer.gameObject.SetActive(false);
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
}
