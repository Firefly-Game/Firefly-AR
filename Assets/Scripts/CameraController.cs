using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float sensitivity = 1;

    void Update()
    {
        if (!MenuManager.isGamePaused) // Check if game is not paused
        {
            float rotateHorizontal = Input.GetAxis("Mouse X");
            float rotateVertical = Input.GetAxis("Mouse Y");
            float scroll = Input.mouseScrollDelta.y;

            transform.RotateAround(transform.position, -Vector3.up, rotateHorizontal * sensitivity);
            transform.RotateAround(transform.position, transform.right, rotateVertical * sensitivity);
            transform.RotateAround(transform.position, transform.forward, scroll * sensitivity);
        }
    }
}

