using UnityEngine;
using FMODUnity;

public class goalRed : MonoBehaviour
{
    [SerializeField] EventReference goalRedEvent;
   
    public void OnTriggerEnter(Collider other)
    {
    if (other.CompareTag("Ball"))
    {
        RuntimeManager.PlayOneShot(goalRedEvent, transform.position);
    }
    
    }
   
}
