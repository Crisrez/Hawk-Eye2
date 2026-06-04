using UnityEngine;
using TMPro;

public class TimerMatch : MonoBehaviour
{
    [SerializeField] private MatchManager matchManager;
    [SerializeField] private TextMeshProUGUI tMProTimer;

    void Update()
    {
        if (matchManager.GetTimeRemaining() <= 0)
        {
            tMProTimer.text = "90:00";
            return;
        }

        // 1. Obtenemos el tiempo real transcurrido (de 0 a 90 segundos)
        // Si GetTimeRemaining() cuenta hacia atrás (de 90 a 0), restamos para saber el tiempo transcurrido
        float tiempoRealTranscurrido = 90f - matchManager.GetTimeRemaining();

        // 2. Convertimos el tiempo real a "Minutos Virtuales"
        // Como 1 segundo real = 1 minuto virtual, el tiempo transcurrido son nuestros minutos.
        int minutosVirtuales = Mathf.FloorToInt(tiempoRealTranscurrido);

        // 3. Calculamos los "Segundos Virtuales"
        // Extraemos la parte decimal del segundo real (lo que va entre segundo y segundo) 
        // y lo multiplicamos por 60 para transformarlo en segundos virtuales.
        float parteDecimal = tiempoRealTranscurrido % 1f;
        int segundosVirtuales = Mathf.FloorToInt(parteDecimal * 60f);

        // 4. Mostramos el resultado (Iría desde 00:00 hasta 90:00)
        tMProTimer.text = $"{minutosVirtuales:00}:{segundosVirtuales:00}";

    }
}
