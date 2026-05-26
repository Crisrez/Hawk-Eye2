using UnityEngine;

public class FanaticcEliminator : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fanatic")) 
        {
            Destroy(other.gameObject); 
        }
    }
}
