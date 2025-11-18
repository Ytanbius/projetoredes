using Unity.VisualScripting;
using UnityEngine;
using Fusion;
using UnityEngine.InputSystem;
using System.Collections;

public class KnightPlayerBehavior : NetworkBehaviour
{
    public Animator animator;
    public BoxCollider2D groundCheck;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Camera cam;
    public InputManager input;

    public LayerMask groundMask;

    [SerializeField] Vector2 move;

    public PlayerRef player;

    public GameObject checkpoint;

    public float jumpForce;
    public float moveSpeed;
    public float gravity = -9.8f;
    [Range(0f, 1f)]
    public float drag;
    [Range(0f, 1f)]
    public float airDrag;

    public bool grounded;
    public bool jump = false;
    private bool jumped = false;

    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            cam = Camera.main;
            cam.GetComponent<CameraMovement>().target = this.transform.gameObject;
            checkpoint = ServerManager.instance.firstCheckPoint;
        }
    }
    private void Start()
    {
        input = this.GetComponent<InputManager>();
        animator = this.GetComponent<Animator>();
        sprite = this.GetComponent<SpriteRenderer>();
        player = this.Object.StateAuthority;
        groundCheck = GetComponentInChildren<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        GetInputs();
    }
    public override void FixedUpdateNetwork()
    {
        Move();
        CheckGround();
        if (grounded && move.x == 0 && rb.linearVelocity.y <= 0)
            rb.linearVelocity *= drag;
    }
    private void Move()
    {
        if (Mathf.Abs(move.x) > 0)
        {
            if (grounded)
                rb.linearVelocity = new Vector2(Mathf.Round(move.x) * Runner.DeltaTime * moveSpeed, rb.linearVelocity.y);
            else
                rb.linearVelocity = new Vector2(Mathf.Round(move.x) * airDrag * Runner.DeltaTime * moveSpeed, rb.linearVelocity.y);
            if (move.x > 0)
            {
                sprite.flipX = false;
                animator.SetBool("IsWalking", true);
            }
            else
            {
                sprite.flipX = true;
                animator.SetBool("IsWalking", true);
            }
        }
        else
            animator.SetBool("IsWalking", false);
        if (!grounded && jumped)
        {
            input.jump = false;
        }
        if (jumped && grounded && !jump)
        {
            animator.SetBool("IsJumping", false);
            jumped = false;
        }
        if (jump && grounded && !jumped)
        {
            transform.Translate(Vector3.up * Runner.DeltaTime * jumpForce * gravity);
            animator.SetBool("IsJumping", true);
            jumped = true;
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
    public void GetInputs()
    {
        move = input.move;
        jump = input.jump;
    }
}
