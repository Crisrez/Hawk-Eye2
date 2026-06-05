using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class EndingController : MonoBehaviour
{
    [SerializeField] private GameObject endingCanvas, endFulbo, endSoccer, endFired;
    [SerializeField] private float timeToShowEnding = 3f;
    [SerializeField] private float minimoScoreToWin = 10f;

    void Start()
    {
        endingCanvas.SetActive(false);
        endFulbo.SetActive(false);
        endSoccer.SetActive(false);
        endFired.SetActive(false);
    }

    void Update()
    {
        if (GameData.MatchState == MatchState.Ended)
        {
            Invoke(nameof(TurnOffTV), timeToShowEnding);
        }
    }

    void TurnOffTV()
    {
        endingCanvas.SetActive(true);
        Cursor.visible = true;
        Invoke(nameof(ShowEnding), timeToShowEnding / 2f);
    }

    void ShowEnding()
    {
        if (GameData.GetScoreFulbo() < minimoScoreToWin && GameData.GetScoreSoccer() < minimoScoreToWin)
        {
            endFired.SetActive(true);
        }
        else if (GameData.GetScoreFulbo() > GameData.GetScoreSoccer())
        {
            endFulbo.SetActive(true);
        }
        else if (GameData.GetScoreSoccer() > GameData.GetScoreFulbo())
        {
            endSoccer.SetActive(true);
        }

    }
}
