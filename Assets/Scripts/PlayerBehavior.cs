using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 move;
    public bool jump = false;
    public float moveSpeed;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        rb.linearVelocity = new Vector2(move.x * moveSpeed * Time.deltaTime, rb.linearVelocityY);
    }
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
    public void JumpInput(bool jumpTrigger)
    {
        jump = jumpTrigger;
    }
}
