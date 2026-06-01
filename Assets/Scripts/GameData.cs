using UnityEngine;
using System.Collections.Generic;

public static class GameData
{
    private static int scoreTeamRed = 0;
    private static int scoreTeamBlue = 0;
    private static int membersPerTeam;
    private static List<GameObject> teamRed = new List<GameObject>();
    private static List<GameObject> teamBlue = new List<GameObject>();

    private static List<GameObject> homesPositions = new List<GameObject>();

    private static float scoreFulbo;
    private static float scoreSoccer;

    private static float durationFight = 5f;

    private static float volumenGame;

    private static Team posessionBall = Team.Red;

    private static MatchState matchState = MatchState.Starting;

    public static MatchState MatchState
    {
        get => matchState;
        set => matchState = value;
    }

    public static void SetHomesPositions(GameObject father)
    {
        homesPositions.Clear();

        foreach (Transform child in father.transform)
        {
            homesPositions.Add(child.gameObject);
        }
    }

    public static List<GameObject> GetHomesPositions()
    {
        return homesPositions;
    }

    public static void SetMembersPerTeam(int members)
    {
        membersPerTeam = members;
        teamRed = new List<GameObject>(membersPerTeam);
        teamBlue = new List<GameObject>(membersPerTeam);
    }

    public static void AddGoaltoTeam(Team team)
    {
        switch (team)
        {
            case Team.Red:
                scoreTeamRed++;
                break;
            case Team.Blue:
                scoreTeamBlue++;
                break;
            default:
                Debug.LogError("Invalid team identifier. Use 'Red' for Red or 'Blue' for Blue.");
                break;
        }
    }

    public static (int, int) GetScoreTeams()
    {
        return (scoreTeamRed, scoreTeamBlue);
    }

    public static int GetMembersPerTeam()
    {
        return membersPerTeam;
    }

    public static void SetTeamRed(GameObject member)
    {
        teamRed.Add(member);
    }

    public static void SetTeamBlue(GameObject member)
    {
        teamBlue.Add(member);
    }

    public static List<GameObject> GetTeamRed()
    {
        return teamRed;
    }

    public static List<GameObject> GetTeamBlue()
    {
        return teamBlue;
    }

    public static void ClearTeams()
    {
        teamRed.Clear();
        teamBlue.Clear();
    }

    public static void SetScoreFulbo(float score)
    {
        scoreFulbo = Mathf.Clamp(scoreFulbo + score, 0, 100);
    }

    public static float GetScoreFulbo()
    {
        return scoreFulbo;
    }

    public static void SetScoreSoccer(float score)
    {
        scoreSoccer = Mathf.Clamp(scoreSoccer + score, 0, 100);
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

    public static float GetDurationFight()
    {
        return durationFight;
    }

    public static void SetPosessionBall(Team team)
    {
        posessionBall = team;
    }

    

    public static Team? GetPosessionBall()
    {
        return posessionBall;
    }


    public static void ResetData()
    {
        scoreTeamRed = 0;
        scoreTeamBlue = 0;
        membersPerTeam = 0;
        teamRed.Clear();
        teamBlue.Clear();
        homesPositions.Clear();
        scoreFulbo = 0f;
        scoreSoccer = 0f;
        durationFight = 5f;
        volumenGame = 1f; // Default volume
        posessionBall = Team.Red; // Default possession
        matchState = MatchState.Starting; // Default match state
    }

}
