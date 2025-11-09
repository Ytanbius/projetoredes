using UnityEngine;
using Fusion;
using UnityEngine.InputSystem;

public class MagePlayerBehavior : NetworkBehaviour
{
    private Camera cam;
    private InputManager input;

    public Vector2 move;
    public bool interact;

    public int player;

    public float _moveSpeed;

    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            cam = Camera.main;
            cam.GetComponent<CameraMovement>().target = transform.gameObject;
        }
    }
    private void Start()
    {
        input = GetComponent<InputManager>();
        player = this.Object.StateAuthority.PlayerId;
    }
    private void Update()
    {
        GetInput();
    }
    public override void FixedUpdateNetwork()
    {
        Move();
    }
    private void Move()
    {
        this.transform.Translate(move * _moveSpeed * Runner.DeltaTime);
    }
    private void GetInput()
    {
        move = new Vector2(input.move.x, 0);
        interact = input.interact;
    }

}
