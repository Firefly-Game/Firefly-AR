using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARFireflyMovement : MonoBehaviour
{
    private float amplitude = 0.5f;
    private float frequency = 4.84f;
    private float speed = 1.04f;
    public GameObject target;
    private Vector3 height = new Vector3(0.0f,2.0f,0.0f); // How high above target object the firefly should be


    void Start()
    {
        
       
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        
    }

    private void Move()
    {
        // Movement
        Vector3 direction = ((target.transform.position + height) - transform.position).normalized;
        transform.position += new Vector3(direction.x, Mathf.Sin(Time.time * frequency) * amplitude, direction.z) * Time.deltaTime * speed;

        // Rotation
        //transform.rotation = Quaternion.LookRotation(direction, transform.up);
        //transform.rotation = Quaternion.Euler(0f, target.transform.rotation.eulerAngles.y, 0f); 

        //transform.position = transform.position + transform.up * Mathf.Sin(Time.time * frequency) * amplitude;
        //transform.position = new Vector3(initPos.x, Mathf.Sin(Time.time * frequency) * amplitude + currentY, initPos.z);

    }


    /*private void MoveTowardsTarget()
    {
        float distCovered = (Time.time - startTime) * travelSpeed;
        float fractionOfJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(initPos, targetPos, fractionOfJourney);
    }*/



}
