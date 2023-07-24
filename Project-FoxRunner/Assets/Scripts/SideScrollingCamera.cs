using UnityEngine;

public class SideScrollingCamera : MonoBehaviour
{
    public Transform target;           // The target the camera will follow (e.g., the player)
    public float smoothSpeed = 0.125f; // The smoothing speed of the camera movement

    private Vector3 offset;

    private void Start()
    {
        if (target != null)
        {
            offset = transform.position - target.position;
        }
        else
        {
            Debug.LogError("No target assigned to the SideScrollingCamera!");
        }
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
            transform.position = new Vector3(smoothedPosition.x, 0, transform.position.z);
        }
    }
}
