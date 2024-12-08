using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text targetsRemainingText; // Reference to the TMP text field

    [Header("Player Settings")]
    public PlayerMovement2 playerMovement; // Reference to the PlayerMovement2 script

    private int targetsRemaining; // Number of targets remaining

    public GameObject gameWinScreen; // Reference to the GameWinScreen UI panel


    void Start()
    {
        // Initialize the count of targets
        UpdateTargetCount();
    }

    void Update()
    {
        // Continuously update the target count in case targets are destroyed
        UpdateTargetCount();
    }

    void UpdateTargetCount()
    {
        // Count all objects tagged as "Target"
        targetsRemaining = GameObject.FindGameObjectsWithTag("Target").Length;

        // Update the TMP text field
        if (targetsRemainingText != null)
        {
            targetsRemainingText.text = "Targets left: " + targetsRemaining;
        }

        // Check if there are no targets left
        if (targetsRemaining == 0)
        {
            DisablePlayerMovement();
            ShowGameWinScreen();
        }
    }

    void DisablePlayerMovement()
    {
        // Disable player movement by setting moveSpeed to 0 or disabling the script
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }
    }


    void ShowGameWinScreen()
    {
        if (gameWinScreen != null)
        {
            gameWinScreen.SetActive(true); // Activate the GameWinScreen panel
        }
        else
        {
            Debug.LogError("GameWinScreen is not assigned in the Inspector!");
        }
    }
}
