using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;

public class SoccerCrowdController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Slider soccerEnergySlider;
    [SerializeField] private StudioEventEmitter crowdEmitter;

    [Header("Thresholds")]
    [SerializeField] private float maestroThreshold = 40f;
    [SerializeField] private float tuttiThreshold = 60f;

    private const string ParameterName = "La_Banda_Loca";
    private const string SilenceLabel = "Silencio";
    private const string MaestroLabel = "elMaestro";
    private const string TuttiLabel = "Tutti";

    private string currentLabel = "";

    private void Start()
    {
        if (soccerEnergySlider == null)
        {
            Debug.LogError("Falta asignar el Slider de Soccer.");
            enabled = false;
            return;
        }

        if (crowdEmitter == null)
        {
            Debug.LogError("Falta asignar el Studio Event Emitter.");
            enabled = false;
            return;
        }

        soccerEnergySlider.onValueChanged.AddListener(UpdateCrowd);

        StartCoroutine(ApplyInitialState());
    }

    private IEnumerator ApplyInitialState()
    {
        // Espera un frame para que el Studio Event Emitter cree su instancia.
        yield return null;

        UpdateCrowd(soccerEnergySlider.value);
    }

    private void UpdateCrowd(float soccerValue)
    {
        if (!crowdEmitter.EventInstance.isValid())
        {
            Debug.LogWarning("La instancia del FMOD Studio Event Emitter todavía no es válida.");
            return;
        }

        string desiredLabel;

        if (soccerValue < maestroThreshold)
        {
            desiredLabel = SilenceLabel;
        }
        else if (soccerValue < tuttiThreshold)
        {
            desiredLabel = MaestroLabel;
        }
        else
        {
            desiredLabel = TuttiLabel;
        }

        if (desiredLabel == currentLabel)
        {
            return;
        }

        FMOD.RESULT result =
            crowdEmitter.EventInstance.setParameterByNameWithLabel(
                ParameterName,
                desiredLabel
            );

        if (result != FMOD.RESULT.OK)
        {
            Debug.LogError(
                $"No se pudo cambiar {ParameterName} a {desiredLabel}. Error FMOD: {result}"
            );
            return;
        }

        currentLabel = desiredLabel;

        Debug.Log(
            $"FMOD: {ParameterName} = {desiredLabel} | Soccer Energy = {soccerValue}"
        );
    }

    private void OnDestroy()
    {
        if (soccerEnergySlider != null)
        {
            soccerEnergySlider.onValueChanged.RemoveListener(UpdateCrowd);
        }
    }
}