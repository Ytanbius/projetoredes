using UnityEngine;
using Fusion;

public class CameraMovement : MonoBehaviour
{
    public GameObject target;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Update()
    {
        if (target == null)
            return;
    }
    public void LateUpdate()
    {
            transform.position = target.transform.position + (Vector3.back * 6);
    }
}
