using UnityEngine;

public class FanaticEliminator : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fanatic")) 
        {
            Destroy(other.gameObject); 
        }
    }
}
