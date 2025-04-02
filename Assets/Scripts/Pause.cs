using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Pause : MonoBehaviour
{

    public static Pause Instance;

    public Button pauseButton;

    private bool isPaused = false;
    private float fadeDuration = 1.0f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        pauseButton.onClick.AddListener(TogglePause);
    }

    public void TogglePause()
    {
        if(!isPaused)
        {
            StartCoroutine(FadeOutAndPause());
        }
        else
        {
            ResumeGame();
        }
    }


   IEnumerator FadeOutAndPause()
   {
        AudioManager.Instance.FadeOutAllAudio(fadeDuration);

        float timer = 0.0f;
        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = 0;
        isPaused = true;
   }



    public void ResumeGame()
    {
        Time.timeScale = 1;
        AudioManager.Instance.FadeInAllAudio(fadeDuration);
        isPaused = false;
    }

}
