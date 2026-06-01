using UnityEngine;
using TMPro;

public class AnimationTextTutorial : MonoBehaviour
{
    [SerializeField] private float duration = 2f; // Duración de la animación en segundos
    [SerializeField] private float fontSizeMin = 10f; // Tamaño de fuente mínimo
    [SerializeField] private float fontSizeMax = 30f; // Tamaño de fuente máximo
    [SerializeField] private TextMeshProUGUI textMeshPro; // Referencia al componente TextMeshPro

    void Update()
    {
        // Calcula el tamaño de fuente basado en el tiempo transcurrido
        float t = Mathf.PingPong(Time.time, duration) / duration; // Normaliza el tiempo entre 0 y 1
        float fontSize = Mathf.Lerp(fontSizeMin, fontSizeMax, t); // Interpola entre el tamaño mínimo y máximo
        // Aplica el tamaño de fuente al componente TextMeshPro
        textMeshPro.fontSize = fontSize;

    }
}
