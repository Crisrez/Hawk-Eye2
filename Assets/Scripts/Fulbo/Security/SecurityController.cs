using UnityEngine;
using UnityEngine.AI;

public class SecurityController : MonoBehaviour
{
    [SerializeField] private UnityEngine.AI.NavMeshAgent agent;
    [SerializeField] private Transform target;
    
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private Transform[] tunnelPositions;

    [SerializeField] private GameObject fanatico;

    [SerializeField] private float walking = 3.5f;
    [SerializeField] private float running = 5f;
    [SerializeField] private float stayTime = 0.5f;

    //[SerializeField] private float distanceToChase = 5f;

    enum State { Idle, Patrolling, Chasing, ChasingBack }
    [SerializeField] private State currentState = State.Idle;

    void Start()
    {
        if (agent == null)
        {
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        }

        currentState = State.Idle;
        agent.speed = walking;
        target = patrolPoints[0]; // Establece el primer puesto como objetivo inicial
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
        
        agent.SetDestination(target.position); // Asegura que el agente siempre se dirija al objetivo actual
    }

    void Idle()
    {
        Invoke(nameof(Patrol), stayTime); // Espera un tiempo antes de iniciar la patrulla
    }

    void Patrol()
    {
        currentState = State.Patrolling;
        //agent.speed = walking;

        //agent.SetDestination(target.position);

        if (Vector3.Distance(transform.position, target.position) < 1f)
        {
            target = target == patrolPoints[0] ? patrolPoints[1] : patrolPoints[0]; // Cambia al otro puesto
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
        agent.speed = running;

        target = fanatico.transform; // Establece el objetivo como el fanático
        //agent.SetDestination(fanatico.transform.position); // Persigue al objetivo

        if (Vector3.Distance(transform.position, fanatico.transform.position) < 1.2f)
        {
            // Aquí puedes agregar lógica para lo que sucede cuando el guardia alcanza al objetivo
            Debug.Log("¡Objetivo alcanzado!");

            fanatico.transform.SetParent(transform);
            FanaticController controlador = fanatico.GetComponent<FanaticController>();
            controlador.enabled = false;
            UnityEngine.AI.NavMeshAgent navMesh = fanatico.GetComponent<UnityEngine.AI.NavMeshAgent>();
            navMesh.enabled = false;

            Rigidbody rg = fanatico.GetComponent<Rigidbody>();
            rg.isKinematic = true;

            fanatico = null;

            //Destroy(fanatico); // Destruye el objeto del objetivo
            currentState = State.ChasingBack;
        }
    }

    /*void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == fanatico)
        {
            Debug.Log("CAPTURE AL FANATICO");

            fanatico.transform.SetParent(transform);
            FanaticController controlador = fanatico.GetComponent<FanaticController>();
            controlador.enabled = false;
            fanatico = null;
        }
    }*/

    void ChaseBack()
    {
        if (Vector3.Distance(transform.position, tunnelPositions[0].position) < Vector3.Distance(transform.position, tunnelPositions[1].position))
        {
            target = tunnelPositions[0]; // Regresa al primer puesto
        }
        else
        {
            target = tunnelPositions[1]; // Regresa al segundo puesto
        }

        //agent.SetDestination(target.position);
        
        if (Vector3.Distance(transform.position, target.position) < 1.5f)
        {
            Debug.Log("Guardia ha devuelto al fanatico.");
            agent.speed = walking;
            target = patrolPoints[1];
            //agent.SetDestination(target.position); // Regresa al primer puesto
            currentState = State.Idle; // Vuelve al estado Idle después de regresar al puesto
        }

        /*agent.speed = speedPatrol;
        agent.SetDestination(target.position); // Regresa al primer puesto

        if (Vector3.Distance(transform.position, target.position) < 1f)
        {
            target = target == patrolPoints[0] ? patrolPoints[1] : patrolPoints[0]; // Cambia al otro puesto
            currentState = State.Idle; // Vuelve al estado Idle después de regresar al puesto
        }*/
    }

    /*void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, distanceToChase);
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(homePosition1.position, 1f);
        Gizmos.DrawWireSphere(homePosition2.position, 1f);
    }*/
}
