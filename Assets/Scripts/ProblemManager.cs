using UnityEngine;
using UnityEngine.InputSystem;

public class ProblemManager : MonoBehaviour
{
    [SerializeField] private FanaticSpawner fanaticSpawner;
    [SerializeField] private FightSpawner fightSpawner;
    [SerializeField] private float timer = 0f;
    [SerializeField] private float interval = 5f;
    [SerializeField] private float probability;
    [SerializeField] private float minProbability = 10f;
    [SerializeField] private float maxProbability = 100f;

    void Update()
    {
        if (GameData.MatchState != MatchState.Playing)
        {
            return;
        }

        probability = Mathf.Lerp(minProbability, maxProbability, GameData.GetScoreFulbo() / 100f); // Incrementa la probabilidad de problemas en función del puntaje de fulbo

        //Debug.Log($"Probability: {probability}%");

        timer += Time.deltaTime;

        if (timer >= interval)
        {
            timer = 0f;
            if (Random.Range(0f, 100f) < probability)
            {
                Debug.Log("A problem has occurred!");
                SpawnProblem();
            }
            else
            {
                Debug.Log("No problem this time.");
            }
        }


        if (probability >= 90f)
        {
            interval = 3f; // Reduce el intervalo a 3 segundos cuando la probabilidad alcanza el máximo
        }
        else
        {
            interval = 5f; // Mantén el intervalo en 5 segundos mientras la probabilidad es menor que el máximo
        }
    }

    void SpawnProblem()
    {
        float randomValue = Random.Range(0,2);

        switch (randomValue)
        {
            case 0:
                fightSpawner.SpawnFight();
                break;
            case 1:
                fanaticSpawner.SpawnFanatic();
                break;
        }

        //(Random.value < 0.5f ? (System.Action)fightSpawner.SpawnFight : fanaticSpawner.SpawnFanatic)();
    }
}
