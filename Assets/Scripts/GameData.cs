using UnityEngine;
using System.Collections.Generic;

public static class GameData
{
    [SerializeField] private static int scoreTeamA = 0;
    [SerializeField] private static int scoreTeamB = 0;
    [SerializeField] private static List<GameObject> teamA, teamB;
    [SerializeField] private static int membersPerTeam;
    [SerializeField] private static float scoreFulbo;
    [SerializeField] private static float scoreSoccer;
    [SerializeField] private static float volumenGame;

    public static void SetMembersPerTeam(int members)
    {
        membersPerTeam = members;
        teamA = new List<GameObject>(membersPerTeam);
        teamB = new List<GameObject>(membersPerTeam);
    }

    public static void AddGoaltoTeam(char team)
    {
        switch (team)
        {
            case 'A':
                scoreTeamA++;
                break;
            case 'B':
                scoreTeamB++;
                break;
            default:
                Debug.LogError("Invalid team identifier. Use 'A' or 'B'.");
                break;
        }
    }

    public static (int, int) GetScoreTeams()
    {
        return (scoreTeamA, scoreTeamB);
    }

    public static int GetMembersPerTeam()
    {
        return membersPerTeam;
    }

    public static void SetTeamA(GameObject member)
    {
        teamA.Add(member);
    }

    public static void SetTeamB(GameObject member)
    {
        teamB.Add(member);
    }

    public static List<GameObject> GetTeamA()
    {
        return teamA;
    }

    public static List<GameObject> GetTeamB()
    {
        return teamB;
    }

    public static void ClearTeams()
    {
        teamA.Clear();
        teamB.Clear();
    }

    public static void SetScoreFulbo(float score)
    {
        scoreFulbo = score;
    }

    public static float GetScoreFulbo()
    {
        return scoreFulbo;
    }

    public static void SetScoreSoccer(float score)
    {
        scoreSoccer = score;
    }

    public static float GetScoreSoccer()
    {
        return scoreSoccer;
    }

    public static void SetVolumenGame(float volumen)
    {
        volumenGame = volumen;
    }

    public static float GetVolumenGame()
    {
        return volumenGame;
    }

}
