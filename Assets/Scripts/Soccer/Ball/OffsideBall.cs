using UnityEngine;
using UnityEngine.InputSystem;

public class OffsideBall : MonoBehaviour
{
    //[SerializeField] private float timer, timeToOffside = 5f;
    //[SerializeField] private bool isOffside = false;
    [SerializeField] private float xMas, xMenos, zMas, zMenos;
    [SerializeField] private BallController ballController;

    void Update()
    {
        /*if (isOffside)
        {
            timer += Time.deltaTime;
            if (timer >= timeToOffside)
            {
                ballController.ResetPosition();
                timer = 0f;
                isOffside = false;
            }
        }

        if (transform.position.x > xMas || transform.position.x < xMenos || transform.position.z > zMas || transform.position.z < zMenos)
        {
            isOffside = true;
        }*/

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            ballController.ResetPosition();
        }
    }
}
