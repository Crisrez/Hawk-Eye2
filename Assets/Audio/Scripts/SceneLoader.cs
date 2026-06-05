using System.Collections;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class SceneLoader : MonoBehaviour
{
    [Header("FMOD")]
    [SerializeField] private EventReference menuMusicEvent;

    [Header("Fade")]
    [SerializeField] private float fadeOutTime = 1.5f;

    private EventInstance menuMusicInstance;
    private bool isLoading;
    private bool musicReleased;

    private void Start()
    {
        if (menuMusicEvent.IsNull)
        {
            Debug.LogError("SceneLoader: falta asignar la música del menú.");
            return;
        }

        menuMusicInstance = RuntimeManager.CreateInstance(menuMusicEvent);
        menuMusicInstance.setVolume(1f);
        menuMusicInstance.start();
    }

    public void LoadScene(string sceneName)
    {
        if (isLoading)
        {
            return;
        }

        StartCoroutine(FadeOutAndLoad(sceneName));
    }

    private IEnumerator FadeOutAndLoad(string sceneName)
    {
        isLoading = true;

        float elapsed = 0f;

        while (elapsed < fadeOutTime)
        {
            elapsed += Time.unscaledDeltaTime;

            float normalizedTime = Mathf.Clamp01(elapsed / fadeOutTime);
            float volume = Mathf.Lerp(1f, 0f, normalizedTime);

            menuMusicInstance.setVolume(volume);

            yield return null;
        }

        // Garantiza que llegue exactamente a cero antes de detenerla.
        menuMusicInstance.setVolume(0f);
        menuMusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        menuMusicInstance.release();

        musicReleased = true;

        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    private void OnDestroy()
    {
        // Evita volver a detener una instancia ya liberada.
        if (!musicReleased && menuMusicInstance.isValid())
        {
            menuMusicInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            menuMusicInstance.release();
        }
    }
}