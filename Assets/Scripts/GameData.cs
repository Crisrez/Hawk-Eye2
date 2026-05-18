using UnityEngine;

public static class GameData
{
    [SerializeField] private static int scoreTeamA = 0;
    [SerializeField] private static int scoreTeamB = 0;
    [SerializeField] private static GameObject[] teamA, teamB;

    public static int ScoreTeamA
    {
        get => scoreTeamA;
        set => scoreTeamA = value;
    }

    public static int ScoreTeamB
    {
        get => scoreTeamB;
        set => scoreTeamB = value;
    }

    public static GameObject[] TeamA
    {
        get => teamA;
        set => teamA = value;
    }

    public static GameObject[] TeamB
    {
        get => teamB;
        set => teamB = value;
    }
}
