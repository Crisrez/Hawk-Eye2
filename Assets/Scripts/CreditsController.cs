using UnityEngine;
using TMPro;

public class CreditsController : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 70f; // Speed at which the credits scroll
    [SerializeField] private RectTransform creditsPanel; // The panel containing the credits
    [SerializeField] private float startPositionY = -1100f; // Y position to start from
    [SerializeField] private float endPositionY = -1500f; // Y position to end at
    [SerializeField] private float delayBeforeStart = 2f; // Delay before the credits start scrolling
    [SerializeField] private float delayAfterEnd = 2f; // Delay after the credits finish scrolling
    [SerializeField] private TextMeshProUGUI creditsText; // Reference to the TextMeshProUGUI component for the credits text
    [SerializeField] private string creditsContent; // The content of the credits
    [SerializeField] private float timer = 0f; // Timer to track the delay
    [SerializeField] private bool canStart = false; // To control when the credits can start scrolling

    [SerializeField] private RectTransform finalPanel; // Reference to the final panel that appears after the credits


    void Start()
    {
        creditsPanel.offsetMax = new Vector2(creditsPanel.offsetMax.x, startPositionY); // Set the starting position of the credits panel

        var (scoreRed, scoreBlue) = GameData.GetScoreTeams();
        creditsContent = creditsContent.Replace("%goles%", (scoreRed + scoreBlue).ToString()); // Replace the placeholder with the total goals scored
        creditsContent = creditsContent.Replace("%quilombo%", GameData.GetCountProblem().ToString()); // Replace the placeholder with the total number of problems encountered

        creditsText.text = creditsContent; // Set the credits text to the content with placeholders replaced
    }

    void Update()
    {
        if (!canStart) return; // Exit the Update method until the credits can start

        timer += Time.deltaTime;

        if (timer > delayBeforeStart && -creditsPanel.offsetMax.y > endPositionY)
        {
            creditsPanel.offsetMax = new Vector2(creditsPanel.offsetMax.x, creditsPanel.offsetMax.y + scrollSpeed * Time.deltaTime);

            finalPanel.offsetMax = new Vector2(finalPanel.offsetMax.x, finalPanel.offsetMax.y + scrollSpeed * Time.deltaTime); // Move the final panel along with the credits
            finalPanel.offsetMin = new Vector2(finalPanel.offsetMin.x, finalPanel.offsetMin.y + scrollSpeed * Time.deltaTime); // Move the final panel along with the credits
        }
        else if (-creditsPanel.offsetMax.y <= endPositionY)
        {
            timer = 0f; // Reset timer for the delay after the credits finish
            if (timer > delayAfterEnd)
            {
                // Optionally, you can add code here to transition to another scene or close the credits
            }
        }
    }

    public void StartCredits()
    {
        canStart = true; // Allow the credits to start scrolling
    }

    public void SetFinalPanelActive(RectTransform rectTransform)
    {
        if (finalPanel == null)
        {
            finalPanel = rectTransform; // Set the reference to the final panel
        }
        else
        {
            Debug.LogError("Final panel reference is not set in CreditsController.");
        }
    }
}
