
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : SoundManagerScript
{
    private Rigidbody2D rb;

    private Animator animator;
    [SerializeField] private AudioSource bgm_audiosource;
    [SerializeField] private UnityEngine.UI.Image freezetime_icon_anim;

    private SpriteRenderer sprite;
    [Header("HP管理スクリプト")]
    [SerializeField] private PlayerHpScript playerHpScript;

    [Header("チェックポイント管理オブジェクト")]
    [SerializeField] private CheckPointManager pointManager;

    [Header("重力を逆にすることができるクールタイム")]
    [SerializeField] private float kInvertCoolTime = 2.0f;
    
    [Header("重力を逆にしたときの落下速度")]
    [SerializeField] private float kInvertSpeed = 15.0f;
    
    [Header("横移動速度")]
    [SerializeField] private float kSpeed = 2.0f;
    
    [Header("スタン時間")]
    [SerializeField] private float kStunTime = 2.0f;
    
    [Header("リスポーン後待つ時間")]
    [SerializeField] private float kWaitTime = 1.0f;

    [Header("ジャンプの力")]
    [SerializeField] private float kJumpPower = 700.0f;

    private float currentInvertTime = 0f;

    private float freeInvertTime = 10.0f;

    private float freeInvertEndTime = 10.0f;

    private float timeCount = 0f;

    private enum PlayerState
    {
        None,
        Running,        // 走っている状態
        TopRunning,     // 天井を走っている状態
        Inverting,      // 反転中
        Jumping,        // ジャンプ中
        Damage,         // ダメージ中
        Waiting,        // 止まっている状態
        Dead            // やられた時
    }

    private PlayerState state;

    private bool isOnTop = false;

    private bool isJumping = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        freezetime_icon_anim.type = UnityEngine.UI.Image.Type.Filled;
        freezetime_icon_anim.fillAmount = 1.0f;
        PlayMainBgm(bgm_audiosource);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        if (playerHpScript == null)
        {
            playerHpScript = FindAnyObjectByType<PlayerHpScript>();
        }

        if (playerHpScript == null)
        {
            Debug.LogError("PlayerHpScript が見つかりません。Inspector で設定するか、シーンに配置してください。");
        }

        currentInvertTime = kInvertCoolTime;
        state = PlayerState.Running;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case PlayerState.None:
                break;
            case PlayerState.Running:
                RunningUpdate();
                break;
            case PlayerState.TopRunning:
                RunningUpdate();
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case PlayerState.Inverting:
                InvertUpdate();
                break;
            case PlayerState.Jumping:
                break;
            case PlayerState.Damage:
                DamageUpdate();
                break;
            case PlayerState.Waiting:
                WaitingUpdate();
                break;
            case PlayerState.Dead:
                break;
        }
    }

    private void RunningUpdate()
    {
        currentInvertTime += Time.deltaTime;
    
        if (currentInvertTime <= kInvertCoolTime && !IsFreeInverse())
        {
            freezetime_icon_anim.fillAmount = currentInvertTime / kInvertCoolTime;
        }

        if (!isJumping && state != PlayerState.Inverting && Keyboard.current.pKey.wasPressedThisFrame)
        {
            OnJumpSE();
            // ジャンプの力を重力の符号によって向きを変えながら与える
            rb.AddForce(new Vector2(0.0f, kJumpPower * Mathf.Sign(rb.gravityScale)));

            isJumping = true;

            animator.Play("player_jump");
        }

        if (IsFreeInverse())
        {
            freeInvertTime += Time.deltaTime;

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                OnReverseSE();
                state = PlayerState.Inverting;

                rb.gravityScale *= -1.0f;

                sprite.flipY = !sprite.flipY;

                currentInvertTime = 0.0f;

                animator.Play("player_jump");
            }
        }

        if (currentInvertTime > kInvertCoolTime && !isJumping)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame && !IsFreeInverse())
            {
                OnReverseSE();
                state = PlayerState.Inverting;
                freezetime_icon_anim.fillAmount = 0.0f;

                rb.gravityScale *= -1.0f;

                sprite.flipY = !sprite.flipY;

                currentInvertTime = 0.0f;

                animator.Play("player_jump");
            }
        }

        rb.linearVelocityX = kSpeed;
    }
    private void InvertUpdate()
    {
        rb.AddForce(new Vector2(0.0f, kInvertSpeed * -Mathf.Sign(rb.gravityScale)));
    }

    private void DamageUpdate()
    {
        timeCount += Time.deltaTime;

        rb.linearVelocityX = 0.0f;

        rb.bodyType = RigidbodyType2D.Static;

        if (timeCount > kStunTime)
        {
            TurningBack();
            if (playerHpScript != null)
            {
                int x = playerHpScript.HpMinus(1);
                print(x);
            }
            rb.bodyType = RigidbodyType2D.Dynamic;
            animator.Play("player_wait");
            state = PlayerState.Waiting;
        }
    }

    private void WaitingUpdate()
    {
        timeCount += Time.deltaTime;

        rb.linearVelocityX = 0.0f;

        if(timeCount > kWaitTime)
        {
            animator.Play("player_run");
            state = PlayerState.Running;
            currentInvertTime = kInvertCoolTime;
        }
    }

    // 今後チェックポイントが実装されたらその地点からリスポーンするようにするかも
    private void TurningBack()
    {
        Vector3 backPos = pointManager.GetCheckPoint().position;
        backPos.y = transform.position.y;

        transform.position = backPos;
    }

    public void AddSpeed(float speed)
    {
        kSpeed += speed;
    }

    public void SetFreeInverse(float time)
    {
        freeInvertEndTime = time;
        freeInvertTime = 0.0f;
    }

    public bool IsFreeInverse()
    {
        return freeInvertEndTime >= freeInvertTime;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            if(state != PlayerState.Damage)
            {
                OnCollisionSE();
                state = PlayerState.Damage;
                animator.Play("player_damage");
                freezetime_icon_anim.fillAmount = 1.0f;
                timeCount = 0.0f;
            }
        }

        if(collision.gameObject.CompareTag("Ground"))
        {
            animator.Play("player_run");

            isJumping = false;

            if(state == PlayerState.Inverting)
            {
                state = PlayerState.Running;
            }
        }
    }
}
