using UnityEngine;
using TMPro;

public class CreditsController : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 20f; // Speed at which the credits scroll
    [SerializeField] private RectTransform creditsPanel; // The panel containing the credits
    [SerializeField] private float startPositionY = 1000f; // Y position to start from
    [SerializeField] private float endPositionY = -1000f; // Y position to end at
    [SerializeField] private float delayBeforeStart = 2f; // Delay before the credits start scrolling
    [SerializeField] private float delayAfterEnd = 2f; // Delay after the credits finish scrolling
    [SerializeField] private TextMeshProUGUI creditsText; // Reference to the TextMeshProUGUI component for the credits text
    [SerializeField] private string creditsContent; // The content of the credits
    [SerializeField] private float timer = 0f; // Timer to track the delay


    void Start()
    {
        creditsPanel.localPosition = new Vector3(creditsPanel.localPosition.x, startPositionY, creditsPanel.localPosition.z);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > delayBeforeStart && creditsPanel.localPosition.y > endPositionY)
        {
            creditsPanel.localPosition += Vector3.up * scrollSpeed * Time.deltaTime;
        }
        else if (creditsPanel.localPosition.y <= endPositionY)
        {
            timer = 0f; // Reset timer for the delay after the credits finish
            if (timer > delayAfterEnd)
            {
                // Optionally, you can add code here to transition to another scene or close the credits
            }
        }
    }
}
