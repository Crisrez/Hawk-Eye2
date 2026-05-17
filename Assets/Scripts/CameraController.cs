using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Movimiento Lateral (Traslación)")]
    [Tooltip("Máxima distancia que la cámara se moverá hacia la izquierda o derecha.")]
    public float maxLateralMovement = 2f;

    [Header("Movimiento Vertical (Rotación)")]
    [Tooltip("Máximo ángulo de rotación hacia arriba o abajo.")]
    public float maxPitchAngle = 10f;

    [Header("Suavizado")]
    [Tooltip("Velocidad de respuesta. Valores más bajos = más suave y amortiguado.")]
    public float smoothTime = 5f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    void Start()
    {
        // Guardamos el punto de origen de la cámara
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    void Update()
    {
        // 1. Obtener y normalizar la posición del mouse en la pantalla (-1 a 1)
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        float mouseXNormalized = ((mousePosition.x / Screen.width) - 0.5f) * 2f;
        float mouseYNormalized = ((mousePosition.y / Screen.height) - 0.5f) * 2f;

        mouseXNormalized = Mathf.Clamp(mouseXNormalized, -1f, 1f);
        mouseYNormalized = Mathf.Clamp(mouseYNormalized, -1f, 1f);

        // 2. Calcular la posición LATERAL objetivo (Desplazamiento en X)
        float targetXPosition = mouseXNormalized * maxLateralMovement;
        // Mantenemos la Y y Z iniciales para que solo se mueva de lado
        targetPosition = initialPosition + new Vector3(targetXPosition, 0f, 0f);

        // 3. Calcular la rotación VERTICAL objetivo (Rotación en X / Pitch)
        // Invertimos el signo para que al mover el mouse arriba, mire arriba
        float targetPitch = -mouseYNormalized * maxPitchAngle;
        targetRotation = initialRotation * Quaternion.Euler(targetPitch, 0f, 0f);

        // 4. Aplicar los movimientos de forma suavizada (Lerp / Slerp)
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * smoothTime);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * smoothTime);
    }
}
