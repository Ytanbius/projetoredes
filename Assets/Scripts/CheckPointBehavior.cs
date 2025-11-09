using UnityEngine;
using Fusion;

public class CheckPointBehavior : NetworkBehaviour
{
    public int checkpointProgress;
    [SerializeField] KnightPlayerBehavior playerBehavior;

    private void OnTriggerStay2D(Collider2D other)
    {
        playerBehavior = other.gameObject.GetComponent<KnightPlayerBehavior>();
        if(playerBehavior != null )
        {
            playerBehavior.checkpoint = GameManager.instance.checkPointManager.ChangeCheckPoint(playerBehavior.checkpoint, this.gameObject);
        }
    }
}
