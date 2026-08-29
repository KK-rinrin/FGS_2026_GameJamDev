using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;

public class StartingScript : SoundManagerScript
{
    [SerializeField] private AudioSource starting_audiosource;
    [SerializeField] private Slider audio_volume_slider;

    void Start()
    {
        PlayStartingBgm(starting_audiosource);
        // starting_audiosource.clip = starting_bgm;
        // starting_audiosource.Play();
    }

    public void StartGameBTN()
    {
        OnClickSE();
        starting_audiosource.Stop();
        UnityEngine.SceneManagement.SceneManager.LoadScene("KaminagaScene");
    }

    public void QuitGameBTN()
    {
        OnClickSE();
        starting_audiosource.Stop();
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void RetryGameBTN()
    {
        OnClickSE();
        starting_audiosource.Stop();
        Initiate.Fade("TitleScene", Color.black, 1.0f);
    }

    public void AudioVolumeSLIDER()
    {
        starting_audiosource.volume = audio_volume_slider.value;
    }
}
