using Unity.VisualScripting;
using UnityEngine;
using Fusion;
using UnityEngine.InputSystem;
using System.Collections;

public class KnightPlayerBehavior : NetworkBehaviour
{
    public BoxCollider2D groundCheck;

    private Rigidbody2D rb;

    public LayerMask groundMask;

    private Vector2 move;

    public float jumpForce;
    public float moveSpeed;
    [Range(0f, 1f)]
    public float drag;

    public bool jump = false;
    public bool grounded;
    private void Start()
    {
        groundCheck = GetComponentInChildren<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Move();
    }
    public override void FixedUpdateNetwork()
    {
        CheckGround();
        if(grounded && move.x == 0 && rb.linearVelocity.y <= 0)
            rb.linearVelocity *= drag;
    }
    private void Move()
    {
        if (Mathf.Abs(move.x) > 0)
        {
            rb.linearVelocity = new Vector2(move.x * Runner.DeltaTime * moveSpeed, rb.linearVelocity.y);
        }
        if (move.y > 0 && grounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, move.y * Runner.DeltaTime * jumpForce);
        }
    }

    void CheckGround()
    {
        grounded = Physics2D.OverlapAreaAll(groundCheck.bounds.min, groundCheck.bounds.max, groundMask).Length > 0;
    }


    //Inputs
    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }
    public void MoveInput(Vector2 moveDir)
    {
        move = moveDir;
        Debug.Log(move);
    }
}
