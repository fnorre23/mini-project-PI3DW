using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //Variables

    public Rigidbody _rb;

    public float _walkSpeed;

    private Vector3 _moveDirection;

    public InputActionReference move;

    // Her er mit forsøg på at lave kamera relativ movement
    public Transform _camera;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        _moveDirection = move.action.ReadValue<Vector3>();
    }

    private void FixedUpdate()
    {
        _rb.linearVelocity = new Vector3(_moveDirection.x * _walkSpeed, _moveDirection.y * _walkSpeed, _moveDirection.z * _walkSpeed);
    }
}
