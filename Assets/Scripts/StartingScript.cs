using UnityEngine;
using UnityEditor;
using System;
using TMPro;
using UnityEngine.UI;

public class StartingScript : SoundManagerScript
{
    [SerializeField] private AudioSource starting_audiosource;
    [SerializeField] private Slider audio_volume_slider;
    [SerializeField] private Slider master_volume_slider;
    [SerializeField] private Canvas setting_panel;
    [SerializeField] private Button se_onoff_btn;
    [SerializeField] private TMP_Text se_onoff_btn_text;
    private bool is_setting_panel_opened = false;

    void Start()
    {
        setting_panel.enabled = false;
        is_setting_panel_opened = false;
        PlayStartingBgm(starting_audiosource);
        // starting_audiosource.clip = starting_bgm;
        // starting_audiosource.Play();
    }

    public void StartGameBTN()
    {
        OnClickSE();
        starting_audiosource.Stop();
        //Initiate.Fade("KaminagaScene", Color.black, 1.0f);
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

    public void OpenSettingBTN()
    {
        if (!is_setting_panel_opened)
        {
            setting_panel.enabled = true;
            is_setting_panel_opened = true;
        }
        else
        {
            setting_panel.enabled = false;
            is_setting_panel_opened = false;
        }
    }

    public void SeOnoffBTN()
    {
        if (se_amplification > 0.0f)
        {
            se_amplification = 0.0f;
            se_onoff_btn_text.text = "OFF";
        }
        else
        {
            se_amplification = 5.0f;
            se_onoff_btn_text.text = "ON";
        }
    }

    public void AudioVolumeSLIDER()
    {
        starting_audiosource.volume = audio_volume_slider.value * master_volume_slider.value;
        bgm_volume = audio_volume_slider.value * master_volume_slider.value;
        master_volume = master_volume_slider.value;
    }
}
