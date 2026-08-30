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
    public static bool has_se = true;
    public static float bgm_volume = 1.0f;

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
        if (!has_se) {return;}
        se_audiosource.PlayOneShot(jump_se, 5.0f);
    }

    public void OnReverseSE()
    {
        if (!has_se) {return;}
        se_audiosource.PlayOneShot(reverse_se, 5.0f);
    }

    public void OnCollisionSE()
    {
        if (!has_se) {return;}
        se_audiosource.PlayOneShot(collision_se, 5.0f);
    }

    public void OnClickSE()
    {
        if (!has_se) {return;}
        se_audiosource.PlayOneShot(click_se, 5.0f);
    }
}
