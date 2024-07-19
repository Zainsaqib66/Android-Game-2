using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    public float moveSpeed = 1f; // Speed of ground movement
    public float groundLeftBoundary = -1.6f; // Left boundary of the ground
    public float groundRightBoundary = 1.6f; // Right boundary of the ground

    private float direction = 1f; // Initial movement direction (1 = right, -1 = left)

    void Update()
    {
        // Calculate movement amount based on direction and speed
        float moveX = direction * moveSpeed * Time.deltaTime;
        float newXPosition = transform.position.x + moveX;

        // Check boundaries to reverse direction if needed
        if (newXPosition > groundRightBoundary)
        {
            direction = -1f; // Change direction to left
        }
        else if (newXPosition < groundLeftBoundary)
        {
            direction = 1f; // Change direction to right
        }

        // Apply the new position
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
    }
}
