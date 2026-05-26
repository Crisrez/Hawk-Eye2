using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FanaticSpawner fanaticSpawner;
    [SerializeField] private FightSpawner fightSpawner;
    [SerializeField] private float timer = 0f;
    [SerializeField] private float problemInterval = 5f;
    [SerializeField] private float problemProbability = 10f;

    void Start()
    {
        
    }

    void Update()
    {
        problemProbability += GameData.GetScoreFulbo() * 0.5f; // Incrementa la probabilidad de problemas en función del puntaje de fulbo
        timer += Time.deltaTime;

        if (timer >= problemInterval)
        {
            timer = 0f;
            if (Random.Range(0f, 100f) < problemProbability)
            {
                SpawnProblem();
            }
        }


        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            fanaticSpawner.SpawnFanatic();
            fightSpawner.SpawnFight();
        }
    }

    void SpawnProblem()
    {
        (Random.value < 0.5f ? (System.Action)fightSpawner.SpawnFight : fanaticSpawner.SpawnFanatic)();
    }
}
