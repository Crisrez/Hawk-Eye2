using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Vector3 initialPosition;

    void Start()
    {
        initialPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
    }

    public void ResetPosition()
    {
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        GetComponent<Rigidbody>().isKinematic = true;
        transform.position = initialPosition;
        GetComponent<Rigidbody>().isKinematic = false;
    }
}
