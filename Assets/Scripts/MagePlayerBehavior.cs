using UnityEngine;
using Fusion;
using UnityEngine.InputSystem;

public class MagePlayerBehavior : NetworkBehaviour
{
    public Vector2 move;
    public bool interact;

    public int player;

    public float _moveSpeed;

    private void Start()
    {
        player = this.Object.StateAuthority.PlayerId;
    }
    public override void FixedUpdateNetwork()
    {
        Move();
    }
    private void Move()
    {
        this.transform.Translate(move * _moveSpeed * Runner.DeltaTime);
    }
    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }
    public void OnInteract(InputValue value)
    {
        interact = value.isPressed;
    }
    public void MoveInput(Vector2 moveDir)
    {
        move = moveDir;
    }
}
