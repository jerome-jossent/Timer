using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timer_Fill : MonoBehaviour
{
    public timer timer;
    public TMPro.TMP_InputField if_min;
    public TMPro.TMP_InputField if_sec;

    // Start is called before the first frame update
    void Start()
    {
        timer._SetTime(if_min, if_sec);
    }

}
