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
    private void OnTriggerStay2D(Collider2D other)
    {
        KnightPlayerBehavior playerBehavior = other.GetComponent<KnightPlayerBehavior>();
        if (playerBehavior != null)
        {
            playerBehavior.onDeath();
        }
    }
}
