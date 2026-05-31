using UnityEngine;
using Unity.Cinemachine;
using System.Collections;

public class CameraControlMenu : MonoBehaviour
{
    [SerializeField] private CinemachineCamera menu, tutorial, config;
    [SerializeField] private GameObject menuUI, tutorialUI, configUI;

    public void ChangeToMenu()
    {
        menu.Priority = 10;
        tutorial.Priority = 0;
        config.Priority = 0;

        tutorialUI.SetActive(false);
        configUI.SetActive(false);

        StartCoroutine(ActiveUI(menuUI, 2f));
    }

    public void ChangeToTutorial()
    {
        menu.Priority = 0;
        tutorial.Priority = 10;
        config.Priority = 0;

        menuUI.SetActive(false);
        configUI.SetActive(false);

        StartCoroutine(ActiveUI(tutorialUI, 2f));
    }
     
    public void ChangeToConfig()
    {
        menu.Priority = 0;
        tutorial.Priority = 0;
        config.Priority = 10;

        menuUI.SetActive(false);
        tutorialUI.SetActive(false);

        StartCoroutine(ActiveUI(configUI, 2f));
    }

    IEnumerator ActiveUI(GameObject uI, float delay)
    {
        yield return new WaitForSeconds(delay);
        uI.SetActive(true);
    }
}
