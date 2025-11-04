using UnityEngine;
using Fusion;

public class TrapBehavior : NetworkBehaviour
{
    public GameObject trapPrefab;
    private MagePlayerBehavior playerBehavior;
    private PlayerRef playerRef;

    private void OnTriggerStay2D(Collider2D other)
    {
        playerBehavior = other.GetComponent<MagePlayerBehavior>();
        if(playerBehavior.Object.HasStateAuthority && playerBehavior)
        {
            Instantiate(trapPrefab, this.transform.position, Quaternion.identity);
            Debug.Log("sla man");
        }
    }
}
