using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Rendering.Universal;

public class SoundManagerScript : MonoBehaviour
{
    [SerializeField] private AudioSource se_audiosource;
    [SerializeField] private AudioClip main_bgm;
    [SerializeField] private AudioClip starting_bgm;
    [SerializeField] private AudioClip jump_se;
    [SerializeField] private AudioClip reverse_se;
    [SerializeField] private AudioClip collision_se;
    [SerializeField] private AudioClip click_se;
    protected static float se_amplification = 5.0f;
    protected static float bgm_volume = 1.0f;
    protected static float master_volume = 1.0f;

    void Update()
    {
        #if DEBUG
        if (Input.GetKeyDown(KeyCode.Return)){
            OnClickSE();
        }
        #endif
    }

    public void PlayStartingBgm(AudioSource bgm_audiosource)
    {
        bgm_audiosource.clip = starting_bgm;
        bgm_audiosource.volume = bgm_volume;
        bgm_audiosource.Play();
    }

    public void PlayMainBgm(AudioSource bgm_audiosource)
    {
        bgm_audiosource.clip = main_bgm;
        bgm_audiosource.volume = bgm_volume;
        bgm_audiosource.Play();
    }

    public void OnJumpSE()
    {
        se_audiosource.PlayOneShot(jump_se, se_amplification * master_volume);
    }

    public void OnReverseSE()
    {
        se_audiosource.PlayOneShot(reverse_se, se_amplification * master_volume);
    }

    public void OnCollisionSE()
    {
        se_audiosource.PlayOneShot(collision_se, se_amplification * master_volume);
    }

    public void OnClickSE()
    {
        se_audiosource.PlayOneShot(click_se, se_amplification * master_volume);
    }
}
