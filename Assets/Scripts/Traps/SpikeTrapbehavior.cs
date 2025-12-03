using Fusion;
using System.Collections;
using UnityEngine;

public class SpikeTrapbehavior : NetworkBehaviour
{
    private Collider2D objCollider;
    public LayerMask playerMask;
    public int vanishCD;

    private void Start()
    {
        objCollider = GetComponent<Collider2D>();
        StartCoroutine(Vanish());
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        KnightPlayerBehavior playerBehavior = other.GetComponent<KnightPlayerBehavior>();
        if (playerBehavior != null)
        {
            playerBehavior.onDeath();
        }
    }
    IEnumerator Vanish()
    {
        yield return new WaitForSeconds(vanishCD);
        Destroy(gameObject);
    }
}
