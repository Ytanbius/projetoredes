using UnityEngine;
using UnityEngine.InputSystem;
using Fusion;

public class InputManager : NetworkBehaviour
{
    public bool interact;
    public bool jump;
    public Vector2 move;
    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }
    public void OnJump(InputValue value)
    {
        JumpInput(value.isPressed);
    }
    public void OnInteract(InputValue value)
    {
        interact = value.isPressed;
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
