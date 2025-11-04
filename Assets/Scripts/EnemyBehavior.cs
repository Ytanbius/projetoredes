using Fusion;
using System.Collections;
using UnityEngine;

public class EnemyBehavior : NetworkBehaviour
{
    public int deathCD;
    private bool dead = false;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        KnightPlayerBehavior playerBehavior = other.GetComponentInParent<KnightPlayerBehavior>();
        if(playerBehavior && !dead && other.GetComponent<BoxCollider2D>())
        {
            OnDeath();
            Physics2D.IgnoreCollision(this.GetComponent<CapsuleCollider2D>(), other.GetComponentInParent<CapsuleCollider2D>(), true);
        }
    }
    private void OnDeath()
    {
        StartCoroutine(DeathCD());
    }

    private IEnumerator DeathCD()
    {
        dead = true;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.freezeRotation = true;
        this.transform.localScale /= new Vector2(1, 2);
        yield return new WaitForSeconds(deathCD);
        Destroy(gameObject);
    }
}
