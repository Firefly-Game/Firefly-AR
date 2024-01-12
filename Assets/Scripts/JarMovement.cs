using UnityEngine;

public class JarMovement : MonoBehaviour
{
    public float rollSensitivity = 1.0f; // Sensitivity multiplier for roll
    public float maxRollTiltAngle = 30.0f; // Max tilt angle for roll (left-right)
    public float smoothFactor = 0.5f;

    void Update()
    {
        TiltJarBasedOnPhoneRotation();
    }

    void TiltJarBasedOnPhoneRotation()
    {
        Vector3 acceleration = Input.acceleration;

        // Calculate pitch angle (for forward-backward tilt)
        float pitchTiltAngle = Mathf.Clamp(-acceleration.y * rollSensitivity * maxRollTiltAngle, -maxRollTiltAngle, maxRollTiltAngle);

        // Calculate roll angle (for left-right tilt)
        float rollTiltAngle = Mathf.Clamp(-acceleration.x * rollSensitivity * maxRollTiltAngle, -maxRollTiltAngle, maxRollTiltAngle);

        // Apply the rotation around the X-axis for pitch and Z-axis for roll
        Quaternion targetRotation = Quaternion.Euler(-pitchTiltAngle, 0, rollTiltAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothFactor * Time.deltaTime);

        // Debug output
        Debug.Log($"Pitch Angle: {pitchTiltAngle}, Roll Angle: {rollTiltAngle}");
    }
}