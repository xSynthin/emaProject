using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private float speed =10f;
    private LineRenderer lr;
    
    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }
    void Update()
    {
        color.a = Mathf.Lerp(color.a,0, Time.deltaTime * speed);
        lr.startColor = color;
        lr.endColor = color;
    }
}
