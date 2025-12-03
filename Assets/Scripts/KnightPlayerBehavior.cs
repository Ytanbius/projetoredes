using UnityEngine;
using Fusion;

public class KnightPlayerBehavior : NetworkBehaviour
{
    public Animator animator;
    public BoxCollider2D groundCheck;
    private Rigidbody2D rb;
    private Camera cam;
    public InputManager input;

    public LayerMask groundMask;
    public Physics2D playerPhysics;

    [SerializeField] Vector2 move;

    public PlayerRef player;

    public GameObject checkpoint;

    public float jumpForce;
    public float moveSpeed;
    public int points;
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
        if(HasInputAuthority)
        {
            cam = Camera.main;
            cam.GetComponent<CameraMovement>().target = this.transform.gameObject;
            checkpoint = ServerManager.instance.firstCheckPoint;
        }
    }
    private void Start()
    {
        input = this.gameObject.GetComponent<InputManager>();
        player = this.Object.StateAuthority;
        animator = this.GetComponent<Animator>();
        groundCheck = GetComponentInChildren<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    public override void FixedUpdateNetwork()
    {
        GetInputs();
        Move();
        CheckGround();
        if (grounded && move.x == 0)
        {
            rb.linearVelocity *= drag;
        }
    }
    private void Move()
    {
        if (Mathf.Abs(move.x) > 0)
        {
            if (grounded)
                rb.linearVelocity = new Vector2(Mathf.Round(move.x) * moveSpeed, rb.linearVelocity.y);
            else
                rb.linearVelocity = new Vector2(Mathf.Round(move.x) * airDrag * moveSpeed, rb.linearVelocity.y);
            if (move.x > 0)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                animator.SetBool("IsWalking", true);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
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
            rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
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
        points -= 10;
        GameManager.instance.LoadLastCheckPoint(this.gameObject, checkpoint);
    }
    public void GetInputs()
    {
        move = input.move;
        jump = input.jump;
    }
    public void OnFinish()
    {
        Destroy(gameObject);
    }
}
