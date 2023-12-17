using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyAnimation : MonoBehaviour
{
    private float amplitude = 0.1f;
    private float frequency = 3.0f;
    private Vector3 initPos;

    void Start()
    {
        initPos = transform.position;
    }


    // Update is called once per frame
    void Update()
    {

        transform.position = new Vector3(initPos.x, Mathf.Sin(Time.time * frequency) * amplitude + initPos.y, initPos.z);
    }

    
}
