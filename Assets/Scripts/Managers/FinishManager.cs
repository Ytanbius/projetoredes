using UnityEngine;
using Fusion;

public class FinishManager : NetworkBehaviour
{
    public GameObject player;
    public Canvas canvas;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<KnightPlayerBehavior>() != null)
        {
            player = collision.gameObject;
            player.GetComponent<KnightPlayerBehavior>().OnFinish();
            OnFinish();
        }
    }
    public void onButtonPress()
    {
        GameManager.instance.LeaveRoom();
    }
    public void OnFinish()
    {
        canvas = Instantiate(canvas);
    }
}
