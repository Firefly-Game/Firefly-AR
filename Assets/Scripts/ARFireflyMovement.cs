using UnityEngine;

public class ARFireflyMovement : MonoBehaviour
{
    private float amplitude = 0.5f;
    private float frequency = 4.84f;
    private float speed = 1.04f;
    public GameObject target;
    //private Vector3 height = new Vector3(0.0f,3.0f,0.0f); // How high above target object the firefly should be
    private Vector3 vertStep = new Vector3(0.0f,0.5f,0.0f); // If firefly is far above or under target, take steps vertically
    private float radiusSphere = 3.0f;

    // Update is called once per frame
    void Update()
    {
        float dist = (target.transform.position - transform.position).magnitude;

        if (dist > radiusSphere)
        {
            MoveTowardsGoal();
            RotateTowardsGoal();

            // If above or under target, move vertically
            if (transform.position.y < target.transform.position.y)
            {
                MoveUp();
            }

            if (transform.position.y > target.transform.position.y)
            {
                MoveDown();
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
        Vector3 direction = (target.transform.position - transform.position).normalized;
        transform.position += new Vector3(direction.x, Mathf.Sin(Time.time * frequency) * amplitude, direction.z) * Time.deltaTime * speed;
    }

    private void MoveUpAndDown()
    {
        transform.position += new Vector3(0, Mathf.Sin(Time.time * frequency) * amplitude, 0) * Time.deltaTime * speed;
    }

    // Rotate but keep the rotation in the y-direction
    private void RotateTowardsGoal()
    {
        Vector3 goal = target.transform.position;
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
