using UnityEngine;

public class SpriteOrientation : MonoBehaviour
{
    [SerializeField] private Camera orientationCamera;

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
