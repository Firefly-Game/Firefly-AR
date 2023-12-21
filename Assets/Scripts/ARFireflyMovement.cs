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
    private Vector3 height = new Vector3(0.0f,3.0f,0.0f); // How high above target object the firefly should be
    private Vector3 vertStep = new Vector3(0.0f,0.5f,0.0f); // If firefly is far above or under target, take steps vertically
    private bool hasReachedGoal = false; // When reached goal, should not move away any more
    private float startTime;

    void Start()
    {
        
       
    }


    // Update is called once per frame
    void Update()
    {
        float dist = ((target.transform.position + height) - transform.position).magnitude;

        
        if (!hasReachedGoal)
        {
            // If close, has reached goal and should not move any more
            if (dist < 0.2)
            {
                Debug.Log("Has reached goal");
                hasReachedGoal = true;
            }
            // Else should move
            else
            {
                MoveTowardsGoal();
                RotateTowardsGoal();
                float vertDist = target.transform.position.y - transform.position.y;


                // If above or under target, move vertically
                if (transform.position.y < ((target.transform.position + height).y))
                {
                    MoveUp();
                }

                if (vertDist > ((target.transform.position + height).y))
                {
                    MoveDown();
                }
            }
        }
        else
        {
            // Simply move up and down 
            MoveUpAndDown();
        }
        
    }

    private void MoveTowardsGoal()
    {
        Vector3 direction = ((target.transform.position + height) - transform.position).normalized;
        transform.position += new Vector3(direction.x, Mathf.Sin(Time.time * frequency) * amplitude, direction.z) * Time.deltaTime * speed;

    }

    private void MoveUpAndDown()
    {
        transform.position += new Vector3(0, Mathf.Sin(Time.time * frequency) * amplitude, 0) * Time.deltaTime * speed;
    }

    // Rotate but keep the rotation in the y-direction
    private void RotateTowardsGoal()
    {
        Vector3 goal = target.transform.position + height;
        transform.LookAt(new Vector3(goal.x, transform.position.y, goal.z));
    }


    private void MoveUp()
    {
        transform.position += vertStep;
    }

    private void MoveDown()
    {
        transform.position -= vertStep;
    }

   


}
