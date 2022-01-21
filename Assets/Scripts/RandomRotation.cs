using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    Vector3 angleInit;
    public Vector3 Min;
    public Vector3 Max;

    public float speed_deg_by_sec = 10;
    public float time_between_random = 1;

    float nextPosition_time;
    Quaternion _targetRotation;

    // Start is called before the first frame update
    void Start()
    {
        nextPosition_time = time_between_random;
        angleInit = transform.localRotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextPosition_time)
        {
            nextPosition_time += time_between_random;

            Vector3 angles = angleInit + new Vector3(Random.Range(Min.x, Max.x), Random.Range(Min.y, Max.y), Random.Range(Min.z, Max.z));

            _targetRotation = Quaternion.Euler(angles);

        }



        // Turn towards our target rotation.
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, _targetRotation, speed_deg_by_sec * Time.deltaTime);


    }
}
