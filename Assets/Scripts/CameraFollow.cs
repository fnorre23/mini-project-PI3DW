using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // The player or object at the center of the orbit
    public float distance = 5.0f;  // Distance of the camera from the target (rim of the glass)
    public float verticalAngle = 45.0f;  // Fixed vertical angle of the camera
    public float rotationSpeed = 100.0f;  // Speed of rotation around the target

    private float currentYaw = 0.0f;  // Current horizontal angle around the target

    void LateUpdate()
    {
        if (target == null) return;

        // Get rotation input from Q and E keys
        if (Input.GetKey(KeyCode.Q))
        {
            currentYaw -= rotationSpeed * Time.deltaTime; // Rotate left
        }
        if (Input.GetKey(KeyCode.E))
        {
            currentYaw += rotationSpeed * Time.deltaTime; // Rotate right
        }

        // Convert the yaw and vertical angle into a rotation
        Quaternion rotation = Quaternion.Euler(verticalAngle, currentYaw, 0);

        // Calculate the position on the "rim of the glass"
        Vector3 positionOffset = rotation * Vector3.back * distance;

        // Set the camera's position and make it look at the target
        transform.position = target.position + positionOffset;
        transform.LookAt(target.position);
    }
}


