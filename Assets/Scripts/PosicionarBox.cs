using UnityEngine;
using UnityEngine.InputSystem;

public class PosicionarBox : MonoBehaviour
{
    [SerializeField] private GameObject box;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private FOVController fOVController;
    [SerializeField] private float minScale, maxScale;

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        box.transform.localScale = Vector3.Lerp(new Vector3(minScale, minScale, minScale), new Vector3(maxScale, maxScale, maxScale), fOVController.GetValorScrollNormalized());

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore))
        {
            //Debug.Log(hit.point);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
            box.transform.position = hit.point;
        }

    }
}
