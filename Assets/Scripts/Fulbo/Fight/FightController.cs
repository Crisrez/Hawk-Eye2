using UnityEngine;

public class FightController : MonoBehaviour
{
    float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= GameData.GetDurationFight())
        {
            // Aquí puedes agregar la lógica para finalizar la pelea, como determinar el ganador o reiniciar el juego
            Debug.Log("La pelea ha terminado.");
            Destroy(this.gameObject); // Destruye el objeto de la pelea, o puedes implementar otra lógica según tus necesidades
        }
    }
}
