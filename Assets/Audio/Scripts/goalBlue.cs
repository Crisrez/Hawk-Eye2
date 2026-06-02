using UnityEngine;
using FMODUnity;

public class goalBlue : MonoBehaviour
{
    [SerializeField] EventReference goalBlueEvent;
   
    public void OnTriggerEnter(Collider other)
    {
    if (other.CompareTag("Ball"))
    {
        RuntimeManager.PlayOneShot(goalBlueEvent, transform.position);
    }
    
    }
   
}
