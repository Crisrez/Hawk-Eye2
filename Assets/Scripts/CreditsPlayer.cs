using UnityEngine;

public class CreditsPlayer : MonoBehaviour
{
    [SerializeField] private CreditsController creditsController; // Reference to the CreditsController script
    [SerializeField] private RectTransform finalPanel; // Reference to the final panel RectTransform

    public void PlayCredits()
    {
        if (creditsController != null)
        {
            creditsController.StartCredits(); // Call the method to start the credits
            creditsController.SetFinalPanelActive(finalPanel); // Set the final panel active
        }
        else
        {
            Debug.LogError("CreditsController reference is not set in CreditsPlayer.");
        }
    }

}
