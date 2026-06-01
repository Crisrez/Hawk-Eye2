using UnityEngine;
using Unity.Cinemachine;

public class PRUEBAS : MonoBehaviour
{
    //[SerializeField] private GameObject[] red, blue;
    [SerializeField] private GameObject posiciones;

    [SerializeField] private CinemachineCamera menu, aerea;

    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void ElegirPosiciones()
    {
        GameData.SetMembersPerTeam(3);
        GameData.SetHomesPositions(posiciones);
    }

    public void CambioDeCamaras()
    {
        menu.Priority = 0;
        aerea.Priority = 1;
    }
}
