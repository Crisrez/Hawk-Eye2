using UnityEngine;
using System.Collections.Generic;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private Team myTeam;

    [SerializeField] private List<GameObject> teammates;
    [SerializeField] private List<GameObject> opponents;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float shootForce = 500f;
    [SerializeField] private bool withBall = false;
    [SerializeField] private bool underAttack = false;
    [SerializeField] private bool inPosition = false;

    [SerializeField] private Transform homePosition;
    [SerializeField] private Transform opponentGoal;

    [SerializeField] private float radarRange = 10f;
    [SerializeField] private float shootRange = 15f;
    [SerializeField] private float enemyDetectionRange = 5f;

    [SerializeField] private PlayerState currentState = PlayerState.Idle;

    [SerializeField] private TeamState teamState;

    public Team MyTeam => myTeam;
    public void SetTeam(Team team)
    {
        myTeam = team;
    }

    public List<GameObject> Teammates => teammates;
    public void SetTeammates(List<GameObject> teamMates)
    {
        teammates = teamMates;
    }

    public List<GameObject> Opponents => opponents;
    public void SetOpponents(List<GameObject> opponents)
    {
        this.opponents = opponents;
    }

    public float MoveSpeed => moveSpeed;

    public float ShootForce => shootForce;

    public bool WithBall => withBall;

    public bool UnderAttack => underAttack;

    public Transform HomePosition => homePosition;
    public void SetHomePosition(Transform position)
    {
        homePosition = position;
    }

    public Transform OpponentGoal => opponentGoal;
    public void SetOpponentGoal(Transform goal)
    {
        opponentGoal = goal;
    }

    public float RadarRange => radarRange;

    public float ShootRange => shootRange;

    public float EnemyDetectionRange => enemyDetectionRange;

    public PlayerState CurrentState => currentState;

    public TeamState TeamState
    {
        get => teamState;
        set => teamState = value;
    }

    public void SetWithBall(bool value)
    {
        withBall = value;
    }

    public void SetUnderAttack(bool value)
    {
        underAttack = value;
    }

    public void SetCurrentState(PlayerState newState)
    {
        currentState = newState;
    }

    public void SetInPosition(bool value)
    {
        inPosition = value;
    }

    public bool GetInPosition()
    {
        return inPosition;
    }
}