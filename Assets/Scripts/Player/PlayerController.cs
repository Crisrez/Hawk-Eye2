using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float shootForce = 500f;
    [SerializeField] private bool withBall = false;
    [SerializeField] private bool underAttack = false;

    [SerializeField] private GameObject ball;
    [SerializeField] private NavMeshAgent agent;
    
    [SerializeField] private Transform homePosition;
    [SerializeField] private Transform opponentGoal;
    [SerializeField] private Transform targetPosition;

    [SerializeField] private float radarRange = 10f;
    [SerializeField] private float shootRange = 15f;
    [SerializeField] private float enemyDetectionRange = 5f;

    [SerializeField] private GameObject[] teammates;
    [SerializeField] private GameObject[] opponents;

    enum PlayerState
    {
        Idle,
        Chase,
        Attack,
        Shoot,
        Pass
    }
    [SerializeField] private PlayerState currentState = PlayerState.Idle;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        targetPosition = homePosition;
    }

    void Update()
    {
        agent.speed = moveSpeed;
        agent.SetDestination(targetPosition.position);

        switch (currentState)
        {
            case PlayerState.Idle:
                Idle();
                //AnalyzeSituation();
                break;
            case PlayerState.Chase:
                Chase();
                ///AnalyzeSituation();
                break;
            case PlayerState.Attack:
                Attack();
                //AnalyzeSituation();
                break;
            case PlayerState.Shoot:
                Shoot();
                //AnalyzeSituation();
                break;
            case PlayerState.Pass:
                Pass();
                break;
        }
    }

    void Idle()
    {
        targetPosition = homePosition;
        AnalyzeSituation();
    }

    void Chase()
    {
        targetPosition = ball.transform;
        AnalyzeSituation();
    }

    void Attack()
    {
        targetPosition = opponentGoal.transform;
        AnalyzeSituation();
    }

    void Shoot()
    {
        ball.transform.SetParent(null); // Detach the ball from the player
        ball.GetComponent<Rigidbody>().AddForce((opponentGoal.position - transform.position).normalized * shootForce);
        withBall = false;
        AnalyzeSituation();
    }

    void Pass()
    {
        GameObject bestTeammate = null;
        float bestDistance = Mathf.Infinity;

        foreach (GameObject teammate in teammates)
        {
            if (teammate != this.gameObject) // Don't pass to self
            {
                float distanceToTeammate = Vector3.Distance(transform.position, teammate.transform.position);
                if (distanceToTeammate <= bestDistance)
                {
                    bestDistance = distanceToTeammate;
                    if (distanceToTeammate <= Vector3.Distance(transform.position, opponentGoal.position)) // Only consider teammates within shooting range
                        bestTeammate = teammate;
                }
            }
        }

        // For simplicity, pass to the best teammate
        if (bestTeammate != null)
        {
            ball.transform.SetParent(null); // Detach the ball from the player
            ball.GetComponent<Rigidbody>().AddForce((bestTeammate.transform.position - transform.position).normalized * shootForce);
            withBall = false;
        }
        AnalyzeSituation();
    }

    void AnalyzeSituation()
    {
        float distanceToBall = Vector3.Distance(transform.position, ball.transform.position);
        float distanceToGoal = Vector3.Distance(transform.position, opponentGoal.position);

        if (underAttack && withBall)
        {
            currentState = PlayerState.Pass;
        }
        else if (withBall)
        {
            if (distanceToGoal <= shootRange)
            {
                currentState = PlayerState.Shoot;
            }
            else
            {
                currentState = PlayerState.Attack;
            }
        }
        else
        {
            if (distanceToBall <= radarRange /*&& distanceToBall < chaseRange*/)
            {
                currentState = PlayerState.Chase;
            }
            else
            {
                currentState = PlayerState.Idle;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            //GameObject ball = collision.gameObject;

            withBall = true;
            ball.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            ball.transform.position = transform.position + transform.forward * 1f; // Position the ball in front of the player
            ball.transform.SetParent(transform); // Make the ball a child of the player to move together
        }
    }

    void OnTriggerEnter(Collider other)
    {
        foreach (GameObject opponent in opponents)
        {
            if (other.gameObject == opponent)
            {
                underAttack = true;
            }
        }
    }























    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radarRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyDetectionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, shootRange);
    }
}
