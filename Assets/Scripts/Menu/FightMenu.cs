using UnityEngine;

public class FightMenu : MonoBehaviour
{
    float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 3)
        {
            timer = 0f;
            // Aquí puedes agregar la lógica para finalizar la pelea, como determinar el ganador o reiniciar el juego
            Debug.Log("La pelea ha terminado.");
            this.gameObject.SetActive(false);
        }
    }
}
