using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPilotage : MonoBehaviour
{
    public bool pilotageManuel;

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

    public void _SetManu(bool val)
    {
        pilotageManuel = val;
    }

    // Update is called once per frame
    void Update()
    {
        if (pilotageManuel)
        {
            timer.moveMobile = false;
            rrROLL.enabled = false;
            rrPAN.enabled = false;

            //robot
            transform.Translate(Vector3.right * stick_G.Y * Time.deltaTime);

            //roll en y haut en sens horaire
            ROLL.transform.Rotate(new Vector3(stick_D.Y * Time.deltaTime * -20, 0, 0));

            //pan en x à droite
            PAN.transform.Rotate(new Vector3(0, 0, stick_D.X * Time.deltaTime * -20));

        }
        else
        {
            timer.moveMobile = true;
            rrROLL.enabled = true;
            rrPAN.enabled = true;
        }
    }
}
