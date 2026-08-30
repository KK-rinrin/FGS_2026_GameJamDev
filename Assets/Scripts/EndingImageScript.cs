using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private Sprite success_sprite;
    [SerializeField] private Sprite failed_sprite;
    [SerializeField] private GameObject ending_bg_gobj;
    [SerializeField] private Sprite success_text_sprite;
    [SerializeField] private Sprite failed_text_sprite;
    [SerializeField] private UnityEngine.UI.Image text_image;
    public static bool is_success = true;

    public void Start()
    {
        SpriteRenderer ending_bg_sr = ending_bg_gobj.GetComponent<SpriteRenderer>();
        ending_bg_sr.sprite = is_success ? success_sprite : failed_sprite;
        text_image.sprite = is_success ? success_text_sprite : failed_text_sprite;
    }
}
