using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    [SerializeField] private Vector3 initialPosition;
    [SerializeField] private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
    }

    public void ResetPosition()
    {
        transform.position = initialPosition;
        rb.isKinematic = false;
    }

    public void Gol()
    {
        transform.SetParent(null);
        rb.linearVelocity = Vector3.zero;
        rb.isKinematic = true;
        //rb.angularVelocity = Vector3.zero;
        transform.position = new Vector3(100,100,100);
    }
}
