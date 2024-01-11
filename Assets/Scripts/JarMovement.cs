using UnityEngine;

public class JarMovement : MonoBehaviour
{
    public float rollSensitivity = 1.0f; // Sensitivity multiplier for roll
    public float pitchSensitivity = 0.5f; // Sensitivity multiplier for pitch
    public float maxRollTiltAngle = 30.0f; // Max tilt angle for roll (left-right)
    public float maxPitchTiltAngle = 15.0f; // Max tilt angle for pitch (forward-backward)
    public float smoothFactor = 0.5f;

    void Update()
    {
        TiltJarBasedOnPhoneRotation();
    }

    void TiltJarBasedOnPhoneRotation()
    {
        Vector3 acceleration = Input.acceleration;

        // Calculate roll and pitch angles with separate sensitivities
        float rollTiltAngle = Mathf.Clamp(acceleration.x * rollSensitivity * maxRollTiltAngle, -maxRollTiltAngle, maxRollTiltAngle);
        float pitchTiltAngle = Mathf.Clamp(acceleration.y * pitchSensitivity * maxPitchTiltAngle, -maxPitchTiltAngle, maxPitchTiltAngle);

        // Apply the rotation around the z-axis for roll and x-axis for pitch
        Quaternion targetRotation = Quaternion.Euler(-pitchTiltAngle, 0, -rollTiltAngle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothFactor * Time.deltaTime);

        // Debug output
        Debug.Log($"Roll Angle: {rollTiltAngle}, Pitch Angle: {pitchTiltAngle}");
    }
}