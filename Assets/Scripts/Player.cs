using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;

    private SpriteRenderer sprite;
    [Header("HP管理スクリプト")]
    [SerializeField] private PlayerHpScript playerHpScript;

    [Header("チェックポイント管理オブジェクト")]
    [SerializeField] private CheckPointManager pointManager;

    [Header("重力を逆にすることができるクールタイム")]
    [SerializeField] private float kInvertCoolTime = 2.0f;
    
    [Header("横移動速度")]
    [SerializeField] private float kSpeed = 2.0f;
    
    [Header("スタン時間")]
    [SerializeField] private float kStunTime = 2.0f;
    
    [Header("リスポーン後待つ時間")]
    [SerializeField] private float kWaitTime = 1.0f;

    private float currentInvertTime = 0f;

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
        rb = GetComponent<Rigidbody2D>();
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
            case PlayerState.Inverting:
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

        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            rb.AddForce(new Vector2(0.0f, 700.0f));
        }

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

    private void DamageUpdate()
    {
        timeCount += Time.deltaTime;

        rb.linearVelocityX = 0.0f;

        if (timeCount > kStunTime)
        {
            TurningBack();
            print("*****");
            if (playerHpScript != null)
            {
                int x = playerHpScript.HpMinus(1);
                print(x);
            }
            state = PlayerState.Waiting;
        }
    }

    private void WaitingUpdate()
    {
        timeCount += Time.deltaTime;

        rb.linearVelocityX = 0.0f;

        if(timeCount > kWaitTime)
        {
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

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            if(state != PlayerState.Damage)
            {
                state = PlayerState.Damage;
                timeCount = 0.0f;
            }
        }

        if(collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
