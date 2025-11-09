using UnityEngine;
using System.Collections;

public class MusicFadeController : MonoBehaviour
{
    public AudioSource musicSource;

    public void ResetMusic()
    {
        StartCoroutine(ResetMusicRoutine());
    }

    public void SlowMusic()
    {
        StartCoroutine(SlowMusicRoutine());
    }

    IEnumerator SlowMusicRoutine()
    {
        float startPitch = musicSource.pitch;
        float startVolume = musicSource.volume;

        float duration = 0.9f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            musicSource.pitch = Mathf.Lerp(startPitch, 0.45f, t / duration);
            musicSource.volume = Mathf.Lerp(startVolume, startVolume * 0.8f, t / duration);
            yield return null;
        }
    }
    IEnumerator ResetMusicRoutine()
    {
        float startPitch = musicSource.pitch;
        float startVolume = musicSource.volume;

        float duration = 0.9f;
        float t = 0f;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            musicSource.pitch = Mathf.Lerp(startPitch, 1f, t / duration);
            musicSource.volume = Mathf.Lerp(startVolume, startVolume * 1f, t / duration);
            yield return null;
        }
    }

}
