using UnityEngine;
using UnityEngine.AI;

public class FanaticController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private GameObject target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
    }

    void Update()
    {
        if (target == null)  return;

        agent.SetDestination(target.transform.position);

        if (Vector3.Distance(transform.position, target.transform.position) < 1f)
        {
            // Aquí puedes agregar lógica adicional si el fanático llega al objetivo
            Debug.Log("Fanático ha llegado al objetivo.");
        }
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    void OnColliderEnter(Collider other)
    {
        if (other.CompareTag("Security"))
        {
            // Aquí puedes agregar lógica para lo que sucede cuando el fanático es arrestado por un guardia
            Debug.Log("Fanático ha sido arrestado por un guardia.");
        }
    }
}
