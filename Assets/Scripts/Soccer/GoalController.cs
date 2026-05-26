using UnityEngine;
using System;

public class GoalController : MonoBehaviour
{
    [SerializeField] private BallController ballController;
    [SerializeField] private Team teamIdentifier; // 'Red' or 'Blue' to identify which team this goal belongs to
    //public static Action<int> OnGoalScored;

    void Start()
    {
        //GoalController.OnGoalScored += HandleGoalScored;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            GameData.MatchState = MatchState.Starting;
            ballController = other.GetComponent<BallController>();
            GameData.AddGoaltoTeam(teamIdentifier); // Assuming team A scores when the ball enters this goal
            Debug.Log("HOLA");
            ballController.Gol();
            //OnGoalScored?.Invoke();
        }
    }

    private void HandleGoalScored()
    {
        // Handle goal scored event here
    }
}
