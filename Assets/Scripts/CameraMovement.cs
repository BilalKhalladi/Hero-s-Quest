using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset; 

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, position, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
