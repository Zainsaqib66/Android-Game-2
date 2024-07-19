using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float startingMoveSpeed = 5f; // Starting speed of forward movement (reduced speed)
    public float sideMoveSpeed = 3f; // Speed of sideways movement (reduced speed)
    public float groundLeftBoundary = -1.6f; // Define the left boundary of the ground
    public float groundRightBoundary = 1.6f; // Define the right boundary of the ground
    public float groundBottomBoundary = -10f; // Define the bottom boundary of the ground for game over
    public ScoreManager scoreManager; // Reference to the ScoreManager script

    private float currentMoveSpeed; // Current speed of forward movement
    private bool moveForward = false;
    private bool canMove = true;
    private Rigidbody rb;
    private Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Freeze rotation to avoid unwanted tilting
        currentMoveSpeed = startingMoveSpeed; // Initialize current speed with starting speed
        mainCamera = Camera.main; // Get reference to the main camera
    }

    void Update()
    {
        if (canMove)
        {
            // Move forward when space is pressed
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveForward = true;
            }

            if (moveForward)
            {
                transform.Translate(Vector3.forward * currentMoveSpeed * Time.deltaTime);
                // Increase speed based on forward movement
                currentMoveSpeed += Time.deltaTime * 0.5f; // Example: increase speed by 0.5 units per second
            }

            // Move sideways based on arrow keys
            float moveX = 0f;

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                moveX = -sideMoveSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                moveX = sideMoveSpeed * Time.deltaTime;
            }

            // Calculate new position
            Vector3 newPosition = transform.position + new Vector3(moveX, 0, 0);

            // Clamp the newPosition within the ground boundaries
            newPosition.x = Mathf.Clamp(newPosition.x, groundLeftBoundary, groundRightBoundary);

            // Apply the clamped position
            transform.position = newPosition;

            // Keep the player on the ground
            Vector3 playerPosition = transform.position;
            playerPosition.y = Mathf.Max(playerPosition.y, groundBottomBoundary);
            transform.position = playerPosition;

            // Update camera position to follow player
            Vector3 cameraPosition = mainCamera.transform.position;
            cameraPosition.x = transform.position.x;
            mainCamera.transform.position = cameraPosition;

            // Check if player has fallen off the ground
            if (transform.position.y < groundBottomBoundary)
            {
                GameOver();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Destroy green boxes and stop on yellow boxes
        if (collision.gameObject.CompareTag("GreenBox"))
        {
            Destroy(collision.gameObject);
            scoreManager.AddScore(1); // Add 1 to the score for each green box destroyed
        }
        else if (collision.gameObject.CompareTag("YellowBox"))
        {
            canMove = false;
        }
        else if (collision.gameObject.CompareTag("Winner"))
        {
            canMove = false;
            WinGame();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Winner"))
        {
            canMove = false;
            rb.velocity = Vector3.zero; // Stop all movement
            rb.isKinematic = true; // Disable physics interactions
            WinGame();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over!"); // Print message to console (optional)
        // Implement game over logic here, such as showing a game over screen or restarting the scene
        // For example, you can restart the scene:
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void WinGame()
    {
        Debug.Log("You Win!"); // Print message to console (optional)
        // Implement win game logic here, such as showing a win screen or transitioning to a new scene
        // For example, you can transition to a win scene:
        SceneManager.LoadScene("WinScene");
    }
}
