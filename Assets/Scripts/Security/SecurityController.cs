using UnityEngine;
using UnityEngine.AI;

public class SecurityController : MonoBehaviour
{
    [SerializeField] private UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] private Transform homePosition1;
    [SerializeField] private Transform homePosition2;
    [SerializeField] private Transform target;
    [SerializeField] private GameObject fanatico;

    [SerializeField] private float speedPatrol = 3.5f;
    [SerializeField] private float speedChase = 5f;
    [SerializeField] private float stayTime = 0.5f;

    [SerializeField] private float distanceToChase = 5f;

    enum State { Idle, Patrolling, Chasing, ChasingBack }
    [SerializeField] private State currentState = State.Idle;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        currentState = State.Idle;
        target = homePosition1; // Establece el primer puesto como objetivo inicial
    }

    void Update()
    {
        if (fanatico != null) currentState = State.Chasing;
        
        switch (currentState)
        {
            case State.Idle:
                Idle();
                break;
            case State.Patrolling:
                Patrol();
                break;
            case State.Chasing:
                Chase();
                break;
            case State.ChasingBack:
                ChaseBack();
                break;
        }
    }

    void Idle()
    {
        Invoke(nameof(Patrol), stayTime); // Espera un tiempo antes de iniciar la patrulla
    }

    void Patrol()
    {
        currentState = State.Patrolling;
        agent.speed = speedPatrol;

        agent.SetDestination(target.position);

        if (Vector3.Distance(transform.position, target.position) < 1f)
        {
            target = target == homePosition1 ? homePosition2 : homePosition1; // Cambia al otro puesto
            currentState = State.Idle;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fanatic"))
        {
            if (currentState != State.Chasing && fanatico == null)
            {
                fanatico = other.gameObject;
            }
        }
    }

    void Chase()
    {
        agent.speed = speedChase;
        agent.SetDestination(fanatico.transform.position); // Persigue al objetivo

        if (Vector3.Distance(transform.position, fanatico.transform.position) < 1f)
        {
            // Aquí puedes agregar lógica para lo que sucede cuando el guardia alcanza al objetivo
            Debug.Log("¡Objetivo alcanzado!");
            Destroy(fanatico); // Destruye el objeto del objetivo
            currentState = State.ChasingBack;
        }
    }

    void ChaseBack()
    {
        agent.speed = speedPatrol;
        agent.SetDestination(target.position); // Regresa al primer puesto

        if (Vector3.Distance(transform.position, target.position) < 1f)
        {
            target = target == homePosition1 ? homePosition2 : homePosition1; // Cambia al otro puesto
            currentState = State.Idle; // Vuelve al estado Idle después de regresar al puesto
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distanceToChase);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(homePosition1.position, 1f);
        Gizmos.DrawWireSphere(homePosition2.position, 1f);
    }
}
