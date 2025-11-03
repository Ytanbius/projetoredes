using UnityEngine;
using Fusion;
using UnityEngine.InputSystem;
using static Unity.Collections.Unicode;

public class MagePlayerBehavior : NetworkBehaviour
{
    public Vector2 move;
    private Rigidbody2D _rb;

    public float _moveSpeed;

    private void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }
    private void Move()
    {
        _rb.linearVelocity = move * Runner.DeltaTime * _moveSpeed;
    }
    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }
    public void MoveInput(Vector2 moveDir)
    {
        move = moveDir;
    }
}
