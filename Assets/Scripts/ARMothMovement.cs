using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARMothMovement : MonoBehaviour
{
    private float amplitude = 0.5f;
    private float frequency = 4.84f;
    private float speed = 1.04f;
    public GameObject target;
    private Vector3 distanceBehind = new Vector3(1.0f, 2.0f, -2.0f); // The distance between target and point behind
    private Vector3 vertStep = new Vector3(0.0f, 0.5f, 0.0f); // If firefly is far above or under target, take steps vertically



    void Start()
    {


    }


    // Update is called once per frame
    void Update()
    {
        MoveTowardsGoal();
        RotateTowardsGoal();
        float vertDist = target.transform.position.y - transform.position.y;

        // If more than two steps above or under target, move vertically
        /*if (vertDist > (2.0 * vertStep.y))
        {
            MoveUp();
        }

        if (vertDist < (-2.0 * vertStep.y))
        {
            MoveDown();
        }*/


    }

    private void MoveTowardsGoal()
    {
        Vector3 direction = ((target.transform.position + distanceBehind) - transform.position).normalized;
        transform.position += new Vector3(direction.x, Mathf.Sin(Time.time * frequency) * amplitude, direction.z) * Time.deltaTime * speed;

    }

    private void RotateTowardsGoal()
    {

        transform.LookAt(target.transform.position + distanceBehind);
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
