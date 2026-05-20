using UnityEngine;
using UnityEngine.InputSystem;

public class PosicionarBox : MonoBehaviour
{
    [SerializeField] private GameObject box;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());



        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask, QueryTriggerInteraction.Ignore))
        {
            //Debug.Log(hit.point);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
            box.transform.position = hit.point;
        }

    }
}
