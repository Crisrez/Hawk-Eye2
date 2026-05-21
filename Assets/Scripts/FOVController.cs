using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class FOVController : MonoBehaviour
{
    //[SerializeField] private Camera monitorCamera;
    [SerializeField] private CinemachineCamera monitorCamera;

    [SerializeField] private float scrollSensitivity = 1f;
    [SerializeField] private float minFOV = 15f;
    [SerializeField] private float maxFOV = 70f;

    float valorScroll = 45f;

    void Update()
    {
        //monitorCamera.fieldOfView += Mouse.current.scroll.ReadValue().y * -scrollSensitivity;
        //monitorCamera.fieldOfView = Mathf.Clamp(monitorCamera.fieldOfView, minFOV, maxFOV);

        valorScroll += Mouse.current.scroll.ReadValue().y * -scrollSensitivity;
        valorScroll = Mathf.Clamp(valorScroll, minFOV, maxFOV);

        //Debug.Log(valorScroll);

        CambiarFOV(valorScroll);

    }

    void CambiarFOV(float newFOV)
    {
        LensSettings lensSettings = monitorCamera.Lens;
        lensSettings.FieldOfView = newFOV;
        monitorCamera.Lens = lensSettings;
    }

    public float GetValorScrollNormalized()
    {
        return Mathf.InverseLerp(minFOV, maxFOV, valorScroll);
    }
}
