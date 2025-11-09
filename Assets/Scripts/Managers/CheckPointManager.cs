using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public void LoadLastCheckPoint(GameObject player, GameObject checkpoint)
    {
        player.transform.position = checkpoint.transform.position;
    }
    public GameObject ChangeCheckPoint(GameObject curCheckPoint, GameObject otherCheckPoint)
    {
        CheckPointBehavior curCheckPointBehavior = curCheckPoint.GetComponent<CheckPointBehavior>();
        CheckPointBehavior otherCheckPointBehavior = otherCheckPoint.GetComponent<CheckPointBehavior>();
        if (curCheckPointBehavior.checkpointProgress < otherCheckPointBehavior.checkpointProgress || curCheckPoint == null)
            curCheckPoint = otherCheckPoint;
        return curCheckPoint;
    }
}
