using UnityEngine;
using UnityEngine.AI;

public class FanaticSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fanaticPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject[] players;

    public void SetFanatic()
    {
        int randomPlayerIndex = Random.Range(0, players.Length);
        float randomXSpawn = Random.Range(spawnPoints[0].position.x, spawnPoints[1].position.x);
        int randomZSpawn = Random.Range(0,2);

        if (randomZSpawn == 0)
            randomZSpawn = (int)spawnPoints[0].position.z;
        else
            randomZSpawn = (int)spawnPoints[1].position.z;

        Vector3 spawn = new Vector3(randomXSpawn, spawnPoints[0].position.y, randomZSpawn);

        GameObject playerTarget = players[randomPlayerIndex];
        GameObject fanatic = Instantiate(fanaticPrefab, spawn, Quaternion.identity);

        FanaticController controller = fanatic.GetComponent<FanaticController>();

        if (controller != null)
            controller.SetTarget(playerTarget);
    }
}
