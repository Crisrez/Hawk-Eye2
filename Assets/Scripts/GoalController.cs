using UnityEngine;

public class GoalController : MonoBehaviour
{
    [SerializeField] private BallController ballController;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Debug.Log("HOLA");
            ballController.ResetPosition();
        }
    }
}
