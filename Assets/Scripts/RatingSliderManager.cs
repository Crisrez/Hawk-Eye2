using UnityEngine;
using UnityEngine.UI;

public class RatingSliderManager : MonoBehaviour
{
    [SerializeField] private Slider sliderFulbo;
    [SerializeField] private Slider sliderSoccer;

    void Start()
    {
        GameData.SetScoreFulbo(0);
        GameData.SetScoreSoccer(0);
    }

    void Update()
    {
        sliderFulbo.value = GameData.GetScoreFulbo();
        sliderSoccer.value = GameData.GetScoreSoccer();
    }

}
