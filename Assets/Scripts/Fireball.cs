using UnityEngine;
using System;

public class Fireball : MonoBehaviour
{
    public event Action OnFireballDestroyed;

    private bool hasCollided = false;


    void OnCollisionEnter(Collision collision)
    {
        if (!hasCollided) // Prevent multiple triggers
        {
            hasCollided = true;
            OnFireballDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (!hasCollided) // Safety fallback in case destruction occurs non-collision
        {
            hasCollided = true;
            OnFireballDestroyed?.Invoke();
        }
    }
}
