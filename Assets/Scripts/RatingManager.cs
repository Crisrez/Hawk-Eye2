using UnityEngine;
using UnityEngine.UI;

public class RatingManager : MonoBehaviour
{
    [SerializeField] private Slider sliderFulbo;
    [SerializeField] private Slider sliderSoccer;

    [SerializeField] private float ratingLosePerSecond = 0.1f;

    void Start()
    {
        GameData.SetScoreFulbo(0);
        GameData.SetScoreSoccer(0);
    }

    void Update()
    {
        sliderFulbo.value = GameData.GetScoreFulbo();
        sliderSoccer.value = GameData.GetScoreSoccer();

        if (sliderFulbo.value > 0)
        {
            GameData.SetScoreFulbo(-ratingLosePerSecond * Time.deltaTime);
        }
        
        if (sliderSoccer.value > 0)
        {
            GameData.SetScoreSoccer(-ratingLosePerSecond * Time.deltaTime);
        }
    }

}
