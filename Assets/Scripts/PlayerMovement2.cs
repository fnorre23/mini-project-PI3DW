using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement2 : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5.0f;  // Horizontal movement speed
    public float rotationSpeed = 360.0f;  // Speed of Y-axis rotation
    public float jumpForce = 5.0f;  // Jump force
    public Transform cameraTransform;  // Reference to the camera's transform

    [Header("Input Actions")]
    public InputActionReference moveAction;  // Reference to the Move input action
    public InputActionReference jumpAction;  // Reference to the Jump input action
    public InputActionReference rotateAction; // Reference to the Rotate input action (Q/E)

    private Vector3 movementInput;  // 3D vector for movement input
    private Rigidbody rb;  // Rigidbody for physics-based movement
    private bool isGrounded = true;  // Tracks whether the player is grounded

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (cameraTransform == null)
        {
            Debug.LogError("Camera Transform is not assigned!");
        }

        // Enable input actions
        moveAction.action.Enable();
        jumpAction.action.Enable();
        rotateAction.action.Enable();
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    void HandleMovement()
    {
        // Read movement input as a Vector3
        movementInput = moveAction.action.ReadValue<Vector3>();

        // Get camera-relative directions
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Flatten forward and right vectors to ignore vertical direction
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        // Calculate movement direction relative to the camera
        Vector3 moveDirection = forward * movementInput.z + right * movementInput.x;

        // Apply movement
        Vector3 movement = moveDirection * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // Define a small threshold to prevent rotation on minor movements
        float movementThreshold = 0.1f;

        // Only update rotation if the movement input is above the threshold
        if (movementInput.magnitude > movementThreshold)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }
    }

    void HandleRotation()
    {
        // Read rotation input from Rotate action
        float rotationInput = rotateAction.action.ReadValue<float>();
        if (Mathf.Abs(rotationInput) > 0f)
        {
            transform.Rotate(Vector3.up, rotationInput * rotationSpeed * Time.fixedDeltaTime);
        }

        //If character has stopped moving, stop all rotation
        if (movementInput.magnitude == 0)
        {
            rb.angularVelocity = Vector3.zero;
        }
    }

    public void OnJump()
    {
        // Read jump input (no need to explicitly read value since it's a button)
        if (jumpAction.action.triggered && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player has landed on the ground
        if (collision.contacts.Length > 0 && collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
