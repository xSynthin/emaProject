using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SpeedSlider : MonoBehaviour
{
    public void Start()
    {
        gameObject.SetActive(false);
    }

    public void TweenSpeedSlider(float timeDuration, Color color)
    {
        DOTween.KillAll();
        transform.localScale = new Vector3(1, 1, 1);
        GetComponent<Image>().color = color;
        gameObject.SetActive(true);
        transform.DOScaleX(0, timeDuration).OnComplete(() =>
        {
            gameObject.SetActive(false);
            transform.localScale = new Vector3(1, 1, 1);
        });
    }
}
