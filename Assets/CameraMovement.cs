using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;  // Offset for the camera, like (0, 0, -10)

    void LateUpdate()
    {
        if (player != null)
        {
            // Set the camera's position to follow the player's X, but not Y
            Vector3 desiredPosition = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z);

            // Smoothly move towards the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
