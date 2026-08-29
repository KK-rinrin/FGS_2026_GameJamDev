using UnityEngine;
using UnityEditor;
using System;

public class StartingScript : MonoBehaviour
{
    [SerializeField] private AudioSource starting_audiosource;
    [SerializeField] private AudioClip starting_bgm;

    void Start()
    {
        starting_audiosource.clip = starting_bgm;
        starting_audiosource.Play();
    }

    public void StartGameBTN()
    {
        starting_audiosource.Stop();
        UnityEngine.SceneManagement.SceneManager.LoadScene("KaminagaScene");
    }

    public void QuitGameBTN()
    {
        starting_audiosource.Stop();
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void RetryGameBTN()
    {
        starting_audiosource.Stop();
        Initiate.Fade("TitleScene", Color.black, 1.0f);
    }
}
