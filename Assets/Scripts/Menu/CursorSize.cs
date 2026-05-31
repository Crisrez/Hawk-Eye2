using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CursorSize : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float sizeMin;
    [SerializeField] private float sizeMax;
    [SerializeField] private FOVController fOVController;
    private RectTransform rectTransform;

    void Start()
    {
        Cursor.visible = false;
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        float newSize = Mathf.Lerp(sizeMin, sizeMax, fOVController.GetValorScrollNormalized());

        rectTransform.sizeDelta = new Vector2(newSize, newSize);

        Vector2 mousePosition = Mouse.current.position.ReadValue();

        rectTransform.position = mousePosition;
    }
}
