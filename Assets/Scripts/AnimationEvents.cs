using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public GameObject enemy;
    public void OnEnemyDeath()
    {
        Destroy(enemy);
    }
}
