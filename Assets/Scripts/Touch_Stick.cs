using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch_Stick : MonoBehaviour
{

    [SerializeField] GameObject FIXE;
    [SerializeField] GameObject HEAD;
    RectTransform rectTransform;
    int touchIndex; // du bon doigt
    [SerializeField] Vector2 impossibleTouch = new Vector2(-1, -1);
    [SerializeField] Vector2 FirstTouch;

    public bool TOUCH;
    public float angle;
    public float x, y;
    public bool invertY;
    float sensibility;


    void Start()
    {
        FIXE.SetActive(false);
        HEAD.SetActive(false);
        FirstTouch = impossibleTouch;
        sensibility = 0.002f;
        rectTransform = HEAD.GetComponent<RectTransform>();

        INITButtons();
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
                {
                    //souris
                    FirstTouch = Input.mousePosition;
                }
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
            if (Input.touchCount == 0)
            {
                //souris
                Vector2 p = Input.mousePosition;
                x = (p.x - FirstTouch.x) * sensibility;
                y = (p.y - FirstTouch.y) * sensibility;
            }
            else
            {
                Vector2 p = Input.GetTouch(touchIndex).position;
                x = (p.x - FirstTouch.x) * sensibility;
                y = (p.y - FirstTouch.y) * sensibility;
            }
            angle = Mathf.Atan2(y, x) * 180 / 3.14f;
            if (invertY)
                y = -y;
        }
        else
        {
            FirstTouch = impossibleTouch;
            x = 0;
            y = 0;
            angle = 0;
        }
        if (FirstTouch != impossibleTouch)
        {
            float X = 112 * Mathf.Cos(angle * Mathf.PI / 180);
            float Y = 112 * Mathf.Sin(angle * Mathf.PI / 180);
            HEAD.transform.localPosition = new Vector3(X, Y, 0);
        }
    }

    bool DoigtSurBouton(Vector2 position)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, position, null, out Vector2 s);
        return rectTransform.rect.Contains(s);
    }
    void INITButtons()
    {
        FIXE.SetActive(true);
        HEAD.SetActive(true);
    }
}