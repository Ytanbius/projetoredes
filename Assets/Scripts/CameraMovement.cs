using UnityEngine;
using Fusion;

public class CameraMovement : MonoBehaviour
{
    public GameObject target;
    public KnightPlayerBehavior knightBehavior;
    public MagePlayerBehavior mageBehavior;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
        if (target == null)
            return;
        knightBehavior = target.GetComponent<KnightPlayerBehavior>();
        mageBehavior = target.GetComponent<MagePlayerBehavior>();
    }
    public void LateUpdate()
    {
        if (knightBehavior != null)
        {
            transform.position = target.transform.position + (Vector3.back * 6);
        }
        else if (mageBehavior != null)
        {
            transform.position = new Vector3(target.transform.position.x, 0, -10);
        }
        else
            return;
    }
}
