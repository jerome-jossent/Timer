using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    public TMPro.TMP_Text text;
    public float period_sec;

    void Update()
    {
        text.color = Color.Lerp(Color.green, Color.blue, Mathf.PingPong(Time.time, period_sec));
    }
}

