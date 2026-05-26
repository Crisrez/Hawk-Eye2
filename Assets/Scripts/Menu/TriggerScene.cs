using UnityEngine;

public class TriggerScene : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private SceneManager sceneManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            sceneManager.LoadScene(sceneName);
        }
    }
}
