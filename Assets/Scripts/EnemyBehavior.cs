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
            rb.MovePosition(transform.position + (transform.right * Runner.DeltaTime * speed));
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.name == "CheckGround")
        {
            KnightPlayerBehavior playerBehavior = other.GetComponentInParent<KnightPlayerBehavior>();
            if (playerBehavior && !dead && other.GetComponent<BoxCollider2D>())
            {
                OnDeath();
                Physics2D.IgnoreCollision(this.GetComponent<CapsuleCollider2D>(), playerBehavior.gameObject.GetComponent<BoxCollider2D>(), true);
            }
        }
    }
    private void OnDeath()
    {
        StopAllCoroutines();
        dead = true;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.freezeRotation = true;
        animator.SetBool("dead", true);
    }

    private IEnumerator TimeToDestroy()
    {
        yield return new WaitForSeconds(vanishCD);
        Destroy(this.gameObject);
    }
}
