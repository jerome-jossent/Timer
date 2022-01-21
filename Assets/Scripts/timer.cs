using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class timer : MonoBehaviour
{
    public bool _reverseAtEnd;
    public bool _running;

    public float t_Total_sec = 5;

    public int t_sec;
    public int t_min;

    public float t_elapsed;
    public float t_remaining;

    public TMPro.TMP_Text txt_temps;
    public TMPro.TMP_Text txt_infos;

    public TMPro.TMP_InputField if_min;
    public TMPro.TMP_InputField if_sec;

    public GameObject A;
    public GameObject B;
    public GameObject mobile;
    public bool moveMobile;

    private void Start()
    {
        _Reset();
    }

    bool changing_by_code;
    internal void _SetTime(TMP_InputField if_min, TMP_InputField if_sec)
    {
        if (changing_by_code) return;


        t_min = int.Parse(if_min.text);
        t_sec = int.Parse(if_sec.text);
        t_Total_sec = t_min * 60 + t_sec;
    }

    public void _Run() { _running = true; }
    public void _Pause() { _running = false; }

    public void _AutoReverse_ON() { _reverseAtEnd = true; }
    public void _AutoReverse_OFF() { _reverseAtEnd = false; }

    public void _Reset()
    {
        if(moveMobile)
            mobile.transform.position = A.transform.position;
        t_elapsed = 0;
    }

    public void _SetMinutesSeconds(string val)
    {
        //"00:00"
        string[] ms = val.Split(':');
        _SetMinutes(ms[0]);
        _SetSeconds(ms[1]);
    }

    public void _SetMinutes(string val)
    {
        changing_by_code = true;
        if (val.Length == 1)
            if_min.text = "0" + val;
        else
            if_min.text = val;
        changing_by_code = false;

        t_min = int.Parse(val);
        t_Total_sec = t_min * 60 + t_sec;
    }
    public void _SetSeconds(string val)
    {
        changing_by_code = true;
        if (val.Length == 1)
            if_sec.text = "0" + val;
        else
            if_sec.text = val;
        changing_by_code = false;

        t_sec = int.Parse(val);
        t_Total_sec = t_min * 60 + t_sec;
    }

    void Update()
    {
        if (t_Total_sec <= 0) return;

        if (_running)
        {
            if (t_elapsed > t_Total_sec)
            {
                if (_reverseAtEnd)
                {
                    GameObject temp = A;
                    A = B;
                    B = temp;
                    _Reset();
                }
                else
                {
                    if (moveMobile)
                        mobile.transform.position = B.transform.position;
                }
            }
            else
            {
                t_elapsed += Time.deltaTime;
                t_remaining = t_Total_sec - t_elapsed;

                CoeffCompute(A.transform.position.x, B.transform.position.x,
                             0, t_Total_sec,
                             out float a_x, out float b_x);
                float x = a_x * t_elapsed + b_x;

                CoeffCompute(A.transform.position.y, B.transform.position.y,
                                         0, t_Total_sec,
                                         out float a_y, out float b_y);
                float y = a_y * t_elapsed + b_y;

                CoeffCompute(A.transform.position.z, B.transform.position.z,
                                         0, t_Total_sec,
                                         out float a_z, out float b_z);
                float z = a_z * t_elapsed + b_z;

                if (moveMobile)
                    mobile.transform.position = new Vector3(x, y, z);
            }
        }
        DisplayTimeInfo();
    }

    void DisplayTimeInfo()
    {
        int progression = (int)(t_elapsed / t_Total_sec * 100);
        txt_temps.text = SecToMinSec(t_elapsed) + " - " + SecToMinSec(t_Total_sec);
        txt_infos.text = progression.ToString() + "%";
    }

    public static string SecToMinSec(float timeInSeconds)
    {
        return TimeSpan.FromSeconds(timeInSeconds).ToString(@"m\:ss");
    }

    void CoeffCompute(float y1, float y2, float x1, float x2, out float a, out float b)
    {
        a = (y2 - y1) / (x2 - x1);
        b = y1 - a * x1;
    }
}