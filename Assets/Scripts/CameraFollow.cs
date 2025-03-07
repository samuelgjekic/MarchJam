using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Assign the player GameObject
    public float smoothSpeed = 5f; // How fast the camera moves
    public float offset = 2f; // How much the camera is offset from player

    void LateUpdate()
    {
        if (target == null) return;

        // Follow target (only X & Y for 2D games)
        Vector3 newPosition = new Vector3(target.position.x + offset, target.position.y, transform.position.z);
        if (newPosition.y < -0.15f) newPosition.y = 0.15f;
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed * Time.deltaTime);
    }
}
