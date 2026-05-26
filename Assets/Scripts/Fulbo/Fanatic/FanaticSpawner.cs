using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class FanaticSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fanaticPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] players;

    void Start()
    {
        //players = new GameObject[GameData.GetTeamRed().Count + GameData.GetTeamBlue().Count];
        //players = GameData.GetTeamRed().Concat(GameData.GetTeamBlue()).ToArray();
    }

    void Update()
    {
        if (players.Length == 0)
            players = GameData.GetTeamRed().Concat(GameData.GetTeamBlue()).ToArray();
    }

    public void SpawnFanatic()
    {
        int randomPlayerIndex = Random.Range(0, players.Length);
        float randomXSpawn = Random.Range(spawnPoints[0].position.x, spawnPoints[1].position.x);
        float randomZSpawn = Random.value < 0.5f ? spawnPoints[0].position.z : spawnPoints[1].position.z;
        /*int randomZSpawn = Random.Range(0,2);

        if (randomZSpawn == 0)
            randomZSpawn = (int)spawnPoints[0].position.z;
        else
            randomZSpawn = (int)spawnPoints[1].position.z;*/

        Vector3 spawn = new Vector3(randomXSpawn, 0.5f, randomZSpawn);

        GameObject playerTarget = players[randomPlayerIndex];
        GameObject fanatic = Instantiate(fanaticPrefab, spawn, Quaternion.identity);

        FanaticController controller = fanatic.GetComponent<FanaticController>();

        if (controller != null)
            controller.SetTarget(playerTarget);
    }
}
