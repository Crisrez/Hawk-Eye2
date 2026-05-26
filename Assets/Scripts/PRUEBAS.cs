using UnityEngine;

public class PRUEBAS : MonoBehaviour
{
    //[SerializeField] private GameObject[] red, blue;
    [SerializeField] private GameObject posiciones;

    void Start()
    {
        /*foreach (GameObject player in red)
        {
            GameData.SetTeamRed(player);
        }
        foreach (GameObject player in blue)
        {
            GameData.SetTeamBlue(player);
        }*/
    }
    void Update()
    {
        
    }

    public void ElegirPosiciones()
    {
        GameData.SetMembersPerTeam(3);
        GameData.SetHomesPositions(posiciones);
    }
}
