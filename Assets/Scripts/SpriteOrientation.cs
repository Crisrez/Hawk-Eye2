using UnityEngine;

public class SpriteOrientation : MonoBehaviour
{
    [SerializeField] private Camera orientationCamera;
    [SerializeField] private string orientationTag;

    void Start()
    {
        if (orientationCamera == null)
        {
            // Try to find the camera with the specified tag
            orientationCamera = GameObject.FindGameObjectWithTag(orientationTag)?.GetComponent<Camera>();
        }
    }

    void Update()
    {
        if (orientationCamera != null)
        {
            // Make the sprite face the camera
            transform.LookAt(transform.position + orientationCamera.transform.rotation * Vector3.forward,
                             orientationCamera.transform.rotation * Vector3.up);
        }
    }
}
