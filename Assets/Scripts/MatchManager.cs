using UnityEngine;
using System.Collections.Generic;

public class MatchManager : MonoBehaviour
{
    [SerializeField] private float matchDuration = 90f; // Duration of the match in seconds
    [SerializeField] private float timer;
    [SerializeField] private float preMatchDelay = 5f; // Time to wait before starting the match after all players are in position

    [SerializeField] private GameObject prefabFootballer, ball;
    [SerializeField] private Transform gateRed, gateBlue;
    [SerializeField] private Transform goalRed, goalBlue;

    void Start()
    {
        timer = matchDuration;
        GameData.MatchState = MatchState.Starting;
        
        for (int i = 0; i < GameData.GetMembersPerTeam(); i++)
        {
            GameObject playerRed = Instantiate(prefabFootballer, gateRed.position, Quaternion.identity);
            GameData.SetTeamRed(playerRed);
            GameObject playerBlue = Instantiate(prefabFootballer, gateBlue.position, Quaternion.identity);
            GameData.SetTeamBlue(playerBlue);
        }

        foreach (GameObject player in GameData.GetTeamRed())
        {
            PlayerData dataPlayer = player.GetComponent<PlayerData>();
            dataPlayer.SetTeam(Team.Red);
            dataPlayer.SetTeammates(GameData.GetTeamRed());
            dataPlayer.SetOpponents(GameData.GetTeamBlue());
            dataPlayer.SetOpponentGoal(goalBlue);
            
            player.GetComponent<PlayerController>().SetBall(ball);

            dataPlayer.SetHomePosition(GameData.GetHomesPositions()[GameData.GetTeamRed().IndexOf(player)].transform);
        }
        foreach (GameObject player in GameData.GetTeamBlue())
        {
            PlayerData dataPlayer = player.GetComponent<PlayerData>();
            dataPlayer.SetTeam(Team.Blue);
            dataPlayer.SetTeammates(GameData.GetTeamBlue());
            dataPlayer.SetOpponents(GameData.GetTeamRed());
            dataPlayer.SetOpponentGoal(goalRed);

            player.GetComponent<PlayerController>().SetBall(ball);

            dataPlayer.SetHomePosition(GameData.GetHomesPositions()[GameData.GetTeamRed().Count + GameData.GetTeamBlue().IndexOf(player)].transform);
        }



    }

    void Update()
    {
        if (GameData.MatchState == MatchState.Playing)
        {
            //ball.GetComponent<BallController>().ResetPosition();

            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                EndMatch();
            }
        }
        else
        {
            CheckPlayersInPosition();
        }
    }

    void EndMatch()
    {
        GameData.MatchState = MatchState.Ended;
        Debug.Log("Match Ended!");
        // Here you can add logic to determine the winner, update scores, etc.

        foreach (GameObject player in GameData.GetTeamRed())
        {
            player.GetComponent<PlayerController>().SetTargetEnd(gateRed);
        }
        foreach (GameObject player in GameData.GetTeamBlue())
        {
            player.GetComponent<PlayerController>().SetTargetEnd(gateBlue);
        }
    }

    void CheckPlayersInPosition()
    {
        foreach (GameObject player in GameData.GetTeamRed())
        {
            if (!player.GetComponent<PlayerData>().GetInPosition())
            {
                return; // If any player is not in position, return early
            }
        }
        foreach (GameObject player in GameData.GetTeamBlue())
        {
            if (!player.GetComponent<PlayerData>().GetInPosition())
            {
                return; // If any player is not in position, return early
            }
        }

        Invoke("StartMatch", preMatchDelay); // Start the match after a short delay to ensure all players are in position
        ball.GetComponent<BallController>().ResetPosition();

    }

    void StartMatch()
    {
        GameData.MatchState = MatchState.Playing;
    }

    public float GetTimeRemaining()
    {
        return timer;
    }

}
