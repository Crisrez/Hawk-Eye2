using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TitleChanger : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title; // Reference to the TextMeshProUGUI component for the title text
    [SerializeField] private string[] possiblesTitles; // Array of possible titles to choose from
    [SerializeField] private int indexTitle = 0; // Static index to keep track of the current title

    void Start()
    {
        title.text = possiblesTitles[0];

        //indexTitle = (indexTitle + 1) % possiblesTitles.Length;
    }

    public void Left() 
    {
        indexTitle = (indexTitle - 1 + possiblesTitles.Length) % possiblesTitles.Length; 
        title.text = possiblesTitles[indexTitle]; 
    }

    public void Right() 
    { 
        indexTitle = (indexTitle + 1) % possiblesTitles.Length; 
        title.text = possiblesTitles[indexTitle]; 
    }

}
