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
        bgm_audiosource.Play();
    }

    public void PlayMainBgm(AudioSource bgm_audiosource)
    {
        bgm_audiosource.clip = main_bgm;
        bgm_audiosource.Play();
    }

    public void OnJumpSE()
    {
        se_audiosource.PlayOneShot(jump_se, 5.0f);
    }

    public void OnReverseSE()
    {
        se_audiosource.PlayOneShot(reverse_se, 5.0f);
    }

    public void OnCollisionSE()
    {
        se_audiosource.PlayOneShot(collision_se, 5.0f);
    }

    public void OnClickSE()
    {
        //se_audiosource.PlayOneShot(click_se, 5.0f);
        StartCoroutine(WaitForSe(1.0f));
    }

    IEnumerator WaitForSe(float wait_time){
        se_audiosource.PlayOneShot(click_se, 5.0f);
        yield return new WaitForSeconds(wait_time);
    }
}
