using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float moveSpeed = 10f; // Movement speed
    public float groundLeftBoundary = -1.6f; // Left boundary of the ground
    public float groundRightBoundary = 1.6f; // Right boundary of the ground
    public float groundTopBoundary = 10f; // Top boundary of the ground
    public float groundBottomBoundary = 0f; // Bottom boundary of the ground (minimum Y position)

    private float direction = 1f; // Initial movement direction (1 for right, -1 for left)
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (gameObject.CompareTag("YellowBox"))
        {
            // Calculate movement in X direction
            float moveX = direction * moveSpeed * Time.deltaTime;
            float newXPosition = transform.position.x + moveX;

            // Check if new X position exceeds boundaries and reverse direction if necessary
            if (newXPosition > groundRightBoundary)
            {
                newXPosition = groundRightBoundary; // Clamp to right boundary
                direction = -1f; // Change direction to left
            }
            else if (newXPosition < groundLeftBoundary)
            {
                newXPosition = groundLeftBoundary; // Clamp to left boundary
                direction = 1f; // Change direction to right
            }

            // Calculate Z position (assuming constant Z position within boundaries)
            float newZPosition = transform.position.z;

            // Keep current Y position and apply the clamped positions
            float newYPosition = transform.position.y;
            rb.MovePosition(new Vector3(newXPosition, newYPosition, newZPosition));
        }
    }
}
