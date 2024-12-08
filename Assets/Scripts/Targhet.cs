using UnityEngine;

public class Targhet : MonoBehaviour
{
    public float speedIncrease = 1.0f; // Speed increase when the target is hit
    private PlayerMovement2 playerMovement; // Reference to the player movement script
    private FireballShooter fireballShooter; // Reference to the fireball shooter script

    void Start()
    {
        // Automatically find the PlayerMovement2 script in the scene
        playerMovement = FindObjectOfType<PlayerMovement2>();
        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement2 script not found in the scene!");
        }

        // Automatically find the FireballShooter script in the scene
        fireballShooter = FindObjectOfType<FireballShooter>();
        if (fireballShooter == null)
        {
            Debug.LogError("FireballShooter script not found in the scene!");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with a fireball
        Fireball fireball = collision.gameObject.GetComponent<Fireball>();
        if (fireball != null)
        {
            // Destroy the fireball
            Destroy(fireball.gameObject);

            // Destroy the target
            Destroy(gameObject);

            if (playerMovement != null)
            {
                playerMovement.moveSpeed += speedIncrease; // Increase the move speed
            }

            if (fireballShooter != null)
            {
                fireballShooter.fireballSpeed += speedIncrease; // Increase fireball speed
            }
        }
    }
}
