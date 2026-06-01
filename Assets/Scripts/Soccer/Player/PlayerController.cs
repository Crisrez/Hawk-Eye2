using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform targetPosition;

    [SerializeField] private PlayerData playerData;

    void Start()
    {
        playerData = GetComponent<PlayerData>();
        agent = GetComponent<NavMeshAgent>();
        targetPosition = playerData.HomePosition;
    }

    void Update()
    {
        if (GameData.MatchState == MatchState.Ended)
        {
            agent.SetDestination(targetPosition.position);

            return;
        }

        playerData.SetInPosition(Vector3.Distance(transform.position, playerData.HomePosition.position) < 1.1f);

        if (playerData.WithBall)
        {
            GameData.SetPosessionBall(playerData.MyTeam);
        }
        else if (playerData.GetInPosition())
        {
            Vector3 ballGround = new Vector3(ball.transform.position.x, transform.position.y, ball.transform.position.z);

            transform.LookAt(ballGround); // Face the target position when not moving
        }

        playerData.TeamState = GameData.GetPosessionBall() != playerData.MyTeam ? TeamState.Defensive : TeamState.Offensive;

        agent.speed = playerData.MoveSpeed;
        agent.SetDestination(targetPosition.position);

        switch (playerData.CurrentState)
        {
            case PlayerState.Idle:
                Idle();
                break;
            case PlayerState.Chase:
                Chase();
                break;
            case PlayerState.Attack:
                Attack();
                break;
            case PlayerState.Shoot:
                Shoot();
                break;
            case PlayerState.Pass:
                Pass();
                break;
        }
    }

    void Idle()
    {
        targetPosition = playerData.HomePosition;
        AnalyzeSituation();
    }

    void Chase()
    {
        targetPosition = ball.transform;
        AnalyzeSituation();
    }

    void Attack()
    {
        targetPosition = playerData.OpponentGoal;
        AnalyzeSituation();
    }

    void Shoot()
    {
        if (!playerData.WithBall){return; } // Can't shoot if we don't have the ball

        transform.LookAt(playerData.OpponentGoal); // Face the opponent's goal

        ball.transform.SetParent(null); // Detach the ball from the player

        //ball.GetComponent<Rigidbody>().isKinematic = false; // Make the ball non-kinematic to be affected by physics

        ball.GetComponent<Rigidbody>().AddForce((playerData.OpponentGoal.position - transform.position).normalized * playerData.ShootForce);
        playerData.SetWithBall(false);
        Invoke("AnalyzeSituation", 1f);
    }

    void Pass()
    {
        GameObject bestTeammate = null;
        float bestDistance = Mathf.Infinity;

        foreach (GameObject teammate in playerData.Teammates)
        {
            if (teammate != this.gameObject) // Don't pass to self
            {
                float distanceToTeammate = Vector3.Distance(transform.position, teammate.transform.position);
                if (distanceToTeammate <= bestDistance)
                {
                    bestDistance = distanceToTeammate;
                    if (distanceToTeammate <= Vector3.Distance(transform.position, playerData.OpponentGoal.position)) // Only consider teammates within shooting range
                        bestTeammate = teammate;
                }
            }
        }

        // For simplicity, pass to the best teammate
        if (bestTeammate != null )
        {
            ball.transform.SetParent(null); // Detach the ball from the player

            //ball.GetComponent<Rigidbody>().isKinematic = false; // Make the ball non-kinematic to be affected by physics

            ball.GetComponent<Rigidbody>().AddForce((bestTeammate.transform.position - transform.position).normalized * playerData.ShootForce * 0.8f);
            playerData.SetWithBall(false);
        }
        Invoke("AnalyzeSituation", .3f);
    }

    void AnalyzeSituation()
    {
        float distanceToBall = Vector3.Distance(transform.position, ball.transform.position);
        float distanceToGoal = Vector3.Distance(transform.position, playerData.OpponentGoal.position);

        if (GameData.MatchState == MatchState.Starting)
        {
            playerData.SetCurrentState(PlayerState.Idle);
            return;
        }
        else if (GameData.MatchState == MatchState.Playing)
        {

            if (playerData.UnderAttack && playerData.WithBall)
            {
                playerData.SetCurrentState(PlayerState.Pass);
            }
            else if (playerData.WithBall)
            {
                if (distanceToGoal <= playerData.ShootRange)
                {
                    playerData.SetCurrentState(PlayerState.Shoot);
                }
                else
                {
                    playerData.SetCurrentState(PlayerState.Attack);
                }
            }
            else
            {
                if (distanceToBall <= playerData.RadarRange)
                {
                    playerData.SetCurrentState(PlayerState.Chase);
                }
                else
                {
                    playerData.SetCurrentState(PlayerState.Idle);
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            if (!playerData.WithBall)
            {
                ObtainBall();
            }
        }
    }

    void ObtainBall()
    {
        playerData.SetWithBall(true);

        //ball.GetComponent<Rigidbody>().linearVelocity = Vector3.zero; // Stop any existing velocity
        //ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; // Stop any existing angular velocity
        //ball.GetComponent<Rigidbody>().isKinematic = true; // Make the ball kinematic to follow the player

        ball.transform.position = transform.position + transform.forward; // Position the ball in front of the player
        ball.transform.SetParent(transform); // Make the ball a child of the player to move together
    }

    void OnTriggerEnter(Collider other)
    {
        foreach (GameObject opponent in playerData.Opponents)
        {
            if (other.gameObject == opponent)
            {
                playerData.SetUnderAttack(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        foreach (GameObject opponent in playerData.Opponents)
        {
            if (other.gameObject == opponent)
            {
                playerData.SetUnderAttack(false);
            }
        }
    }

    public void SetBall(GameObject ball)
    {
        this.ball = ball;
    }

    public void SetTargetEnd(Transform target)
    {
        targetPosition = target;
    }






















    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerData.RadarRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, playerData.EnemyDetectionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, playerData.ShootRange);
    }
}
