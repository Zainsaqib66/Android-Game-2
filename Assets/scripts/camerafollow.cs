using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    private void LateUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }
}
