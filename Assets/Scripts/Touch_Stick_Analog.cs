using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch_Stick_Analog : MonoBehaviour
{
    [SerializeField] GameObject FIXE;
    [SerializeField] GameObject HEAD;
    RectTransform rectTransform;
    int touchIndex; // du bon doigt
    [SerializeField] Vector2 impossibleTouch = new Vector2(-1, -1);
    [SerializeField] Vector2 FirstTouch;

    public bool TOUCH;
    public float angle;
    public float amplitude;
    public float X, Y;

    public bool invertY;
    float sensibility;

    void Start()
    {
        FirstTouch = impossibleTouch;
        rectTransform = FIXE.GetComponent<RectTransform>();
    }

    public void _TOUCH() { TOUCH = true; }
    public void _UNTOUCH() { TOUCH = false; }

    void Update()
    {
        if (TOUCH)
        {
            if (FirstTouch == impossibleTouch)
            {
                if (Input.touchCount == 0)
                    //souris
                    FirstTouch = Input.mousePosition;
                else
                {
                    //trouver le bon doigt !
                    for (int i = 0; i < Input.touchCount; i++)
                    {
                        Touch touchtmp = Input.GetTouch(i);
                        if (DoigtSurBouton(touchtmp.position))
                        {
                            touchIndex = i;
                            FirstTouch = Input.GetTouch(touchIndex).position;
                            break;
                        }
                    }
                }
            }

            Vector2 p;
            if (Input.touchCount == 0)
                //souris
                p = Input.mousePosition;
            else
                p = Input.GetTouch(touchIndex).position;

            float x = p.x - FirstTouch.x;
            float y = p.y - FirstTouch.y;
            angle = Mathf.Atan2(y, x) * 180 / 3.14f;
            amplitude = 2 * Vector2.Distance(FirstTouch, p);
            amplitude = Mathf.Clamp(amplitude, 0, 112);

            X = Mathf.Cos(angle * Mathf.PI / 180);
            Y = Mathf.Sin(angle * Mathf.PI / 180);
        }
        else
        {
            FirstTouch = impossibleTouch;
            angle = 0;
            amplitude = 0;
            X = 0;
            Y = 0;
        }

        HEAD.transform.localPosition = new Vector3(X * amplitude, Y * amplitude, 0);

        if (amplitude < 40)
        {
            X = 0;
            Y = 0;
        }
        else
        {
            float a = 1 / (1 - 0.3f);
            float b = 1 - a;
            X = X * a + ((X > 0) ? b : -b);
            Y = Y * a + ((Y > 0) ? b : -b);
        }
        if (invertY)
            Y = -Y;
    }

    bool DoigtSurBouton(Vector2 position)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, position, null, out Vector2 s);
        return rectTransform.rect.Contains(s);
    }
}