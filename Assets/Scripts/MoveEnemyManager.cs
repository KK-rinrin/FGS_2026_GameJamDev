using UnityEngine;

public class MoveEnemyManager : MonoBehaviour
{

    private Rigidbody2D rb;

    [Header("‰¡ˆÚ“®‘¬“x")]
    [SerializeField] private float kSpeed = -0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocityX = kSpeed;
    }
}
