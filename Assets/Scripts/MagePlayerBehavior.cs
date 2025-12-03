using Fusion;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;

public class MagePlayerBehavior : NetworkBehaviour
{
    private Camera cam;
    private InputManager input;
    public TrapBehavior nearestTrap;
    public CanvasGroup escolhaUI;
    public GameObject hud;
    public GameObject hudTimer;
    public Rigidbody2D _rb;

    public Vector2 move;
    public bool interact;

    public PlayerRef player;
    private int trapIndex;

    public float _moveSpeed;
    public bool canMove = true;
    public bool canChoose = false;
    public string chooseOption;
    public GameObject[] traps;

    public override void Spawned()
    {
        if (HasStateAuthority)
        {
            cam = Camera.main;
            cam.GetComponent<CameraMovement>().target = transform.gameObject;
            hud = Instantiate(hud, Vector2.zero, Quaternion.identity);
            hudTimer = Instantiate(hudTimer, Vector2.zero, Quaternion.identity);
            escolhaUI = hud.GetComponentInChildren<CanvasGroup>(name == "Escolha");
            if(GetComponent<InputManager>().HasStateAuthority)
                input = GetComponent<InputManager>();
            player = this.Object.StateAuthority;
            _rb = GetComponent<Rigidbody2D>();
        }
    }
    private void Update()
    {
        InteractTrap();
        if(canChoose)
        {
            ChooseOption();
        }
    }
    public override void FixedUpdateNetwork()
    {
        GetInput();
        Move();
    }
    public void InteractTrap()
    {
        if (interact && nearestTrap != null)
        {
            if(!nearestTrap.canActivate && !nearestTrap.hasActivated)
            {
                escolhaUI.alpha = 1;
                escolhaUI.blocksRaycasts = true;
                canMove = false;
                canChoose = true;
            }
            else if (!nearestTrap.hasActivated && nearestTrap.canActivate)
            {
                nearestTrap.onActivate();
                nearestTrap.hasActivated = true;
                interact = false;
            }
            interact = false;
        }
    }
    private void Move()
    {
        if(input.move != Vector2.zero)
            _rb.MovePosition((Vector2)transform.position + (input.move * _moveSpeed * Runner.DeltaTime));
    }
    private void GetInput()
    {
        move = input.move;
        interact = input.interact;
    }

    public void ChooseOption()
    {
        InputSystem.onAnyButtonPress.Call(currentAction =>
        {
            if (currentAction is KeyControl key)
            {
                if(int.TryParse(currentAction.name, out trapIndex))
                    if(trapIndex <= traps.Length && trapIndex > 0)
                        OnChoose(traps[trapIndex-1]);
            }
        });
    }

    public void OnChoose(GameObject prefab)
    {
        nearestTrap.trapPrefab = prefab;
        nearestTrap.canActivate = true;
        escolhaUI.alpha = 0;
        escolhaUI.blocksRaycasts = false;
        canMove = true;
        canChoose = false;
    }
}
