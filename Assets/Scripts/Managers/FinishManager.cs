using UnityEngine;
using Fusion;

public class FinishManager : NetworkBehaviour
{
    public GameObject player;
    public GameObject canvas;
    public GameObject timer;
    public KnightPlayerBehavior playerBehavior;
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("1");
        playerBehavior = other.gameObject.GetComponent<KnightPlayerBehavior>();
        if (playerBehavior != null)
        {
            Debug.Log("2");
            player = other.gameObject;
            Debug.Log("3");
            playerBehavior.OnFinish();
            Debug.Log("4");
            OnFinish();
            Debug.Log("5");
        }
    }
    public void onButtonPress()
    {
        GameManager.instance.LeaveRoom();
    }
    public void OnFinish()
    {
        timer.SetActive(false);
        canvas = Instantiate(canvas, Vector2.zero, Quaternion.identity);
    }
}
