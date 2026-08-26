using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("Reference to the player's transform that the camera will follow.")] public Transform player;
    [Tooltip("Offset from the player's position.")] public Vector3 offset;

    private void Start()
    {
        if (player != null)
            offset = transform.position - player.position; // Cache initial offset.
    }

    private void LateUpdate()
    {
        if (player != null)
            transform.position = player.position + offset; // Follow player preserving offset.
    }
}
