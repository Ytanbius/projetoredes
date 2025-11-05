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

    public int player;

    public GameObject checkpoint;

    public float jumpForce;
    public float moveSpeed;
    [Range(0f, 1f)]
    public float drag;

    public bool grounded;
    public bool jump = false;
    private void Start()
    {
        player = this.Object.StateAuthority.PlayerId;
        groundCheck = GetComponentInChildren<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    public override void FixedUpdateNetwork()
    {
        if (!grounded)
            jump = false;
        Move();
        CheckGround();
        if (grounded && move.x == 0 && rb.linearVelocity.y <= 0)
            rb.linearVelocity *= drag;
    }
    private void Move()
    {
        if (Mathf.Abs(move.x) > 0)
        {
            rb.linearVelocity = new Vector2(Mathf.Round(move.x) * Runner.DeltaTime * moveSpeed, rb.linearVelocity.y);
        }
        if (jump && grounded)
        {
            rb.AddForceY(Runner.DeltaTime * jumpForce, ForceMode2D.Impulse);
        }
    }

    void CheckGround()
    {
        grounded = Runner.GetPhysicsScene2D().OverlapArea(groundCheck.bounds.min, groundCheck.bounds.max, groundMask) != null;
    }

    public void onDeath()
    {
        GameManager.instance.LoadLastCheckPoint(this.gameObject, checkpoint);
    }

    //Inputs
    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }
    public void OnJump(InputValue value)
    {
        JumpInput(value.isPressed);
    }
    public void MoveInput(Vector2 moveDir)
    {
        move = moveDir;
    }
    public void JumpInput(bool trigger)
    {
        jump = trigger;
    }
}
