using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    private SpriteRenderer sprite;

    [Header("重力を逆にすることができるクールタイム")]
    [SerializeField] private float kInvertCoolTime = 2.0f;

    [Header("横移動速度")]
    [SerializeField] private float kSpeed = 2.0f;

    private float currentInvertTime = 0f;

    private enum PlayerState
    {
        None,
        Running,        // 走っている状態
        TopRunning,     // 天井を走っている状態
        Inverting,      // 反転中
        Damage,         // ダメージ中
        Dead            // やられた時
    }

    private PlayerState state;

    private bool isOnTop = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        currentInvertTime = kInvertCoolTime;
        state = PlayerState.Running;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case PlayerState.Running:
                RunningUpdate();
                break;
            case PlayerState.Inverting:
                break;
        }
    }

    private void RunningUpdate()
    {
        currentInvertTime += Time.deltaTime;

        if (currentInvertTime > kInvertCoolTime)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                rb.gravityScale *= -1.0f;

                sprite.flipY = !sprite.flipY;

                currentInvertTime = 0.0f;
            }
        }

        rb.linearVelocityX = kSpeed;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
