using Fusion;
using System.Collections;
using UnityEngine;

public class EnemyBehavior : NetworkBehaviour
{
    public int vanishCD;
    public float speed;
    private bool dead = false;
    private Rigidbody2D rb;
    public Animator animator;

    public override void Spawned()
    {
        StartCoroutine(TimeToDestroy());
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public override void FixedUpdateNetwork()
    {
            rb.linearVelocity = new Vector2(transform.right.x * Runner.DeltaTime * speed, rb.linearVelocityY);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        KnightPlayerBehavior playerBehavior = other.GetComponentInParent<KnightPlayerBehavior>();
        if (other.gameObject.name == "CheckGround")
        {
            if (playerBehavior && !dead && other.GetComponent<BoxCollider2D>() && other.transform.position.y >= transform.position.y+transform.localScale.y/2)
            {
                OnDeath();
                Physics2D.IgnoreCollision(this.GetComponent<CapsuleCollider2D>(), playerBehavior.gameObject.GetComponent<BoxCollider2D>(), true);
            }
        }
        else if (playerBehavior && !dead)
        {
            playerBehavior.onDeath();
        }
    }
    private void OnDeath()
    {
        StopAllCoroutines();
        dead = true;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.freezeRotation = true;
        animator.SetTrigger("dead");
    }

    private IEnumerator TimeToDestroy()
    {
        yield return new WaitForSeconds(vanishCD);
        Destroy(this.gameObject);
    }
}
