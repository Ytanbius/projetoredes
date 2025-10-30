using Fusion;
using UnityEngine;

public class SpikeTrapbehavior : NetworkBehaviour
{
    private Collider2D objCollider;
    public LayerMask playerMask;

    private void Start()
    {
        objCollider = GetComponent<Collider2D>();
    }
    public override void FixedUpdateNetwork()
    {
        GameObject player;
        KnightPlayerBehavior playerBehavior;
        player = Runner.GetPhysicsScene2D().OverlapArea(objCollider.bounds.min, objCollider.bounds.max, playerMask).gameObject;
        if (player != null)
        {
            playerBehavior = player.GetComponent<KnightPlayerBehavior>();
            playerBehavior.onDeath();
        }
    }
}
