using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPilotage : MonoBehaviour
{
    public bool pilotageChariotManuel;
    public bool pilotageCameraManuel;

    public timer timer;
    public Touch_Stick_Analog stick_G;
    public Touch_Stick_Analog stick_D;

    public GameObject ROLL;
    public GameObject PAN;

    RandomRotation rrROLL;
    RandomRotation rrPAN;

    // Start is called before the first frame update
    void Start()
    {
        rrROLL = ROLL.GetComponent<RandomRotation>();
        rrPAN = PAN.GetComponent<RandomRotation>();
    }

    public void _SetChariotManu(bool val)
    {
        pilotageChariotManuel = val;
    }
    public void _SetCameraManu(bool val)
    {
        pilotageCameraManuel = val;
    }

    // Update is called once per frame
    void Update()
    {
        if (pilotageChariotManuel)
        {
            timer.moveMobile = false;            
            //robot
            transform.Translate(Vector3.right * stick_G.Y * Time.deltaTime);
        }
        else
        {
            timer.moveMobile = true;
        }

        if (pilotageCameraManuel)
        {
            rrROLL.enabled = false;
            rrPAN.enabled = false;
            //roll en y haut en sens horaire
            ROLL.transform.Rotate(new Vector3(stick_D.Y * Time.deltaTime * -20, 0, 0));

            //pan en x à droite
            PAN.transform.Rotate(new Vector3(0, 0, stick_D.X * Time.deltaTime * -20));
        }
        else
        {
            rrROLL.enabled = true;
            rrPAN.enabled = true;
        }
    }
}
