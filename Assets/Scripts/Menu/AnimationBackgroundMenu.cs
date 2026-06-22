using UnityEngine;
using TMPro;

public class AnimationBackgroundMenu : MonoBehaviour
{
    [SerializeField] private float timer, interval;
    [SerializeField] private GameObject fanatic, security, fight, dron;
    [SerializeField] private Vector3 posFanatic, posSecurity, posDron;
    [SerializeField] private Transform limitLeft, limitRight;
    [SerializeField] private System.Action[] possibleProblems;

    void Start()
    {
        posFanatic = fanatic.transform.position;
        posSecurity = security.transform.position;
        posDron = dron.transform.position;

        fanatic.SetActive(false);
        security.SetActive(false);
        fight.SetActive(false);
        dron.SetActive(false);

        possibleProblems = new System.Action[]
        {
            OnFight
        };
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > interval)
        {
            timer = 0;

            if (possibleProblems != null)
            {
                int indexProblem = Random.Range(0, possibleProblems.Length);

                possibleProblems[indexProblem]?.Invoke();
            }

        }

    }

    void OnFight()
    {
        fight.transform.position = SetRandomPosition();
        fight.SetActive(true);
    }

    Vector3 SetRandomPosition()
    {
        float zRandom = Random.Range(limitLeft.position.z, limitRight.position.z);

        return new Vector3(limitLeft.position.x, limitRight.position.y, zRandom);
    }
}
