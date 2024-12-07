using System.Collections.Generic;
using UnityEngine;

public class WallVisibility : MonoBehaviour
{
    public Transform player; // Reference to the player
    public Camera referenceCamera; // Reference to the camera
    public LayerMask wallLayer; // LayerMask to identify wall objects
    public Material transparentMaterial; // Pre-configured transparent material (assign in Inspector)

    private Dictionary<Renderer, Material> originalMaterials = new Dictionary<Renderer, Material>(); // Store original wall materials
    private HashSet<Renderer> hiddenWalls = new HashSet<Renderer>(); // Track hidden walls

    void Start()
    {
        // Cache the original materials of all wall renderers in the wallLayer
        foreach (GameObject wall in GameObject.FindGameObjectsWithTag("Wall"))
        {
            Renderer renderer = wall.GetComponent<Renderer>();
            if (renderer != null)
            {
                if (!originalMaterials.ContainsKey(renderer)) // Avoid duplicates
                {
                    originalMaterials[renderer] = renderer.material;
                }
            }
        }
    }

    void Update()
    {
        if (referenceCamera == null || player == null) return; // Ensure camera and player are assigned

        Vector3 direction = player.position - referenceCamera.transform.position;
        Ray ray = new Ray(referenceCamera.transform.position, direction);

        // Store which walls should be hidden this frame
        HashSet<Renderer> currentlyHiddenWalls = new HashSet<Renderer>();

        // Perform raycast
        if (Physics.Raycast(ray, out RaycastHit hit, direction.magnitude, wallLayer))
        {
            Renderer wallRenderer = hit.collider.GetComponent<Renderer>();
            if (wallRenderer != null)
            {
                // Make the wall invisible
                MakeWallInvisible(wallRenderer);

                // Track this wall as currently hidden
                currentlyHiddenWalls.Add(wallRenderer);
            }
        }

        // Restore visibility for walls that are no longer hidden
        foreach (Renderer renderer in hiddenWalls)
        {
            if (!currentlyHiddenWalls.Contains(renderer)) // If the wall is no longer blocking view
            {
                RestoreWallVisibility(renderer);
            }
        }

        // Update the hidden walls set
        hiddenWalls = currentlyHiddenWalls;
    }

    private void MakeWallInvisible(Renderer wallRenderer)
    {
        if (transparentMaterial != null)
        {
            wallRenderer.material = transparentMaterial; // Apply the assigned transparent material
        }
        else
        {
            Debug.LogError("Transparent Material is not assigned in the Inspector!");
        }
    }

    private void RestoreWallVisibility(Renderer wallRenderer)
    {
        if (originalMaterials.TryGetValue(wallRenderer, out Material originalMaterial))
        {
            wallRenderer.material = originalMaterial; // Restore the original material
        }
    }
}
