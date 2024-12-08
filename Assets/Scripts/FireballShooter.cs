using UnityEngine;
using UnityEngine.InputSystem;

public class FireballShooter : MonoBehaviour
{
    [Header("Fireball Settings")]
    public GameObject fireballPrefab;
    public Transform fireballSpawnPoint;
    public float fireballSpeed = 10f;
    public int maxFireballs = 3; // Limit for active fireballs
    public float lifeTime = 5f; // Time before fireball auto-destroys

    private int currentFireballCount = 0;

    [Header("Input Settings")]
    public InputActionReference shootAction;

    private void OnEnable()
    {
        shootAction.action.performed += OnShootPerformed;
    }

    private void OnDisable()
    {
        shootAction.action.performed -= OnShootPerformed;
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        if (currentFireballCount < maxFireballs)
        {
            ShootFireball();
        }
    }

    void ShootFireball()
    {
        GameObject fireball = Instantiate(fireballPrefab, fireballSpawnPoint.position, fireballSpawnPoint.rotation);
        Rigidbody rb = fireball.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = fireballSpawnPoint.forward * fireballSpeed;
        }

        currentFireballCount++;

        // Reduce the count when the fireball is destroyed
        Fireball fireballCollision = fireball.GetComponent<Fireball>();
        if (fireballCollision != null)
        {
            fireballCollision.OnFireballDestroyed += HandleFireballDestroyed;
        }

        Destroy(fireball, lifeTime); // Ensure fireball auto-destroys after 5 seconds
    }

    private void HandleFireballDestroyed()
    {
        currentFireballCount = Mathf.Max(0, currentFireballCount - 1); // Safely decrement
    }
}