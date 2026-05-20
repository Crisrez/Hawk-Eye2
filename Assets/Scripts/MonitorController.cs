using UnityEngine;
using UnityEngine.InputSystem;

public class MonitorController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Camera monitorCamera;

    [SerializeField] private float izquierdaLimite = -25;
    [SerializeField] private float derechaLimite = 25f;
    [SerializeField] private float profundidadLimite = 10f;
    [SerializeField] private float cercaniaLimite = -20f;

    [SerializeField] private float widthScreenGame;
    [SerializeField] private float heightScreenGame;

    void Start()
    {
        widthScreenGame = Screen.width;
        heightScreenGame = Screen.height;
    }

    void Update()
    {
        //monitorCamera.transform.position = new Vector3(ReMap().x, monitorCamera.transform.position.y, ReMap().y);

        RaycastHit hit;

        if (Physics.Raycast(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()), out hit))
        {
            Debug.Log(hit.point);
            monitorCamera.transform.position = new Vector3(hit.point.x, monitorCamera.transform.position.y, hit.point.z);
        }

    }

    Vector2 ReMap()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        float x = Mathf.InverseLerp(0, widthScreenGame, mousePosition.x);
        float z = Mathf.InverseLerp(0, heightScreenGame, mousePosition.y);

        return new Vector2(Mathf.Lerp(izquierdaLimite, derechaLimite, x), Mathf.Lerp(cercaniaLimite, profundidadLimite, z));
    }
}
