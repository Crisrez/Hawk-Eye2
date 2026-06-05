using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class FulboCrowdController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider fulboSlider;
    [SerializeField] private StudioEventEmitter crowdEmitter;

    private const string ParameterName = "AngryCrowd";

    private void Start()
    {
        if (fulboSlider == null)
        {
            Debug.LogError(
                "FulboCrowdController: falta asignar el Slider Fulbo."
            );

            enabled = false;
            return;
        }

        if (crowdEmitter == null)
        {
            Debug.LogError(
                "FulboCrowdController: falta asignar el Studio Event Emitter."
            );

            enabled = false;
            return;
        }

        fulboSlider.onValueChanged.AddListener(UpdateAngryCrowd);

        // Aplica el valor inicial.
        UpdateAngryCrowd(fulboSlider.value);
    }

    private void UpdateAngryCrowd(float sliderValue)
    {
        // Convierte el rango 0–100 del slider
        // al rango 0–10 de FMOD.
        float fmodValue = Mathf.InverseLerp(
            fulboSlider.minValue,
            fulboSlider.maxValue,
            sliderValue
        ) * 10f;

        crowdEmitter.SetParameter(
            ParameterName,
            fmodValue
        );

        Debug.Log(
            "Fulbo Slider: " +
            sliderValue +
            " | AngryCrowd: " +
            fmodValue
        );
    }

    private void OnDestroy()
    {
        if (fulboSlider != null)
        {
            fulboSlider.onValueChanged.RemoveListener(
                UpdateAngryCrowd
            );
        }
    }
}