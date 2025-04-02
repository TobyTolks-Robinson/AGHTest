using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;

    public AudioClip playerMoving;
    public AudioClip up, down, left, right;

    private AudioSource audioSource;
    private Coroutine narrationRoutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        else Destroy(gameObject);
    }

    public void PlayNarrtion(string direction)
    {
        if (narrationRoutine != null)
            StopCoroutine(narrationRoutine);

        narrationRoutine = StartCoroutine(PlayDirection(direction));
    }

    IEnumerator PlayDirection(string dir)
    {
        audioSource.Stop();

        if (playerMoving)
        {
            audioSource.clip = playerMoving;
            audioSource.Play();
            yield return new WaitForSeconds(playerMoving.length);
        }

        switch (dir)
        {
            case "up": audioSource.clip = up; break;
            case "down": audioSource.clip = down; break;
            case "left": audioSource.clip = left; break;
            case "right": audioSource.clip = right; break;
        }

        audioSource.Play();
    }

    public void FadeOutAllAudio(float duration)
    {
        StartCoroutine(FadeAudio(1.0f, 0.0f, duration));
     }

    public void FadeInAllAudio(float duration)
    {
        StartCoroutine(FadeAudio(0.0f, 1.0f, duration));
    }

    IEnumerator FadeAudio(float startVol, float endVol, float duration)
    {
        float t = 0.0f;
        audioSource.volume = startVol;

        while (t < duration)
        {
            t += Time.unscaledDeltaTime;
            audioSource.volume = Mathf.Lerp(startVol, endVol, t / duration);
            yield return null;
        }

        audioSource.volume = endVol;
    }

    public void PauseNarration() => audioSource.Pause();
    public void ResumeNarration() => audioSource.UnPause();

}
