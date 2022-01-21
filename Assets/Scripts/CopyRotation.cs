using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyRotation : MonoBehaviour
{
    public GameObject Original;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Original.transform.rotation;
    }
}
