using UnityEngine;

public class OutOfCameraDestroyer : MonoBehaviour
{
    private float destroyDistance = 50f; // Distance after which the object is destroyed

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Destroy this object if it goes beyond the camera view plus some extra distance (destroyDistance)
        if (transform.position.x < mainCamera.transform.position.x - destroyDistance)
        {
            Destroy(gameObject);
        }
    }
}
