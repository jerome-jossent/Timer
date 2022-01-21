using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPerimeterTranslation : MonoBehaviour
{
    float perimetre = Mathf.PI * 0.71586f;
    Vector3 positionPrec;

    void Start()
    {
        positionPrec = transform.position;
    }

    void Update()
    {
        //
        float d = Vector3.Distance(transform.position, positionPrec);

        //sens (fonctionne quand position reste au dessus de 0 tout le temps)
        float a1 = Vector3.Magnitude(transform.position);
        float a2 = Vector3.Magnitude(positionPrec);
        float a = a1 - a2;
        float s = (a > 0)?-1:1;
        //Debug.Log(a1.ToString("0.00") + "       " + a2.ToString("0.00") + "   =>  " + s);

        //1 d => 1 p => 360° de rotation
        float rot = s * 360f / perimetre * d;
        transform.Rotate(0, rot, 0);

        positionPrec = transform.position;
    }
}
