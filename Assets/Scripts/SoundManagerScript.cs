using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Rendering.Universal;

public class SoundManagerScript : MonoBehaviour
{
    [SerializeField] private AudioSource main_audiosource;
    [SerializeField] private AudioSource se_audiosource;
    [SerializeField] private AudioClip main_bgm;
    [SerializeField] private AudioClip jump_se;
    [SerializeField] private AudioClip reverse_se;
    [SerializeField] private AudioClip collision_se;

    void Start()
    {
        main_audiosource.clip = main_bgm;
        main_audiosource.Play();
    }

    public void OnJumpSE()
    {
        print("jump se");
        se_audiosource.PlayOneShot(jump_se);
    }

    public void OnReverseSE()
    {
        print("reverse se");
        se_audiosource.PlayOneShot(reverse_se);
    }

    public void OnCollisionSE()
    {
        print("collsion se");
        se_audiosource.PlayOneShot(collision_se);
    }
}
