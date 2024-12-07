using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // The player or object at the center of the orbit
    public float distance = 5.0f;  // Distance of the camera from the target
    public float verticalAngle = 45.0f;  // Fixed vertical angle of the camera
    public float rotationSpeed = 100.0f;  // Speed of rotation around the target

    public InputActionReference rotateAction; // InputActionReference for rotation

    private float currentYaw = 0.0f;  // Current horizontal angle around the target

    void Awake()
    {
        // Enable the rotation input action
        rotateAction.action.Enable();
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Read rotation input from the InputActionReference
        float rotationInput = rotateAction.action.ReadValue<float>();

        // Ensure rotation stops when no input is provided
        if (Mathf.Abs(rotationInput) < Mathf.Epsilon)
        {
            rotationInput = 0f;
        }

        // Apply rotation based on input
        currentYaw += rotationInput * rotationSpeed * Time.deltaTime;

        // Convert the yaw and vertical angle into a rotation
        Quaternion rotation = Quaternion.Euler(verticalAngle, currentYaw, 0);

        // Calculate the position on the "rim of the glass"
        Vector3 positionOffset = rotation * Vector3.back * distance;

        // Set the camera's position and make it look at the target
        transform.position = target.position + positionOffset;
        transform.LookAt(target.position);
    }
}


