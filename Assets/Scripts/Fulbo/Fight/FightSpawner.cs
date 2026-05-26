using UnityEngine;

public class FightSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnLimits;
    [SerializeField] private GameObject fightPrefab;

    public void SpawnFight()
    {
        // Aquí puedes agregar la lógica para generar un nuevo problema en el juego
        Debug.Log("¡Un nuevo problema ha surgido!");

        float spawnX = Random.Range(spawnLimits[0].position.x, spawnLimits[1].position.x);
        float spawnY = Random.Range(spawnLimits[0].position.y, spawnLimits[1].position.y);

        float spawnZ = Mathf.Lerp(spawnLimits[0].position.z, spawnLimits[1].position.z, Mathf.InverseLerp(spawnLimits[0].position.y, spawnLimits[1].position.y, spawnY));

        Vector3 spawnPosition = new Vector3(spawnX, spawnY, spawnZ);

        Instantiate(fightPrefab, spawnPosition, Quaternion.identity);

    }

    public void SpawnFight(Transform transform)
    {
        Instantiate(fightPrefab, transform.position, Quaternion.identity);
    }

}