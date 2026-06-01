using UnityEngine;

public class RecoverBall : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            BallController ballController = collision.gameObject.GetComponent<BallController>();
            ballController.Gol();
            ballController.ResetPosition();
        }
    }

}
