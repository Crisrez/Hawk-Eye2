using UnityEngine;
using FMODUnity;

public class goalBlue : MonoBehaviour
{
    [Header("FMOD Events")]
    [SerializeField] private EventReference goalBlueEvent;
    [SerializeField] private EventReference otherStandEvent;

    [Header("Audio Positions")]
    [SerializeField] private Transform otherStandAudioPoint;

    private int goalCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            RegisterGoal();
        }
    }

    private void RegisterGoal()
    {
        goalCount++;

        Debug.Log("BLUE GOAL COUNT: " + goalCount);

        // Suena siempre en la posición del collider del arco azul
        RuntimeManager.PlayOneShot(goalBlueEvent, transform.position);

        // Suena en el 2do, 4to, 6to gol, etc.
        if (goalCount >= 2)
        {
            Debug.Log("SEGUNDO GOL AZUL DETECTADO: intentando reproducir otherStandEvent");

            if (otherStandEvent.IsNull)
            {
                Debug.LogWarning("Other Stand Event no esta asignado.");
            }
            else if (otherStandAudioPoint == null)
            {
                Debug.LogWarning("Other Stand Audio Point no esta asignado.");
            }
            else
            {
                Debug.Log("Reproduciendo otherStandEvent azul en: " + otherStandAudioPoint.position);
                RuntimeManager.PlayOneShot(otherStandEvent, otherStandAudioPoint.position);
            }

            goalCount = 0;
        }
    }

    [ContextMenu("DEBUG - Force Blue Goal")]
    public void DebugForceGoal()
    {
        RegisterGoal();
    }

    [ContextMenu("DEBUG - Play Blue Other Stand Only")]
    public void DebugPlayOtherStandOnly()
    {
        if (otherStandEvent.IsNull)
        {
            Debug.LogWarning("Other Stand Event no esta asignado.");
            return;
        }

        if (otherStandAudioPoint == null)
        {
            Debug.LogWarning("Other Stand Audio Point no esta asignado.");
            return;
        }

        Debug.Log("DEBUG: Reproduciendo solo otherStandEvent azul en: " + otherStandAudioPoint.position);
        RuntimeManager.PlayOneShot(otherStandEvent, otherStandAudioPoint.position);
    }
}