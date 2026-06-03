using UnityEngine;
using FMODUnity;

public class securityWalkieTakie : MonoBehaviour
{
    [SerializeField] private EventReference securityCom;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fanatic"))
        {
            PlaySecurityCom();
        }
    }

    private void PlaySecurityCom()
    {
        if (securityCom.IsNull)
        {
            Debug.LogWarning("Security Com no está asignado en el Inspector.");
            return;
        }

        Debug.Log("Reproduciendo securityCom en: " + transform.position);
        RuntimeManager.PlayOneShot(securityCom, transform.position);
    }

    [ContextMenu("DEBUG - Play Security Com")]
    public void DebugPlaySecurityCom()
    {
        PlaySecurityCom();
    }
}