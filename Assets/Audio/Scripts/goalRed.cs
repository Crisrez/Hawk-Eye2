using UnityEngine;
using FMODUnity;

public class goalRed : MonoBehaviour
{
    [Header("FMOD Events")]
    [SerializeField] private EventReference goalRedEvent;
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

        Debug.Log("GOAL COUNT: " + goalCount);

        RuntimeManager.PlayOneShot(goalRedEvent, transform.position);

        if (goalCount >= 2)
        {
            Debug.Log("SEGUNDO GOL DETECTADO: intentando reproducir otherStandEvent");

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
                Debug.Log("Reproduciendo otherStandEvent en: " + otherStandAudioPoint.position);
                RuntimeManager.PlayOneShot(otherStandEvent, otherStandAudioPoint.position);
            }

            goalCount = 0;
        }
    }

    [ContextMenu("DEBUG - Force Goal")]
    public void DebugForceGoal()
    {
        RegisterGoal();
    }

    [ContextMenu("DEBUG - Play Other Stand Only")]
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

        Debug.Log("DEBUG: Reproduciendo solo otherStandEvent en: " + otherStandAudioPoint.position);
        RuntimeManager.PlayOneShot(otherStandEvent, otherStandAudioPoint.position);
    }
}